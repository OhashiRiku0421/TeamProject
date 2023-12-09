using UnityEngine;

public class ExternalLifeManager
{
    private static int _totalLife;
    /// <summary>ゲーム全体で使用するライフ</summary>
    public static int TotalLife { get => _totalLife; set => _totalLife += value; }
    /// <summary>シインゲームで使用するライフ</summary>
    public static int Life { get; set; }
}
