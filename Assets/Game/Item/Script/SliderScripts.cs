using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderScripts : MonoBehaviour
{
    [SerializeField, Tooltip("‰ñŽû‚É‚©‚©‚éŽžŠÔ")]
    private float _collectTime = 2F;

    public Image _geji = null;
    public GameObject _pcone = null;

    float max = 1f;
    float min = 0.1f;

    private float cup = 0.0f;
    /// <summary>Œo‰ßŽžŠÔ</summary>
    private float _elapsed = 0.0F;

    private bool _isCollecting = false;

    private void Start()
    {
        _geji.fillAmount = 100f;
    }

    public void CollectStart()
    {
        _isCollecting = true;
    }

    public void CollectEnd()
    {
        _isCollecting = false;
    }
    
    private void Update()
    {
        if (_isCollecting)
        {

            _elapsed += Time.deltaTime;
            _geji.fillAmount = _elapsed / _collectTime;

            if (_elapsed > _collectTime)
            {
                Destroy(_pcone.gameObject);
                gameObject.SetActive(false);

                CriAudioManager.Instance.SE.Play("UI", "SE_Item_Get");
            }
        }
        else
        {
            _elapsed = 0F;
        }
    }
}
