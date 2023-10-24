using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField,Tooltip("�e�X�g�p�̃��C�t")]
    private int _tempHP = 100;

    /// <summary>�v���C���[�̃��C�t���󂯎�邽�߂̃v���p�e�B</summary>
    public int PlayerLife { get => _tempHP; set => _tempHP = value; }

    [SerializeField, Tooltip("ItemDataAsset ���A�T�C������ϐ�")]
    private ItemDataAsset _itemDataAsset;

    /// <summary>ItemDataAsset���̃A�C�e���f�[�^���i�[����ϐ�</summary>
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
            Debug.Log($"�A�C�e���������Ă��܂��� {_inventory.Inventory[itemParam]}");
        }
    }
}
