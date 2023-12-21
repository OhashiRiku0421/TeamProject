using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.UIElements;
using UnityEditor.VersionControl;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;


public class ShaderKeywordWindow : EditorWindow
{
    // メニューに追加
    [MenuItem("Window/ShaderKeywordControlWindow")]
    public static void ShowWindow()
    {
        // ウィンドウの作成
        var window = GetWindow<ShaderKeywordWindow>();
        // ウィンドウタイトル
        window.titleContent = new GUIContent("ShaderKeywordControl");
    }

    private void OnEnable()
    {
        var root = CreateGUI();

        foreach (var VARIABLE in root)
        {
            rootVisualElement.Add(VARIABLE);
        }
    }

    /// <summary>メッシュを新しく作る</summary>
    private TextField _meshCreatePathField = default;

    /// <summary>現在セットされているMesh</summary>
    private Object _fbx = default;
    private VisualElement[] CreateGUI()
    {
        var enableButton = new Button { text = "EnableCurveKeyword" };
        var disableButton = new Button { text = "DisableCurveKeyword" };
        var fbxProp = new ObjectField { label = "FBX", objectType = typeof(Object) };
        _meshCreatePathField = new TextField { label = "CreatePath" };
        var createMeshButton = new Button { text = "RecalculateMeshNormal" };
        
        enableButton.clicked += () => Shader.EnableKeyword("_CUSTOM_TOON_CURVED");
        disableButton.clicked += () => Shader.DisableKeyword("_CUSTOM_TOON_CURVED");
        fbxProp.RegisterValueChangedCallback(OnMeshChanged);
        createMeshButton.clicked += CreateMesh;

        return new VisualElement[] { enableButton, disableButton, fbxProp, _meshCreatePathField, createMeshButton };
    }

    private void OnMeshChanged(ChangeEvent<UnityEngine.Object> callBack)
    {
        _fbx = callBack.newValue;
    }

    private void CreateMesh()
    {
        Debug.Log($"OldPath : {AssetDatabase.GetAssetPath(_fbx)}");

        var path = AssetDatabase.GetAssetPath(_fbx);

        var newFBXImporter = AssetImporter.GetAtPath(path);

        if (newFBXImporter is ModelImporter)
        {
            var modelImporter = (ModelImporter)newFBXImporter;

            //modelImporter.weldVertices = false;
            modelImporter.normalSmoothingAngle = 180F;
            modelImporter.importNormals = ModelImporterNormals.Calculate;
            
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            var normals = new Dictionary<string, List<Vector3>>();
            var triangles = new Dictionary<string, int[]>();
            var vertPos = new Dictionary<string, Vector3[]>();

            {
                var subAssets = AssetDatabase.LoadAllAssetsAtPath(path);

                foreach (var i in subAssets)
                {
                    if (i is Mesh)
                    {
                        var mesh = (Mesh)i;

                        var tempNormals = new List<Vector3>(mesh.vertexCount);
                        var tempTriangle = new int[mesh.triangles.Length];
                        var tempVertPos = new Vector3[mesh.vertexCount];
                        
                        mesh.GetNormals(tempNormals);
                        Array.Copy(mesh.triangles, tempTriangle, mesh.triangles.Length);
                        Array.Copy(mesh.vertices, tempVertPos, mesh.vertexCount);
                        
                        triangles.Add(mesh.name, tempTriangle);
                        normals.Add(mesh.name, tempNormals);
                        vertPos.Add(mesh.name, tempVertPos);
                    }
                }
            }
            
            modelImporter.normalSmoothingAngle = 90F;
            modelImporter.importNormals = ModelImporterNormals.Import;

            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            {
                // var subAssets = AssetDatabase.LoadAllAssetsAtPath(path);
                //
                // foreach (var i in subAssets)
                // {
                //     if (i is Mesh)
                //     {
                //         var mesh = (Mesh)i;
                //
                //         var writeNormals = normals[mesh.name];
                //
                //         Vector3[] vertColor = new Vector3[mesh.vertexCount];
                //         
                //         for (int j = 0; j < mesh.triangles.Length; j++)
                //         {
                //             if (mesh.vertices[mesh.triangles[j]] == vertPos[mesh.name][triangles[mesh.name][j]])
                //             {
                //                 vertColor[mesh.triangles[j]] = writeNormals[triangles[mesh.name][j]];
                //             }
                //             else
                //             {
                //                 var index = Array.IndexOf(mesh.vertices ,vertPos[mesh.name][triangles[mesh.name][j]]);
                //                 if (index >= 0)
                //                 {
                //                     Debug.Log("A");
                //                     vertColor[index] = writeNormals[triangles[mesh.name][j]];
                //                 }
                //                 else
                //                 {
                //                     Debug.Log("B");
                //                 }
                //             }
                //         }
                //         
                //         
                //         mesh.SetColors(vertColor.Select(vec => new Color(vec.x, vec.y, vec.z, 0.0F)).ToList());
                //     }
                // }
                
                AssetDatabase.SaveAssets();
            }
        }
    }
}
