using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeUI : MonoBehaviour
{
    [Tooltip("ライフの残量を表示するテキスト")]
    [SerializeField]
    private Text _lifeNumText;

    public void Intialized()
    {
        _lifeNumText.text = "0000";
    }

    /// <summary>
    /// ライフの残量を表示する
    /// </summary>
    public void SetLifeView(float count) // TODO：intに変更する
    {
        _lifeNumText.text = count.ToString("00");
    }
}
