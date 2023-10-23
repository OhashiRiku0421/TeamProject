using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField,Tooltip("�e�X�g�p�̃��C�t")]
    private int _tempHP = 100;

    [SerializeField, Tooltip("ItemDataAsset ���A�T�C������ϐ�")]
    private ItemDataAsset _itemDataAsset;

    /// <summary>ItemDataAsset���̃A�C�e���f�[�^���i�[����ϐ�</summary>
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
