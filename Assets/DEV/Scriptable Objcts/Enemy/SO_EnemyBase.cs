using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create a scriptable object of inventory via the create asset menu. 
//[CreateAssetMenu(fileName = "New NormalEnemy", menuName = "Enemy/Enemy")]
public class SO_EnemyBase : ScriptableObject
{
    [Header("General settings")]
    public string nameEnemy;
    public EnemyTypeEnum.EnemyClassEnum classEnum;
    public float angryCooldown;

    [Header("Health")]
    public float healthEnemy;

    [Header("Range")]
    public float attackRange;
    public float sightRange;
    public float territorialRange;
    public float angryRange;

    [Header("AI Agent Attributes")] 
    public float rotationDuration;
    public float movementSpeed;
    public float angularSpeed;
    public float acceleration;
    public float stoppingDistance;
    public bool autoBraking;
    
    [Header("Attack")]
    public float attackDamage;
    public float timeBetweenAttacks;

}
