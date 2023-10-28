using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private int treesocore = 5000;
    private int hp;

    public float speed = 10;
    public float jumpPower;

    public GameObject Text;
    public GameObject Text1;

    public GameObject grass;
    public GameObject tree;

    public GameObject Slider;

    public Text TreeScore;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        slider.value = 1;
        hp = treesocore;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(transform.forward * speed * -1, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(transform.right * speed, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(transform.right * speed * -1 , ForceMode.Acceleration);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.up * jumpPower;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            treesocore -= 10;
            hp = hp - 100;
            TreeScore.text = string.Format("{0}", treesocore);
            slider.value = (float)hp / (float)treesocore; ;

            if(treesocore == 0)
            {
                CriAudioManager.Instance.SE.Play("UI", "SE_Item_Get");
            }
        }
 
        StartCoroutine("KeyF");
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if(collision .gameObject.tag == "grass")
        {
            Text.gameObject.SetActive(true); // TextÇï\é¶Ç∑ÇÈ
        }
        if(collision .gameObject.tag == "tree")
        {
            Text1.gameObject.SetActive(true);
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision .gameObject.tag == "grass")
        {
            Text.gameObject.SetActive(false);
            
        }
        if(collision.gameObject.tag == "tree")
        {
            Text1.gameObject.SetActive(false) ;
        }
    }

    IEnumerator KeyF()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Slider.gameObject.SetActive(true);

            Debug.Log("FÇâüÇ≥ÇÍÇΩÅIÅI");
            Destroy(grass.gameObject, 4f);
        
            yield return new WaitForSeconds(4f);
            Text.gameObject.SetActive(false);
            Slider.gameObject.SetActive(false);
            CriAudioManager.Instance.SE.Play("UI", "SE_Item_Get");
        }
       
    }
    
    
}
