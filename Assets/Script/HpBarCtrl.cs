using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarCtrl : MonoBehaviour
{
    private Slider _slider;

    void Start()
    {
        // �X���C�_�[���擾����
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    public float  _hp = 0f;

    void Update()
    {
        _hp += 0.1f;
        // HP�Q�[�W�ɒl��ݒ�
        _slider.value = _hp;

    }
}
