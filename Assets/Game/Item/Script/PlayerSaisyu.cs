using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaisyu : MonoBehaviour
{
    public GameObject saisyuCanvas;

    public GameObject pcone;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "pCone")
        {
            saisyuCanvas.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "pCone")
        {
            saisyuCanvas.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(saisyu());
        }
    }
    IEnumerator saisyu()
    {
        yield return new WaitForSeconds(2f);
        // QbγΙ»πΑ·Qb
        Destroy(pcone.gameObject);
        // QbγΙΜζΜCanvasπρ\¦Ι·ι
        saisyuCanvas.gameObject.SetActive(false);
    }
}