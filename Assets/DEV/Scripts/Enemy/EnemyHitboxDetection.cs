using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.BowAndArrow;
using DEV.Scripts.Enemy;
using UnityEngine;

public class EnemyHitboxDetection : MonoBehaviour
{

    private EnemyBase parentScript;
    private void Awake()
    {
        parentScript = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerArrowBase arrowScript = other.gameObject.GetComponentInParent<PlayerArrowBase>();
        if (arrowScript && other.isTrigger)
        {
            if (arrowScript.ArrowCanDoDamage && !arrowScript.EnemiesThatHaveBeenHit.Contains(parentScript.gameObject))
            {
                parentScript.CollideWithArrow(other);
                arrowScript.EnemiesThatHaveBeenHit.Add(parentScript.gameObject);
            }
        }
    }
}
