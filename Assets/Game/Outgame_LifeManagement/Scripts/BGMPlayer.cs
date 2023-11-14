using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField]
    private string _bgmName;
    void Start()
    {
        CriAudioManager.Instance.BGM.Play("BGM", _bgmName);
    }

    public void PlaySliderSE()
    {
        CriAudioManager.Instance.SE.Play("SE", "SE_System_Slider");
    }
}
