using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeBetSliderController : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("スライダー")]
    private Slider _betSlider = default;

    [SerializeField, Tooltip("スライダーの値に掛ける数")]
    private int _betLife = 50;

    [SerializeField, Tooltip("プレイヤーの最大ライフ値")]
    private int _maxPlayerLife = 1000;

    [SerializeField]
    private ScreenFader _screenFader = null;

    private void Start()
    {
        _screenFader.FadeOut(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        UpdateLife();
    }
    /// <summary>ベットしたライフの残りをプレイヤーのライフに設定し、残りをインゲームシステムに渡す</summary>
    private void UpdateLife()
    {
        int playerLife = (int)_betSlider.value * _betLife;
        int betLife = _maxPlayerLife - playerLife;
        ExternalLifeManager.Life = playerLife;
        ScoreSystem.SetLife(playerLife, betLife);

        Debug.Log($"PlayerLife : {playerLife} \n BetLife : {betLife}");
    }

    public void PlaySliderSE()
    {
        CriAudioManager.Instance.SE.Play("SE", "SE_System_Slider");
    }

    public void Pause()
    {
        _betSlider.interactable = false;
    }

    public void Resume()
    {
        _betSlider.interactable = true;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
