using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour, IInventory
{
    /// <summary>�w�������A�C�e���Ƃ��̌���ێ����Ă����f�B�N�V���i��</summary>
    private Dictionary<ItemData, int> _inventory = new Dictionary<ItemData, int>();

    /// <summary>�C���x���g�����O�����瑀�삷�邽�߂̃v���p�e�B</summary>
    public Dictionary<ItemData, int> Inventory { get => _inventory; set => _inventory = value; }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;

        Debug.Log(_inventory.Count);
    }

    /// <summary>
    /// �J�ڐ�̃V�[���ɃC���x���g�����쐬����֐�
    /// </summary>
    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        //  �J�ڐ�ɃC���x���g�������݂��Ȃ��ꍇ�V�����쐬���āA�C���x���g�����R�s�[����
        if (!FindAnyObjectByType<InventoryManager>())
        {
            var obj = new GameObject("Inventory").AddComponent<InventoryManager>();
            obj.GetComponent<InventoryManager>().Inventory = this._inventory;
        }
        else�@// �C���x���g��������ꍇ�A�C���x���g���̃R�s�[�̂ݍs��
        {
            var obj = FindAnyObjectByType<InventoryManager>();
            obj.Inventory = this._inventory;
        }

        SceneManager.sceneLoaded -= SceneLoaded;
    }

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
    /// �C���x���g������A�C�e������菜���֐�
    /// </summary>
    /// <param name="item">��菜���A�C�e��</param>
    public void RemoveItemFromInventory(ItemData item) 
    {
        if (_inventory[item] > 0) _inventory[item]--;
    }

    /// <summary>
    /// �A�C�e�����g�p����Ƃ��ɌĂяo���֐�
    /// ����Ăяo�����@�ƒ��g���Ȃ�
    /// </summary>
    /// <param name="item">�g�p����A�C�e���̃f�[�^</param>
    public void UseItem(ItemData item)
    {
        
    }
}
