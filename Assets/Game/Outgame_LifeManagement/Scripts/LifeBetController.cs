using UnityEngine;
using UnityEngine.UI;

public class LifeBetController : MonoBehaviour
{
    [SerializeField, Tooltip("スライダー")]
    private Slider _betSlider = default;

    [SerializeField, Tooltip("スライダーの値に掛ける数")]
    private int _betLife = 50;

    [SerializeField, Tooltip("ライフの最大値")]
    private int _maxLife = 1000;

    /// <summary>
    /// 体力の最大数を1000としてベットしたライフの残りをプレイヤーのライフにし、プレイヤーとインゲームシステムに渡す</summary>
    public void UpdateLife()
    {
        int betLife = (int)_betSlider.value * _betLife;
        int playerLife = _maxLife - betLife;
        ExternalLifeManager.Life = playerLife;
        ScoreSystem.SetLife(playerLife, betLife);

        Debug.Log($"PlayerLife : {playerLife}");
        Debug.Log($"BetLife : {betLife}");
    }
}
