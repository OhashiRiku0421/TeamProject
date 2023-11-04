using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderScripts : MonoBehaviour
{
    public Image geji;
    

    float max = 1f;
    float min = 0.1f;

    private float cup = 0.0f;



    private void Start()
    {
        geji.fillAmount = 100f;
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("“–‚½‚Á‚½");

           
            InvokeRepeating(nameof(Slider), 0f, 0.1f);
        }
        //CriAudioManager.Instance.SE.Play("UI", "SE_Item_Get");
        //if (geji.fillAmount == 0f)
        //{
        //    CriAudioManager.Instance.SE.Play("UI", "SE_Item_Get");
        //}
    }
    
    public void Slider()
    {
        geji.fillAmount -= 1f * Time.deltaTime;
        
        
    }
}
