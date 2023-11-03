using UnityEngine;
using UnityEngine.UI;

public class LifeBetController : MonoBehaviour
{
    [SerializeField]
    private Slider _betSlider = default;

    [SerializeField]
    private int _minBetLife = 50;

    [SerializeField]
    private int _maxBetLife = 1000;

    void Start()
    {
        _betSlider.minValue = _minBetLife;
        _betSlider.maxValue = _maxBetLife;
    }

    public void UpdateLife()
    {
        ExternalLifeManager.Life = (int)_betSlider.value;
    }
}
