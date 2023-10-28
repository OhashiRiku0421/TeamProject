using UnityEngine;

public class DestroyOb : MonoBehaviour
{
    public GameObject particleSystemPrefab; // Reference to the particle system prefab

    void OnDestroy()
    {
        // Instantiate or activate the particle system when the object is destroyed
        if (particleSystemPrefab != null)
        {
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
        }
    }
    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }*/
}
