using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField]
    EnemyController _enemyController;

    private void OnAttack()
    {
        _enemyController.EnemyAttack.AttackAsync();
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private void PlayLongMoveSE()
    {
        CriAudioManager.Instance.SE.Play3D(transform.position, "SE", "SE_Enemy02_Footstep_01");
        //CriAudioManager.Instance.SE.Play3D(transform.position,"SE", "SE_Enemy02_Footstep_02");
    }

    private void PlayShortMoveSE()
    {
        CriAudioManager.Instance.SE.Play3D(transform.position, "SE", "SE_Enemy01_Footstep_01");
        //CriAudioManager.Instance.SE.Play3D(transform.position,"SE", "SE_Enemy01_Footstep_02");
    }
}
