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
}
