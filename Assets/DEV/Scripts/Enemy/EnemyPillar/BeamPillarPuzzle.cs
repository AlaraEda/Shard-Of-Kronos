using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DEV.Scripts.EnemyPillar;
using DEV.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

//The Beam Pillar puzzle is used at the end of level1. 
//The objective there is to shoot/kill all 4 beam pillers, 
//which gives you access to the door that leads to level 2. 
public class BeamPillarPuzzle : MonoBehaviour
{
    public bool pillarIsDead;
    public float playerDetectionRange = 40.0f;
    public float attackCooldownTime = 3.0f;
    public AudioSource enemyPillarSound;
    //Beam-Line
    public Vector3 lineStartOffset;
    public Vector3 lineEndOffset;
    public Material lineRendererMaterial;
    public Color lineRendererColor = new Color(1,1,1, .5f);
    public float lineRendererWidth = 0.1f;

    //Projectile
    public float projectileSpeed = 80;
    public float projectileDamage = 5;
    public GameObject projectilePrefab;

    //The object
    public Transform spawnedObjectParent;
    public GameObject orbParent;

    //Color
    public float colorChangeTime = 0.5f;
    public Color inactiveColor = Color.gray;
    public Color attackingColor = Color.red;
    public Color deadColor = Color.black;
    public GameObject fireRingParticlesParentObject;

    //Other
    private Transform playerTransform;
    private bool playerIsInRange;
    private bool cooldownHasStarted;
    private float curCooldown;
    private LineRenderer lr;
    private Vector3 lineStart;
    private MeshRenderer[] orbMeshRenderers;
    private Material orbMat;
    private int curColorIndex;
    private GameObject fireParticle1;
    private GameObject fireParticle2;

    //Default Set Up
    private void Awake()
    {
        playerTransform = SceneContext.Instance.playerTransform;
        curCooldown = attackCooldownTime;

        lineStart = transform.position + lineStartOffset;
        GameObject followPlayerLine = new GameObject();
        followPlayerLine.transform.parent = spawnedObjectParent;
        followPlayerLine.name = "SpawnedLine";
        followPlayerLine.transform.position = lineStart;
        followPlayerLine.AddComponent<LineRenderer>();
        lr = followPlayerLine.GetComponent<LineRenderer>();
        lr.material = lineRendererMaterial;
        lr.startColor = lineRendererColor;
        lr.endColor = lineRendererColor;
        lr.startWidth = lineRendererWidth;
        lr.endWidth = lineRendererWidth;
        lr.SetPosition(0, lineStart);
        orbMeshRenderers = orbParent.GetComponentsInChildren<MeshRenderer>();
        orbMat = orbMeshRenderers[0].material;
        fireParticle1 = fireRingParticlesParentObject.transform.GetChild(0).gameObject;
        fireParticle2 = fireRingParticlesParentObject.transform.GetChild(1).gameObject;
        fireParticle1.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        fireParticle2.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        
        curColorIndex = ChangeOrbColor(inactiveColor, colorChangeTime,0);

    }

    /// <Summary>
    /// Change the color of the orb 
    /// </Summary>
    private int ChangeOrbColor(Color newColor, float duration, int indexColor)
    {
        //Fire particles
        switch (indexColor)
        {
            case 0:
                SetScaleOfParticles(.7f, colorChangeTime);
                break;
            case 1:
                SetScaleOfParticles(1.2f, colorChangeTime);
                break;
            case 2:
                SetScaleOfParticles(0f, colorChangeTime);
                break;
        }

        //Loop through all OrbMeshes and slowly change color. 
        for (int i = 0; i < orbMeshRenderers.Length; i++)
        {
            orbMeshRenderers[i].material.DOColor(newColor, "_OrbColor", duration);
        }

        DynamicGI.UpdateEnvironment();
        return indexColor;
    }

    //Duration and scale of fire particles
    private void SetScaleOfParticles(float endValue, float duration)
    {
        fireParticle1.transform.DOScale(endValue, duration);
        fireParticle2.transform.DOScale(endValue, duration);
    }

    //Check if player is standing near a BeamPillar
    private void Update()
    {
        CheckIfPlayerInRange();
        if (!pillarIsDead && playerIsInRange)
        {
            TryToAttack();
        }
    }

    //Attack player when in close range. 
    private void TryToAttack()
    {
        if (playerIsInRange)
        {
            cooldownHasStarted = true;
        }

        if (!cooldownHasStarted) return;
        curCooldown -= Time.deltaTime;
        if (!(curCooldown <= 0.0f)) return;
        cooldownHasStarted = false;
        curCooldown = attackCooldownTime;
        BeamAttack();
    }

    //Update de draw line to player.
    private void LateUpdate()
    {
        DrawLineToPlayerIfInRange();
    }

    //Draw line to player if the player is in range and the pillar isn't dead. 
    private void DrawLineToPlayerIfInRange()
    {
        if (playerIsInRange && !pillarIsDead)
        {
            var lineEnd = playerTransform.position + lineEndOffset;
            lr.SetPosition(1, lineEnd);
        }
        else
        {
            lr.SetPosition(1, lineStart);
        }
    }

    //Check if the player is in the range of the BeamPillar. If he is in range and the pillar is not dead, change color to red. 
    //Else (if the player is in range and the pillar is dead) change the color of the pillar to grey. 
    private void CheckIfPlayerInRange()
    {
        playerIsInRange = Vector3.Distance(transform.position, playerTransform.position) < playerDetectionRange;
        if (playerIsInRange)
        {
            if (curColorIndex != 1 && !pillarIsDead)
            {
                //Change to red
                curColorIndex = ChangeOrbColor(attackingColor, colorChangeTime, 1);
            }
        }
        else
        {
            if (curColorIndex != 0 && !pillarIsDead)
            {
                //Change to grey
                curColorIndex = ChangeOrbColor(inactiveColor, colorChangeTime, 0);
            }
        }
    }

    //Attack Beam, attacks player and does damage to player health. 
    private void BeamAttack()
    {
        var end = playerTransform.position + lineEndOffset;
        var projectile = Instantiate(projectilePrefab, lineStart, Quaternion.identity, spawnedObjectParent);
        projectile.transform.LookAt(end);
        var projScript = projectile.GetComponent<BeamPillarProjectile>();
        projScript.SetLocationSpeedAndDamage(end, projectileSpeed, projectileDamage);
        //enemyPillarSound.Play();
        Instantiate(enemyPillarSound, transform);
    }

    //Destroys Pillar.
    public void DestroyPillar()
    {
        if (curColorIndex != 0)
        {
            pillarIsDead = true;
            curColorIndex = ChangeOrbColor(deadColor, colorChangeTime, 2);
        }
        else
        {
            //Debug.Log("Player is out of range");
        }
    }
}