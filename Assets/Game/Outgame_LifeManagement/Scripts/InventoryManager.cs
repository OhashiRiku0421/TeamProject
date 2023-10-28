using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーのライフを保持・設定するための変数")]
    private static int _life = 100;

    /// <summary>プレイヤーのライフを受け取るためのプロパティ</summary>
    public static int Life { get => _life; }
}
