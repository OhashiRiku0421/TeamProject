using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HirotoPla : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject Text;
    public GameObject grass;

    public GameObject Slider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(KeyF());
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "grass")
        {
            Text.gameObject.SetActive(true); // TextÇï\é¶Ç∑ÇÈ
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "grass")
        {
            Text.gameObject.SetActive(false);

        }
    }

    IEnumerator KeyF()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Slider.gameObject.SetActive(true);

            Debug.Log("FÇâüÇ≥ÇÍÇΩÅIÅI");
            Destroy(grass.gameObject, 2f);

            yield return new WaitForSeconds(5f);
            Text.gameObject.SetActive(false);
            Slider.gameObject.SetActive(false);
            CriAudioManager.Instance.SE.Play("UI", "SE_Item_Get");
        }

    }
}
