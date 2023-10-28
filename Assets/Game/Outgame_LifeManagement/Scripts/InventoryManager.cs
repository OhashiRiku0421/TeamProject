using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IInventory
{
    [SerializeField, Tooltip("プレイヤーのライフを保持・設定するための変数")]
    private static int _life = 100;

    /// <summary>プレイヤーのライフを受け取るためのプロパティ</summary>
    public static int Life { get => _life; }

    /// <summary>購入したアイテムとその個数を保持しておくディクショナリ</summary>
    private Dictionary<ItemData, int> _inventory = new Dictionary<ItemData, int>();

    /// <summary>インベントリを外部から操作するためのプロパティ</summary>
    public Dictionary<ItemData, int> Inventory { get => _inventory; set => _inventory = value; }

    /// <summary>
    /// 購入したアイテムをインベントリに追加する関数
    /// </summary>
    /// <param name="item">追加するアイテム</param>
    public void AddItemToInventory(ItemData item) 
    {
        if (_inventory.ContainsKey(item)) _inventory[item]++;
        else _inventory.Add(item, 1);
    }

    /// <summary>
    /// インベントリからアイテムを取り除く関数
    /// </summary>
    /// <param name="item">取り除くアイテム</param>
    public void RemoveItemFromInventory(ItemData item) 
    {
        if (_inventory[item] > 0) _inventory[item]--;
    }

    //public static int GetArmourValue()
    //{
        
    //}
}
