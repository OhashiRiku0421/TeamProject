using UnityEngine;

public class BGMPlayer : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("BGMの名前")]
    private string _bgmName;

    private int _bgmIndex;
    void Start()
    {
        _bgmIndex = CriAudioManager.Instance.BGM.Play("BGM", _bgmName);
    }

    public void Pause()
    {
        CriAudioManager.Instance.BGM.Pause(_bgmIndex);
    }

    public void Resume()
    {
        CriAudioManager.Instance.BGM.Resume(_bgmIndex);
    }
}
