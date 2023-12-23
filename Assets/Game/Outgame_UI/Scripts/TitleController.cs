using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    private void Start()
    {
        TimerSystem.Timer = 1200;
        ExternalLifeManager.TotalLife = 0;
        ExternalLifeManager.Life = 0;

    }
}
