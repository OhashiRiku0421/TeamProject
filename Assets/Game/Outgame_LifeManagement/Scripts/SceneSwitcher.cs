using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField, Tooltip("任意のキーを入力したらシーンが変わるようにするフラグ")]
    private bool _isAnyKeyPressed = false;

    [SerializeField, Tooltip("遷移したいシーンのシーンアセット")]
    private SceneAsset _sceneAsset;

    private void Update()
    {
        if (_isAnyKeyPressed)
        {
            InputSystem.onAnyButtonPress.CallOnce(ctrl => SceneManager.LoadScene(_sceneAsset.name));
        }
    }
    public void SceneLoad()
    {
        SceneManager.LoadScene(_sceneAsset.name);
    }
}
