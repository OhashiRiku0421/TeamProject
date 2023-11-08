using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderScripts : MonoBehaviour
{
    public Image _geji = null;
    public GameObject _pcone = null;

    float max = 1f;
    float min = 0.1f;

    private float cup = 0.0f;

    private void Start()
    {
        _geji.fillAmount = 100f;
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("“–‚½‚Á‚½");

            _geji.fillAmount -= 1f * Time.deltaTime;

            if(_geji.fillAmount == 0f)
            {
                Destroy(_pcone.gameObject);
                gameObject.SetActive(false);

                CriAudioManager.Instance.SE.Play("UI", "SE_Item_Get");
            }
        } 
    }
}
