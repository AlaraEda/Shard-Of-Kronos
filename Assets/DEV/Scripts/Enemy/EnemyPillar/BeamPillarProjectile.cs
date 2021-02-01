using System;
using DEV.Scripts.Managers;
using UnityEngine;

namespace DEV.Scripts.EnemyPillar
{
    public class BeamPillarProjectile : MonoBehaviour
    {
        
        private float projectileSpeed;
        private float projectileDamage;
        private Vector3 targetLoc = Vector3.zero;
        private bool hasHitPlayer;
        private PlayerController playerController;
        private CheatDebugController cheatDebugController;

        private void Awake()
        {
            playerController = SceneContext.Instance.playerManager;
            cheatDebugController = SceneContext.Instance.cheatDebugController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ForceReceiver>() && !hasHitPlayer)
            {
                if (!cheatDebugController.godModeActive)
                {
                    playerController.PlayerHealth -= projectileDamage;
                }

                hasHitPlayer = true;
                Destroy(gameObject);
            }
        }

        public void SetLocationSpeedAndDamage(Vector3 loc, float speed, float dmg)
        {
            targetLoc = loc;
            projectileSpeed = speed;
            projectileDamage = dmg;
        }

        private void Update()
        {
            if (targetLoc != Vector3.zero)
            {
                
                gameObject.transform.position =
                    Vector3.MoveTowards(transform.position, targetLoc, projectileSpeed * Time.deltaTime);
            }

            if (transform.position == targetLoc)
            {
                Destroy(gameObject);
            }
        }
    }
}
