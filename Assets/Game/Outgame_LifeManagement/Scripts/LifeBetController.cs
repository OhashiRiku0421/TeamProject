using UnityEngine;
using UnityEngine.UI;

public class LifeBetController : MonoBehaviour
{
    [SerializeField, Tooltip("スライダー")]
    private Slider _betSlider = default;

    [SerializeField, Tooltip("スライダーの値に掛ける数")]
    private int _betLife = 50;

    public void UpdateLife()
    {
        Debug.Log($"BetLife : {(int)_betSlider.value * _betLife}");
        ExternalLifeManager.Life = (int)_betSlider.value * _betLife;
    }
}
