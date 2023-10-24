using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IInventory
{
    /// <summary>購入したアイテムとその個数を保持しておくディクショナリ</summary>
    private Dictionary<ItemData, int> _inventory = new Dictionary<ItemData, int>();

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
    /// インベントリにあるアイテムを返品する関数
    /// </summary>
    /// <param name="item">返品するアイテム</param>
    public void RemoveItemFromInventory(ItemData item) 
    {
        if (_inventory[item] > 0) _inventory[item]--;
    }
}
