using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField, Tooltip("任意のキーを入力したらシーンが変わるようにするフラグ")]
    private bool _isAnyKeyPressed = false;

    [SerializeField, Tooltip("遷移したいシーンのシーンアセット")]
    private SceneAsset _sceneAsset;

    [SerializeField, Tooltip("シーンが遷移するまでの時間")]
    private float _waitTime = 0.666f; // セレクトSEがなり終わるまでの時間を初期値として設定している

    private InputAction _anyKeyAction;

    private void Awake()
    {
        if (_isAnyKeyPressed)
        {
            _anyKeyAction = new InputAction("AnyKey", InputActionType.Button);
            _anyKeyAction.AddBinding("<mouse>/press");
            _anyKeyAction.AddBinding("<Keyboard>/anyKey");
            _anyKeyAction.performed += OnAnyKeyPressed;
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
        CriAudioManager.Instance.SE.Play("SE", "SE_System_Select");
    }

    private void OnAnyKeyPressed(InputAction.CallbackContext callback)
    {
        SceneLoaded();
    }
    private void OnEnable()
    {
        if (!_isAnyKeyPressed) return;
        _anyKeyAction.Enable();
    }

    private void OnDisable()
    {
        if (!_isAnyKeyPressed) return;
        _anyKeyAction.Disable();
    }
}
