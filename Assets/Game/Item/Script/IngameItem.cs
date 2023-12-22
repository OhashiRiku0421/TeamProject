using UnityEngine;
using UnityEngine.UI;
using System;

public class IngameItem: MonoBehaviour
{
    private InGameSystem _gameSystem = default;

    [SerializeField, Tooltip("採取にかかる時間")]
    private float _collectTime = 2F;

    [SerializeField, Tooltip("ゲージのImage")]
    private Image _geji = null;

    public event Action ItemGetCallback = default;

    private float _elapsed = 0.0F;

    private bool _isCollecting = false;

    private void Start()
    {
        if (_geji != null)
            _geji.fillAmount = 1f;

        _gameSystem = FindObjectOfType<InGameSystem>();

        if (_gameSystem == null)
        {
            Debug.LogError("InGameSystemが見つかりませんでした");
        }
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

            //CriAudioManager.Instance.SE.Play("SE", "SE_System_Item_Get");

            if (_elapsed > _collectTime)
            {
                _gameSystem.ScoreSystem.GetItem();

                // TODO：アイテム取得時のSEをストップする

                Destroy(this.gameObject);
            }
        }
        else
        {
            _elapsed = 0F;
        }
    }
}
