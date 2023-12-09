using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPause : MonoBehaviour
{
    public void Pause()
    {
        PauseSystem.Instance.Pause();
    }

    public void Resume()
    {
        PauseSystem.Instance.Resume();
    }
}
