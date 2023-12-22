using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("任意のキーが入力されたらシーンが変わるようにするフラグ")]
    private bool _isAnyKeyPressed = false;

    [SerializeField, Tooltip("遷移したいシーンの名前")]
    private string _sceneName;

    [SerializeField, Tooltip("シーンが遷移するまでの時間")]
    private float _waitTime = 0.666f; // セレクトSEがなり終わるまでの時間を初期値として設定している

    private InputAction _anyKeyAction;
    private bool _isPause = false;

    [SerializeField, Tooltip("インゲームシーンの最小インデックス")]
    private int _min = 3;

    private void Awake()
    {
        if (_isAnyKeyPressed && !_isPause)
        {
            _anyKeyAction = new InputAction("AnyKey", InputActionType.Button);
            _anyKeyAction.AddBinding("<mouse>/press");
            _anyKeyAction.AddBinding("<mouse>/rightButton");
            _anyKeyAction.AddBinding("<Keyboard>/anyKey");
            _anyKeyAction.performed += OnAnyKeyPressed;
        }
    }

    public void SceneLoaded()
    {
        if (_isPause) return;
        Invoke("SceneLoad", _waitTime);
        PlaySE();
    }

    private void SceneLoad()
    {
        if (SceneManager.GetActiveScene().name == "LifeBetScene")
        {
            var r = Random.Range(_min, SceneManager.sceneCountInBuildSettings);
            SceneManager.LoadScene(r);
        }

        SceneManager.LoadScene(_sceneName);
    }

    private void PlaySE()
    {
        CriAudioManager.Instance.SE.Play("SE", "SE_System_Select");
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
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
