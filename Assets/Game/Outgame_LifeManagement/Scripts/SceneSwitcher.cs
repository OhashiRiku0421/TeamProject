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

    [SerializeField, Tooltip("シーンが遷移するまでの時間")]
    private float _waitTime = 0.666f;

    private void Start()
    {
        if (_isAnyKeyPressed)
        {
            InputSystem.onAnyButtonPress.CallOnce(ctrl => SceneLoaded());
        }
    }
    public void SceneLoaded()
    {
        PlaySE();
        Invoke("SceneLoad", _waitTime);
    }
    private void SceneLoad()
    {
        SceneManager.LoadScene(_sceneAsset.name);
    }
    private void PlaySE()
    {
        CriAudioManager.Instance.SE.Play("UI", "SE_Select");
    }
}
