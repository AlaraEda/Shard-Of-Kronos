using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RotateObjectInInventory : MonoBehaviour
{

    public GameObject RotateObj { get; set; }

    public float rotSpeed = 0.1f;
    
    public Vector2 mousePosition;
    
    public void RotateObjectInInventoryCanvas()
    {
        float rotX = mousePosition.x * rotSpeed * Time.deltaTime;
        float rotY = mousePosition.y * rotSpeed * Time.deltaTime;
    
        //Debug.Log("mousePosition.X" + rotX);
        RotateObj.transform.Rotate(rotX, rotY, 0, Space.World);
        //RotateObj.transform.Rotate(Vector3.up, -rotX);
        //RotateObj.transform.Rotate(Vector3.right, rotY);
    }
    
}