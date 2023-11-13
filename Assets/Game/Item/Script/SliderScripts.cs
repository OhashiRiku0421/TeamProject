using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderScripts : MonoBehaviour
{
    [SerializeField] private InGameSystem _gameSystem = default;
    
    [SerializeField, Tooltip("����ɂ����鎞��")]
    private float _collectTime = 2F;

    public Image _geji = null;
    public GameObject _pcone = null;

    public event System.Action ItemGetCallback = default;
    
    float max = 1f;
    float min = 0.1f;

    private float cup = 0.0f;
    /// <summary>�o�ߎ���</summary>
    private float _elapsed = 0.0F;

    private bool _isCollecting = false;

    private void Start()
    {
        _geji.fillAmount = 1f;
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
            _geji.fillAmount = 1 - (_elapsed / _collectTime);

            if (_elapsed > _collectTime)
            {
                _gameSystem.ScoreSystem.GetItem();
                
                ItemGetCallback?.Invoke();
                CriAudioManager.Instance.SE.Play("SE", "SE_System_Item_Get");
                
                Destroy(this.gameObject);
                gameObject.SetActive(false);
            }
        }
        else
        {
            _elapsed = 0F;
        }
    }
}
