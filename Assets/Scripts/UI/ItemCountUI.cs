using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class ItemCountUI : MonoBehaviour
{
    [Tooltip("アイテムの所持数を表示するテキスト")] 
    [SerializeField]
    private Text _itemCountText;

    public void Intialized()
    {
        _itemCountText.text = "00";
    }

    /// <summary>
    /// アイテムの所持数を表示する
    /// </summary>
    public void SetItemCountView(int count)
    {
        _itemCountText.text = count.ToString("00");
    }
}
