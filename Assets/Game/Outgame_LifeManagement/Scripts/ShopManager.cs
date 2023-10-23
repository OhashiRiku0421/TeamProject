using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField,Tooltip("テスト用のライフ")]
    private int _tempHP = 100;

    [SerializeField, Tooltip("ItemDataAsset をアサインする変数")]
    private ItemDataAsset _itemDataAsset;

    /// <summary>ItemDataAsset内のアイテムデータを格納する変数</summary>
    private List<ItemData> _itemDatas;

    private void Start()
    {
        _itemDatas = _itemDataAsset.ItemDatas;
    }

    public void BuyItem (string itemname)
    {
        var itemParam = _itemDatas.Find(item => item.Name == itemname);

        if (itemParam != null && _tempHP > itemParam.Cost)
        {
            var inventory = FindAnyObjectByType<InventoryManager>();
            inventory.AddItemToInventory(itemParam);
        }
    }
}
