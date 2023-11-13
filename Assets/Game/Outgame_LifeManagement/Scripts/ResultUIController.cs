using UnityEngine;
using UnityEngine.UI;

public class ResultUIController : MonoBehaviour
{
    [SerializeField, Tooltip("手に入れたライフの値を表示するテキスト")]
    private Text _getLifeValueText = null;

    [SerializeField, Tooltip("失ったライフの値を表示するテキスト")]
    private Text _lostLifeValueText = null;

    [SerializeField, Tooltip("ボーナスライフの値を表示するテキスト")]
    private Text _bonusLifeValueText = null;

    [SerializeField, Tooltip("現在のライフの値を表示するテキスト")]
    private Text _nowLifeValueText = null;

    [SerializeField, Tooltip("リザルトマネージャー")]
    private ResultManager _resultManager = null;

    void Start()
    {
        _getLifeValueText.text = _resultManager.GetLife.ToString();
        _lostLifeValueText.text = _resultManager.LostLife.ToString();
        _bonusLifeValueText.text = _resultManager.BonusLife.ToString();
        _nowLifeValueText.text = _resultManager.CurrentLife.ToString();
    }
}
