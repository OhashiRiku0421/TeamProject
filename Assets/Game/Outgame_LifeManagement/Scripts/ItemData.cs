using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/CreateItemDataAsset")]
public class ItemDataAsset : ScriptableObject
{
    [SerializeField, Tooltip("�A�C�e���쐬�p�̃��X�g")]
    private List<ItemData> _itemDatas = new List<ItemData>();

    /// <summary>�A�C�e���f�[�^�Q�Ɨp�̃v���p�e�B</summary>
    public List<ItemData> ItemDatas { get => _itemDatas; }
}

[System.Serializable]
public class ItemData
{
    [SerializeField, Tooltip("�A�C�e���̖��O")]
    private string _name = default;

    [SerializeField, Tooltip("�A�C�e���g�p���̌��ʗ�")]
    private int _effectValue = default;

    [SerializeField, Tooltip("�A�C�e���w�����̒l�i")]
    private int _cost = default;

    [SerializeField, Tooltip("�A�C�e�����ʂ̔����^�C�~���O")]
    private EffectType _effect = EffectType.None;

    public string Name { get => _name; }
    public int EffectValue { get => _effectValue; }
    public int Cost { get => _cost; }
    public EffectType Effect { get => _effect;}
}

public enum EffectType
{
    /// <summary>���ʂȂ�</summary>
    None,
    /// <summary>�펞����</summary>
    Passive,
    /// <summary>�C�Ӕ���</summary>
    Active,
}
