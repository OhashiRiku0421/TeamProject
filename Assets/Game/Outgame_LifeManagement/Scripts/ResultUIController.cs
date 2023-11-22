using UnityEngine;
using UnityEngine.UI;

public class ResultUIController : MonoBehaviour
{
    private ScoreData scoreData = new();

    [SerializeField, Tooltip("手に入ったライフ")]
    private Text _getLifeText;

    [SerializeField, Tooltip("クリアした時間")]
    private Text _clearTimeText;

    void Start()
    {
        _getLifeText.text = scoreData.PLayerLife.ToString();
        _clearTimeText.text = scoreData.ClearTime.ToString();
    }
}
