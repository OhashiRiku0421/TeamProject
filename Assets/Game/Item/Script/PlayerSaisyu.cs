using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaisyu : MonoBehaviour
{
    public GameObject _saisyuCanvas;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "pCone")
        {
            _saisyuCanvas.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "pCone")
        {
            _saisyuCanvas.SetActive(false);
        }
    }
}