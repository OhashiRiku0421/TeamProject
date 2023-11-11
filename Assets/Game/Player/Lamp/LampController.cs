using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LampController : MonoBehaviour
{
    private Transform _target;
    
    private void Update()
    {
        transform.position = Vector3.Lerp(_target.position, transform.position, Time.deltaTime);
    }
}
