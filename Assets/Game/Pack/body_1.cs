using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_1 : MonoBehaviour
{
    public GameObject parentObject;
    public float moveSpeed = 2f;
    private bool movingToParent = true;
    private int ignoreLayer; // New variable to store the layer to ignore
    private int _damage = 10;

    void Start()
    {
        // Store the layer to ignore
        ignoreLayer = LayerMask.NameToLayer("IgnoreCollision");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parent"))
        {
            Debug.Log("hit");
            // Check if the collider's layer is not the one to ignore
            if (other.gameObject.layer != ignoreLayer)
            {
                movingToParent = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Parent"))
        {
            Debug.Log("leave");
            // Check if the collider's layer is not the one to ignore
            if (other.gameObject.layer != ignoreLayer)
            {
                movingToParent = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (movingToParent)
        {
            transform.position = Vector3.MoveTowards(transform.position, parentObject.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}
