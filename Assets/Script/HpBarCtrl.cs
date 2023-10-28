using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarCtrl : MonoBehaviour
{
    private Slider _slider;

    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    public float  _hp = 0f;

    void Update()
    {
        _hp += 0.1f;
        // HPゲージに値を設定
        _slider.value = _hp;

    }
}
