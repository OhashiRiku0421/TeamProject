using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    /// <summary>プレイヤーのライフ</summary>
    private int _playerLife = InventoryManager.Life;

    [SerializeField, Tooltip("ItemDataAsset をアサインする変数")]
    private ItemDataAsset _itemDataAsset;

    /// <summary>ItemDataAsset内のアイテムデータを格納する変数</summary>
    private List<ItemData> _itemDatas;

    [SerializeField, Tooltip("インベントリ")]
    private InventoryManager _inventory;

    private void Start()
    {
        _itemDatas = _itemDataAsset.ItemDatas;
    }

    public void BuyItem (string itemName)
    {
        var itemParam = _itemDatas.Find(item => item.Name == itemName);

        if (itemParam != null && _playerLife >= itemParam.Cost)
        {
            _inventory.AddItemToInventory(itemParam);
            _playerLife -= itemParam.Cost;

            Debug.Log($"{itemParam.Name} {_inventory.Inventory[itemParam]}");
        }
        else if (itemParam == null)
        {
            Debug.Log("アイテムの名前が間違っているか、存在しません");
        }

        CriAudioManager.Instance.SE.Play("UI", "SE_Buy");
    }

    public void SellItem (string itemName)
    {
        var itemParam = _itemDatas.Find(item => item.Name == itemName);

        if (itemParam != null && _inventory.Inventory[itemParam] > 0)
        {
            _inventory.RemoveItemFromInventory(itemParam);
            _playerLife += itemParam.Cost;

            Debug.Log($"{itemParam.Name} {_inventory.Inventory[itemParam]}");
        }
        else if (_inventory.Inventory[itemParam] <= 0)
        {
            Debug.Log($"アイテムを持っていません {_inventory.Inventory[itemParam]}");
        }
        else if (itemParam == null)
        {
            Debug.Log("アイテムの名前が間違っているか、存在しません");
        }

        CriAudioManager.Instance.SE.Play("UI", "SE_Cancel");
    }
}
