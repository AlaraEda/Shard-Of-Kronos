using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using UnityEngine;

public class BowGrappleMovement : MonoBehaviour, IMovementModifier
{

    private MovementHandeler movementHandeler;
    
    private void Awake()
    {
        Name = GetType().ToString();
        movementHandeler = SceneContext.Instance.playerManager.movementHandeler;
        
    }

    
    private void OnEnable() => movementHandeler.AddModifier(this);

    private void OnDisable() => movementHandeler.RemoveModifier(this);

    public Vector3 Value { get; set; }
    public string Name { get; set; }
}
