using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    

    //respawn
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "respawn")
        {
            Debug.Log("atatta");
            transform.position = new Vector3(0, 0.5f, -28f);
            
        }
    }
}
