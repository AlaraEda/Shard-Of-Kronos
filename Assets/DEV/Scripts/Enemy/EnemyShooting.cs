/*
 * Child class of the EnemyBase.cs class. It overrides the attack function for a shooting enemy.
 * It also handles damaging the player if a projectile hits the player.
 * It also has a custom delete function, to remove projectile before the enemy is removed.
 *
 * Made by: Mats.
 */

using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Enemy;
using DEV.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

public class EnemyShooting : EnemyBase
{
    // Privately used fields
    private Transform projectileSpawnLocation;
    private List<GameObject> spawnedProjectiles = new List<GameObject>();
    private CheatDebugController cheatDebugController;

    // Public property that defines the speed of the projectile (set by the child variant of this class, EnemyShootingFast.cs)
    public float ShotSpeed { get; set; }


    /// <summary>
    /// Base awake and settings the location from where to spawn the projectile.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        cheatDebugController = SceneContext.Instance.cheatDebugController;
        projectileSpawnLocation = transform.GetChild(2).transform;
    }

    /// <summary>
    /// The overriden attack method for shooting enemies. This method instantiates a projectile and starts a coroutine to move it towards the player.
    /// </summary>
    protected override void Attack()
    {
        base.Attack();
        GameObject bulletSpawn = Instantiate(EnemySettingsModel.enemyShootingProjectilePrefab,
            projectileSpawnLocation.position, Quaternion.identity, SceneContext.Instance.objectsSpawnedByPlayer);
        var script = bulletSpawn.GetComponent<EnemyShootingProjectile>();
        spawnedProjectiles.Add(bulletSpawn);
        script.SetEnemyThatShotProjectile(this);
        var playPos = PlayerTransform.position + new Vector3(0, 1.5f, 0);
        var dist = Vector3.Distance(playPos, bulletSpawn.transform.position);
        var time = dist / ShotSpeed;
        StartCoroutine(MoveProjectile(bulletSpawn, playPos, time, 10));
    }

    /// <summary>
    /// Coroutine that is called by the Attack() method. This coroutine moves the projectile to the location of the player.
    /// It also rotates the projectile towards the player.
    /// </summary>
    /// <param name="projectile">The projectile GameObject to move.</param>
    /// <param name="plPos">The position of the player.</param>
    /// <param name="ti">Calculated time needed to reach the target.</param>
    /// <param name="extraDistanceAfterMiss">How far the projectile should travel forward if it missed the player.</param>
    /// <returns></returns>
    protected IEnumerator MoveProjectile(GameObject projectile, Vector3 plPos, float ti, float extraDistanceAfterMiss)
    {
        transform.LookAt(plPos);
        var script = projectile.GetComponent<EnemyShootingProjectile>();
        var dir = (projectile.transform.position - plPos).normalized;
        Tween move = projectile.transform.DOMove(plPos, ti).SetEase(Ease.Linear);
        yield return move.WaitForCompletion();
        script.SetExtraMoveHasStarted(true);
        var time = extraDistanceAfterMiss / ShotSpeed;
    }

    /// <summary>
    /// Removes health from the player if an projectile hit the player.
    /// </summary>
    public void DamagePlayer()
    {
        if (!cheatDebugController.godModeActive)
        {
            PlayerController.PlayerHealth -= AttackDamage;
        }
    }

    /// <summary>
    /// Overriden DeleteEnemy() method, to remove the projectiles that are currently in the air, before the enemy dies.
    /// It then calls the base.DeleteEnemy() method to delete the enemy.
    /// </summary>
    protected override void DeleteEnemy()
    {
        foreach (var obj in spawnedProjectiles)
        {
            var script = obj.GetComponent<EnemyShootingProjectile>();
            script.SetEnemyThatShotProjectile(null);
            Destroy(obj);
        }

        base.DeleteEnemy();
    }
}