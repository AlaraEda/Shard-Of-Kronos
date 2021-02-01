using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Managers;
using UnityEngine;

public class EnemyShootingProjectile : MonoBehaviour
{
    private EnemyShooting enemyThatShotProjectile;

    private const int PlayerLayer = 10;
    private Transform playerTransform;

    private bool extraMoveHasStarted;
    private float moveTime;
    
    
    private void Awake()
    {
        playerTransform = SceneContext.Instance.playerTransform;
        transform.LookAt(playerTransform);
    }

    private void FixedUpdate()
    {
        if (extraMoveHasStarted)
        {
            
            transform.position += transform.forward * (Time.deltaTime * enemyThatShotProjectile.ShotSpeed);
            if (enemyThatShotProjectile != null)
            {
                if (Vector3.Distance(enemyThatShotProjectile.transform.position, transform.position) > 100)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Gravity>() == null) return;
        enemyThatShotProjectile.DamagePlayer();
        Destroy(gameObject);
    }

    public void SetEnemyThatShotProjectile(EnemyShooting set)
    {
        enemyThatShotProjectile = set;
    }

    public void SetExtraMoveHasStarted(bool set)
    {
        extraMoveHasStarted = set;
    }
}
