using UnityEngine;

public class ExternalLifeManager : MonoBehaviour
{
    /// <summary>プレイヤーのライフを保持しておく変数</summary>
    private static int _life = 50;

    /// <summary>シーン間でライフの受け渡しを行うためのプロパティ</summary>
    public static int Life { get => _life; set => _life = value; }
}
