using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private bool _isAnyPressKeys = false;

    public void SceneLoad(SceneAsset sceneAsset)
    {
        if (_isAnyPressKeys)
        {
            InputSystem.onAnyButtonPress.CallOnce(ctrl => SceneManager.LoadScene(sceneAsset.name));
        }

        SceneManager.LoadScene(sceneAsset.name);
    }
}
