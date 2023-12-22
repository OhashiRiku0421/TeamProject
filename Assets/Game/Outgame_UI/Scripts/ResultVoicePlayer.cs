using System.Collections;
using UnityEngine;
using System;

public class ResultVoicePlayer : MonoBehaviour
{
    public  void PlayVoice(ClearState clearState)
    {
        StartCoroutine(Enumerator(clearState));
    }

    IEnumerator Enumerator(ClearState clearState)
    {
        switch (clearState)
        {
            case ClearState.None:
                throw new ArgumentException($"ClearState���ݒ肳��Ă��܂���BClearState:{clearState}");
            case ClearState.Normal:
                CriAudioManager.Instance.SE.Play("VOICE", "VC_Player_Ingame_Clear_01");
                yield return new WaitForSeconds(1.165f);
                CriAudioManager.Instance.SE.Play("VOICE", "VC_Robe_Ingame_Clear_01");
                break;
            case ClearState.Near:
                CriAudioManager.Instance.SE.Play("VOICE", "VC_Player_Ingame_Clear_02");
                yield return new WaitForSeconds(3.153f);
                CriAudioManager.Instance.SE.Play("VOICE", "VC_Robe_Ingame_Clear_02");
                break;
            case ClearState.Clear:
                CriAudioManager.Instance.SE.Play("VOICE", "VC_Player_Ingame_Clear_03");
                yield return new WaitForSeconds(2.996f);
                CriAudioManager.Instance.SE.Play("VOICE", "VC_Robe_Ingame_Clear_03");
                break;
        }
    }
}

public enum ClearState
{
    None,
    Normal,
    Near,
    Clear,
    Over
}
