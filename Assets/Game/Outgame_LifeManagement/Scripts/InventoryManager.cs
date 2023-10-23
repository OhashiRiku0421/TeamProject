using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IInventory
{
    /// <summary>�w�������A�C�e���Ƃ��̌���ێ����Ă����f�B�N�V���i��</summary>
    private Dictionary<ItemData, int> _inventory = new Dictionary<ItemData, int>();

    /// <summary>
    /// �w�������A�C�e�����C���x���g���ɒǉ�����֐�
    /// </summary>
    /// <param name="item">�ǉ�����A�C�e��</param>
    public void AddItemToInventory(ItemData item) 
    {
        if (_inventory.ContainsKey(item)) _inventory[item]++;
        else _inventory.Add(item, 1);
    }

    /// <summary>
    /// �C���x���g���ɂ���A�C�e����ԕi����֐�
    /// </summary>
    /// <param name="item">�ԕi����A�C�e��</param>
    public void RemoveItemFromInventory(ItemData item) 
    {
        if (_inventory[item] > 0) _inventory[item]--;
    }
}
