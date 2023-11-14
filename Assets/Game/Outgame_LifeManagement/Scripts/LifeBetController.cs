using UnityEngine;
using UnityEngine.UI;

public class LifeBetController : MonoBehaviour
{
    [SerializeField, Tooltip("スライダー")]
    private Slider _betSlider = default;

    [SerializeField, Tooltip("スライダーの値に掛ける数")]
    private int _betLife = 50;

    /// <summary>体力の最大数を1000としてベットしたライフの残りをプレイヤーのライフにする</summary>
    public void UpdateLife()
    {
        Debug.Log($"BetLife : {1000 - (int)_betSlider.value * _betLife}");
        ExternalLifeManager.Life = 1000 - (int)_betSlider.value * _betLife;
    }
}
