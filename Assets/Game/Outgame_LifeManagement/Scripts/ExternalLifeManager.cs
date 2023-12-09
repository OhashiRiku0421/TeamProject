using UnityEngine;

public class ExternalLifeManager
{
    private static int _tortalLife;
    /// <summary>ゲーム全体で使用するライフ</summary>
    public static int TortalLife { get => _tortalLife; set => _tortalLife += value; }
    /// <summary>シインゲームで使用するライフ</summary>
    public static int Life { get; set; }
}
