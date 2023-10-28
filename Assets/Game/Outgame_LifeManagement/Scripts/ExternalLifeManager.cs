using UnityEngine;
using UnityEngine.UI;

public class ExternalLifeManager : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーのライフを保持・設定するための変数")]
    private static int _life = 5000;

    [SerializeField, Tooltip("インプットフィールド")]
    private InputField _lifeInputField;

    /// <summary>シーン間でライフの受け渡しを行うためのプロパティ</summary>
    public static int Life { get => _life; set => _life = value; }

    public void InputLife()
    {
        _life = int.Parse(_lifeInputField.text);
    }
}
