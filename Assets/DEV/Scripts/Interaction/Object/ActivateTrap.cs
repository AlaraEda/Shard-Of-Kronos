using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///
/// This is the box collider that activates SpikeTrap
/// </summary>
public class ActivateTrap : MonoBehaviour
{
    private SpikeTrap spikeTrap;

    public void Awake()
    {
        spikeTrap = transform.parent.GetComponent<SpikeTrap>();
    }

    private void OnTriggerEnter(Collider other)
    {
        spikeTrap.SetAttack();
        Debug.Log("SetAttack");
    }

    private void OnTriggerExit(Collider other)
    {
        spikeTrap.RetrieveAttack();
        Debug.Log("RetrieveAttack");
    }
}
