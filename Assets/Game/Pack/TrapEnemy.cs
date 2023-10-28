using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("ó^Ç¶ÇÈÉ_ÉÅÅ[ÉW")]
    private int _damage = 10;

    public GameObject particleSystemPrefab; // Reference to the particle system prefab
    private void OnTriggerEnter(Collider other)
    {
        //If the object has the "Player" tag then what ever has this code will get destroy effect.
        if (other.gameObject.TryGetComponent<IDamage>(out IDamage iDamage))
        {
            CriAudioManager.Instance.SE.Play("Player", "SE_Player_Attack_Close");

            iDamage.SendDamage(_damage);
            // Destroy the game object.
            //Destroy(other.gameObject);
            Destroy(gameObject);
            other.transform.position = Vector3.back;
            //When the player gets hit, or touches it pop up "Break" in Log.
            Debug.Log("Break");
        }
    }
    void OnDestroy()
    {
        // Instantiate or activate the particle system when the object is destroyed
        if (particleSystemPrefab != null)
        {
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            Debug.Log("play");
        }
    }
}