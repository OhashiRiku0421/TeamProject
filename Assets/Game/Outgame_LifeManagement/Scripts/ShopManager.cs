using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField,Tooltip("テスト用のライフ")]
    private int _tempHP = 100;

    /// <summary>プレイヤーのライフを受け取るためのプロパティ</summary>
    public int PlayerLife { get => _tempHP; set => _tempHP = value; }

    [SerializeField, Tooltip("ItemDataAsset をアサインする変数")]
    private ItemDataAsset _itemDataAsset;

    /// <summary>ItemDataAsset内のアイテムデータを格納する変数</summary>
    private List<ItemData> _itemDatas;

    private InventoryManager _inventory;

    private void Start()
    {
        _itemDatas = _itemDataAsset.ItemDatas;
        _inventory = FindAnyObjectByType<InventoryManager>();
    }

    public void BuyItem (string itemname)
    {
        var itemParam = _itemDatas.Find(item => item.Name == itemname);

        if (itemParam != null && _tempHP >= itemParam.Cost)
        {
            _inventory.AddItemToInventory(itemParam);
            _tempHP -= itemParam.Cost;

            Debug.Log(_inventory.Inventory[itemParam]);
        }
    }

    public void SellItem (string itemname)
    {
        var itemParam = _itemDatas.Find(item => item.Name == itemname);

        if (_inventory.Inventory[itemParam] > 0)
        {
            _inventory.RemoveItemFromInventory(itemParam);
            _tempHP += itemParam.Cost;

            Debug.Log(_inventory.Inventory[itemParam]);
        }
        else
        {
            Debug.Log($"アイテムを持っていません {_inventory.Inventory[itemParam]}");
        }
    }
}
