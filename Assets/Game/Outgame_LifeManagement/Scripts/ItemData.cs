using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/CreateItemDataAsset")]
public class ItemDataAsset : ScriptableObject
{
    [SerializeField, Tooltip("アイテム作成用のリスト")]
    private List<ItemData> _itemDatas = new List<ItemData>();

    /// <summary>アイテムデータ参照用のプロパティ</summary>
    public List<ItemData> ItemDatas { get => _itemDatas; }
}

[System.Serializable]
public class ItemData
{
    [SerializeField, Tooltip("アイテムの名前")]
    private string _name = default;

    [SerializeField, Tooltip("アイテム使用時の効果量")]
    private int _effectValue = default;

    [SerializeField, Tooltip("アイテム購入時の値段")]
    private int _cost = default;

    [SerializeField, Tooltip("アイテム効果の発動タイミング")]
    private EffectType _effect = EffectType.None;

    public string Name { get => _name; }
    public int EffectValue { get => _effectValue; }
    public int Cost { get => _cost; }
    public EffectType Effect { get => _effect;}
}

public enum EffectType
{
    /// <summary>効果なし</summary>
    None,
    /// <summary>常時発動</summary>
    Passive,
    /// <summary>任意発動</summary>
    Active,
}
