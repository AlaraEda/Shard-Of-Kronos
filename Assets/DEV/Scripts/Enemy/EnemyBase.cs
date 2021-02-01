/*
 * This is the base class for our enemy implementation. This class can be extended to change out important parameters
 * and override different methods.
 *
 * Scriptable objects have been used to fill in a big chunk of field values (in the child versions of this base class).
 * It also handles the states of the enemy, check whether or not the enemy should get 'Angry' and chase the player.
 * All the enemy animations are also set in this base class.
 *
 * Made by: Mats & Okan.
 */

using System.Collections;
using DEV.Scripts.BowAndArrow;
using DEV.Scripts.Extensions;
using DEV.Scripts.Managers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace DEV.Scripts.Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        /// <summary>
        /// Enemy state enumerator, to save in which state the enemy currently is.
        /// </summary>
        public enum StateEnum
        {
            Idle = 0,
            Chase = 1,
            Attack = 2,
            ReturningHome = 3,
            Dead = 4
        }

        /// <summary>
        /// Property declaration of the State enum, with event invocation whenever this value is changed.
        /// </summary>
        public StateEnum State
        {
            get => currentState;
            protected set
            {
                if (value != currentState)
                {
                    OnStateChange?.Invoke(this, new EnemyStateChangeEventArgs
                    {
                        PreviousState = currentState,
                        NewState = value
                    });
                    currentState = value;
                    animController.SetInteger(AnimCurrentState, (int) value);
                }
            }
        }

        //Settings
        private SettingsEditor settings;

        // This is an internal field for State property, do NOT use this!
        private StateEnum currentState;

        protected Settings.BowAndArrowSettingsModel BowAndArrowSettings { get; set; }
        protected Settings.EnemySettingsModel EnemySettingsModel { get; set; }

        // Transforms
        protected Transform PlayerTransform { get; set; }
        protected PlayerController PlayerController { get; set; }
        protected Transform EnemyGraphicsTransform { get; set; }

        // General settings for enemies, values are set by scriptable object in lowest form of class child. Usually not changed during runtime.
        protected string NameEnemy { set; get; }
        protected EnemyTypeEnum.EnemyClassEnum ClassEnum { get; set; }
        protected NavMeshAgent Agent { set; get; }
        protected float HealthEnemy { set; get; }
        protected float MovementSpeed { set; get; }
        protected float AngularSpeed { set; get; }
        protected float Acceleration { set; get; }
        protected float StoppingDistance { set; get; }
        protected bool AutoBraking { set; get; }
        protected float AttackDamage { set; get; }
        protected float TimeBetweenAttacks { get; set; }
        protected Vector3 StartingPosition { get; set; }
        protected float SightRange { get; set; }
        protected float AttackRange { get; set; }
        protected float TerritorialRange { get; set; }
        protected float AngryRange { get; set; }
        protected float AngryCooldown { get; set; }
        protected LayerMask WhatIsPlayer { get; set; }

        // Calculated distances & locations
        protected float DistanceToPlayer { get; set; }
        protected float DistancePlayerToInitPoint { get; set; }
        protected float BackHomeDistance { get; set; }
        protected Vector3 TargetPosition { get; set; }

        // Debug
        protected bool DrawGizmos { get; set; }

        // Extra enemy states and vars, usually changed during runtime.
        protected bool IsSlowed { get; set; }
        protected bool HasAttacked { get; set; }
        [SerializeField] private bool IsAngered { get; set; }
        protected bool AngryTimerStarted { get; set; }
        protected float CurrentAngryCooldown { get; set; }
        protected float BaseRotationDuration { get; set; }
        protected float CurrentAttackCooldown { get; set; }

        // Private constants
        private LayerMask enemyLayer;

        // Animations
        protected Animator animController;
        protected static readonly int AnimCurrentState = Animator.StringToHash("CurrentState");
        protected static readonly int AnimIsMovingLeft = Animator.StringToHash("IsRotatingLeft");
        protected static readonly int AnimIsMovingRight = Animator.StringToHash("IsRotatingRight");
        protected static readonly int AnimAttackTrigger = Animator.StringToHash("AttackTrigger");

        // Event Handlers
        public delegate void StateChangedEventHandler(object sender, EnemyStateChangeEventArgs args);

        public event StateChangedEventHandler OnStateChange;

        /// <summary>
        /// Awake function sets private fields and protected properties of the enemy.
        /// </summary>
        public virtual void Awake()
        {
            //AI
            Agent = GetComponent<NavMeshAgent>();
            //get Scripts
            PlayerTransform = SceneContext.Instance.playerTransform;
            PlayerController = SceneContext.Instance.playerManager;
            WhatIsPlayer = LayerMask.GetMask("Player");
            enemyLayer = LayerMask.GetMask("Enemy");
            settings = SettingsEditor.Instance;
            BowAndArrowSettings = settings.bowAndArrowSettingsModel;
            EnemySettingsModel = settings.enemySettingsModel;
            StartingPosition = transform.position;
            State = StateEnum.Idle;
            IsAngered = false;
            EnemyGraphicsTransform = transform.GetChild(0).GetComponent<Transform>();
            Agent.updateRotation = false;
            SceneContext.Instance.ActiveEnemies.AddNonExists(this);
            animController = GetComponentInChildren<Animator>();
            //OnStateChange += (sender, args) => Debug.Log($"New Enemy State: {args.NewState.ToString()}");
        }

        /// <summary>
        /// Sets private field to initial value when spawned in.
        /// </summary>
        public void Start()
        {
            CurrentAngryCooldown = AngryCooldown;
            CurrentAttackCooldown = TimeBetweenAttacks;
        }

        /// <summary>
        /// Method that sets the enemy component values (and property values) with the values present in the scriptable object.
        /// This method is usually called from the lowest available child of this base class.
        /// </summary>
        /// <param name="enemySo">Scriptable object to use the values from to fill components.</param>
        protected virtual void SetBaseScriptableObjectsPrefs(SO_EnemyBase enemySo)
        {
            NameEnemy = enemySo.nameEnemy;
            ClassEnum = enemySo.classEnum;
            AngryCooldown = enemySo.angryCooldown;

            // Health
            HealthEnemy = enemySo.healthEnemy;

            // Ranges
            AttackRange = enemySo.attackRange;
            SightRange = enemySo.sightRange;
            TerritorialRange = enemySo.territorialRange;
            AngryRange = enemySo.angryRange;

            // AI
            BaseRotationDuration = enemySo.rotationDuration;
            MovementSpeed = enemySo.movementSpeed;
            AngularSpeed = enemySo.angularSpeed;
            Acceleration = enemySo.acceleration;
            StoppingDistance = enemySo.stoppingDistance;
            AutoBraking = enemySo.autoBraking;

            // Attack
            AttackDamage = enemySo.attackDamage;
            TimeBetweenAttacks = enemySo.timeBetweenAttacks;

            // Setting the actual component values
            Agent.speed = MovementSpeed;
            Agent.angularSpeed = AngularSpeed;
            Agent.acceleration = Acceleration;
            Agent.stoppingDistance = StoppingDistance;
            Agent.autoBraking = AutoBraking;
        }

        /// <summary>
        /// Fixed update is used to calculate needed distances and also checks and reacts to states.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            var playerPos = PlayerTransform.transform.position;
            DistanceToPlayer = Vector3.Distance(transform.position, playerPos);
            DistancePlayerToInitPoint = Vector3.Distance(StartingPosition, playerPos);
            BackHomeDistance = Vector3.Distance(transform.position, StartingPosition);
            TargetPosition = playerPos;
            ReactToStates();
            CheckState();
        }

        /// <summary>
        /// Update function is used to update internally used timer variables.
        /// </summary>
        protected void Update()
        {
            if (AngryTimerStarted)
            {
                CurrentAngryCooldown -= Time.deltaTime;
                if (CurrentAngryCooldown <= 0)
                {
                    IsAngered = false;
                    AngryTimerStarted = false;
                    CurrentAngryCooldown = AngryCooldown;
                }
            }

            if (HasAttacked)
            {
                CurrentAttackCooldown -= Time.deltaTime;
                if (CurrentAttackCooldown <= 0)
                {
                    HasAttacked = false;
                    CurrentAttackCooldown = TimeBetweenAttacks;
                }
            }
        }

        /// <summary>
        /// Used to check in which state an enemy needs to be. Mainly based on distance from player & distance from
        /// it's initial spawn location.
        /// It also sets the State property to the corresponding value.
        /// </summary>
        protected virtual void CheckState()
        {
            switch (DistanceToPlayer)
            {
                case var ex when ex > SightRange && !IsAngered:
                    State = StateEnum.Idle;
                    break;

                //When distance is smaller than AttackRange && distance is smaller than attackRange
                case var ex when (ex < AttackRange && ex < SightRange) ||
                                 (State == StateEnum.Chase && IsAngered && (ex < AttackRange && ex < SightRange)):
                    State = StateEnum.Attack;
                    break;

                case var ex when (ex < SightRange) || (IsAngered && ex > SightRange):
                    State = StateEnum.Chase;
                    break;
            }

            if (State == StateEnum.Chase || State == StateEnum.Attack)
            {
                //Debug.Log("CurrentlyChasing or attacking");
            }
            else if ((DistancePlayerToInitPoint > TerritorialRange && BackHomeDistance >= 1f) && !IsAngered)
            {
                //Debug.Log("Am not chasing or attacking, and need to return home");
                State = StateEnum.ReturningHome;
            }
        }

        /// <summary>
        /// Method that acts based on the current state of the enemy.
        /// A switch case handles the different states that the enemy can be in.
        /// </summary>
        protected virtual void ReactToStates()
        {
            if (State == StateEnum.Dead)
            {
                DeleteEnemy();
            }
            else
            {
                switch (State)
                {
                    // Stops enemy from moving, and removes previous path (so it doesn't start moving to that path again)
                    case StateEnum.Idle:
                        Agent.isStopped = true;
                        Agent.ResetPath();
                        break;

                    // Moves the enemy back to it's initial spawn location.
                    case StateEnum.ReturningHome:
                        if (BackHomeDistance >= 1f)
                        {
                            Agent.SetDestination(StartingPosition);
                        }
                        else
                        {
                            //Debug.Log("already near home, going Idle");
                            State = StateEnum.Idle;
                        }

                        break;
                    // Sets new destination to the target position (player position + attack range) and rotates enemy towards player
                    case StateEnum.Chase:
                        Agent.SetDestination(TargetPosition);
                        StartCoroutine(RotateToPlayer(false));
                        // If statement to start a cooldown if the enemy is outside his territorial range
                        if (DistancePlayerToInitPoint > TerritorialRange && IsAngered)
                        {
                            if (BackHomeDistance > TerritorialRange)
                            {
                                AngryTimerStarted = true;
                            }
                        }

                        break;

                    // Resets the enemy path (so it is standing still) and starts attack cooldown to instantiate attacks (after rotating towards a player).
                    case StateEnum.Attack:
                        Agent.isStopped = true;
                        Agent.ResetPath();
                        //transform.LookAt(PlayerTransform);
                        if (DistancePlayerToInitPoint > TerritorialRange && IsAngered)
                        {
                            if (BackHomeDistance > TerritorialRange)
                            {
                                AngryTimerStarted = true;
                            }
                        }

                        if (!HasAttacked)
                        {
                            StartCoroutine(RotateToPlayer(true));
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Method that is called when an enemy has been hit by an arrow. It mainly handles taking damage and removing the arrow that hit this enemy.
        ///
        /// This method handles the 'Angry' mechanic as well.
        /// If an enemy has been hit by an arrow, it checks if other enemies are in his 'Angry Range' with a OverlapSphere check.
        /// If there were any enemies in his Angry Range, their 'IsAngered' property will be set to true (and will start to chase the player).
        /// </summary>
        /// <param name="arrowThatHitThisEnemy">Collider parameter of the arrow that hit this enemy.</param>
        public void CollideWithArrow(Collider arrowThatHitThisEnemy)
        {
            var arrowClass = arrowThatHitThisEnemy.gameObject.GetComponentInParent<PlayerArrowBase>();
            Collider[] hits = Physics.OverlapSphere(transform.position, AngryRange, enemyLayer);
           // Debug.Log(hits[0].name);
            foreach (var hit in hits)
            {
                var enemy = hit.gameObject.GetComponentInParent<EnemyBase>();
                enemy.IsAngered = true;
            }

            TakeDamage(arrowClass.ArrowDamage);
            Destroy(arrowThatHitThisEnemy.gameObject.GetComponentInParent<PlayerArrowBase>().gameObject);
        }

        /// <summary>
        /// Method to rotate the enemy towards the player. It Also handles activation the correct animation clip.
        /// When rotation is completed, and the parameter 'triggerAttack' is set to true,
        /// the enemy will also attack (after the animation has been completed).
        /// </summary>
        /// <param name="triggerAttack">If set to true, will tell the enemy to attack after rotation towards the player, and finishing the attack animation.</param>
        /// <returns>Used for postponing actions in the method</returns>
        protected virtual IEnumerator RotateToPlayer(bool triggerAttack)
        {
            // Player pos cached
            Vector3 playerPos = PlayerTransform.position;

            // Check if player is to the left or right of enemy
            TargetLeftRight targetDir = transform.TargetLeftOrRight(playerPos);

            // Get correct animator param based on player direction
            int animParam = targetDir.TargetDirection == TargetLeftRight.LeftRightEnum.Left
                ? AnimIsMovingLeft
                : AnimIsMovingRight;

            // Play turning animation if left/right distance is greater than 0.1
            animController.SetBool(animParam, Mathf.Abs(targetDir.AngleDistance) > 0.1);

            // Start rotation and wait until complete
            Tween rotateTween = transform.DOLookAt(playerPos, BaseRotationDuration);
            yield return rotateTween.WaitForCompletion();

            // Peform attack if needed
            animController.SetBool(animParam, false);
            if (triggerAttack)
            {
                if (!HasAttacked)
                {
                    HasAttacked = true;
                    animController.SetTrigger(AnimAttackTrigger);
                    yield return new WaitForSeconds(1.20f);
                    Attack();
                }
            }
        }

        /// <summary>
        /// Base function of attacking. They are overriden by the children of this class, to have separate melee (not implemented yet) and ranged attacks.
        /// </summary>
        protected virtual void Attack()
        {
        }

        /// <summary>
        /// Method that handles taking damage for the enemies. It also raises an event in the SceneContext.cs script,
        /// to let all other script know that this enemy has been hit.
        /// </summary>
        /// <param name="amount">Amount of damage to take.</param>
        protected virtual void TakeDamage(float amount)
        {
            SceneContext.Instance.RaiseOnEnemyHitEvent(this, new EnemyHitEventArgs
            {
                Enemy = this,
                DamageAmount = amount,
                OldHealth = HealthEnemy,
                NewHealth = HealthEnemy - amount
            });

            HealthEnemy -= amount;
            if (HealthEnemy <= 0)
            {
                State = StateEnum.Dead;
            }
        }

        /// <summary>
        /// Handles removing the enemy from other scripts, before destroying this gameObject.
        /// </summary>
        protected virtual void DeleteEnemy()
        {
            SceneContext.Instance.ActiveEnemies.Remove(this);
            Destroy(gameObject);
        }

        /// <summary>
        /// Draws different Wire Sphere Gizmos to make debugging of the ranges easier for the designers.
        /// </summary>
        protected virtual void OnDrawGizmos()
        {
            if (!DrawGizmos) return;
            var enemyPos = transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enemyPos, AttackRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(enemyPos, SightRange);

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(enemyPos, AngryRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(StartingPosition, TerritorialRange);
        }

        /// <summary>
        /// Method to reduce the movement speed of the enemy based on a set value divider found in the settings.
        /// </summary>
        /// <param name="slow">If true, applies slow. If false, deactivates slow.</param>
        public void SetEnemySlow(bool slow)
        {
            if (slow && !IsSlowed)
            {
                if (!Agent) return;
                Agent.speed /= BowAndArrowSettings.iceAoEArrowSpeedDivision;
                Agent.angularSpeed /= BowAndArrowSettings.iceAoEArrowSpeedDivision;
                BaseRotationDuration *= BowAndArrowSettings.iceAoEArrowSpeedDivision;
                TimeBetweenAttacks *= BowAndArrowSettings.iceAoEArrowSpeedDivision;
                IsSlowed = true;
            }
            else if (!slow && IsSlowed)
            {
                if (!Agent) return;
                Agent.speed *= BowAndArrowSettings.iceAoEArrowSpeedDivision;
                Agent.angularSpeed *= BowAndArrowSettings.iceAoEArrowSpeedDivision;
                BaseRotationDuration /= BowAndArrowSettings.iceAoEArrowSpeedDivision;
                TimeBetweenAttacks /= BowAndArrowSettings.iceAoEArrowSpeedDivision;
                IsSlowed = false;
            }
        }
    }
}