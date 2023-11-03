using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwicher : MonoBehaviour
{
    public void SceneLoad(SceneAsset sceneAsset)
    {
        SceneManager.LoadScene(sceneAsset.name);
    }
}
