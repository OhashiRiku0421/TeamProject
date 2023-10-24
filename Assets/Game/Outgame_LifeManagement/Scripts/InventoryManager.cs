using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour, IInventory
{
    /// <summary>購入したアイテムとその個数を保持しておくディクショナリ</summary>
    private Dictionary<ItemData, int> _inventory = new Dictionary<ItemData, int>();

    /// <summary>インベントリを外部から操作するためのプロパティ</summary>
    public Dictionary<ItemData, int> Inventory { get => _inventory; set => _inventory = value; }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;

        Debug.Log(_inventory.Count);
    }

    /// <summary>
    /// 遷移先のシーンにインベントリを作成する関数
    /// </summary>
    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        //  遷移先にインベントリが存在しない場合新しく作成して、インベントリをコピーする
        if (!FindAnyObjectByType<InventoryManager>())
        {
            var obj = new GameObject("Inventory").AddComponent<InventoryManager>();
            obj.GetComponent<InventoryManager>().Inventory = this._inventory;
        }
        else　// インベントリがある場合、インベントリのコピーのみ行う
        {
            var obj = FindAnyObjectByType<InventoryManager>();
            obj.Inventory = this._inventory;
        }

        SceneManager.sceneLoaded -= SceneLoaded;
    }

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

    /// <summary>
    /// アイテムを使用するときに呼び出す関数
    /// 現状呼び出す方法と中身がない
    /// </summary>
    /// <param name="item">使用するアイテムのデータ</param>
    public void UseItem(ItemData item)
    {
        
    }
}
