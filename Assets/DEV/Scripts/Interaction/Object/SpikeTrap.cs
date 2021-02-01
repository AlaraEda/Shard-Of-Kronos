using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using DG.Tweening;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float height;

    public BoxCollider triggerBox;

    public GameObject spikes;

    public Vector3 normalHeight;

    private bool spiritWorld;

    [SerializeField] private float normalTime;
    [SerializeField] private float normalRetrieveTime;

    [SerializeField] private float spiritTime;
    [SerializeField] private float spiritRetrieveTime;

    public Vector3 positionVector;
    public Vector3 positionVectorExtend;


    public void Awake()
    {
        normalHeight = transform.GetChild(0).transform.localPosition;

        SceneContext.Instance.worldSwitchManager.OnWorldSwitchEvent += OnWorldSwitchEvent;

        spiritWorld = false;
    }


    private void OnWorldSwitchEvent(object sender, WorldSwitchEventArgs args)
    {
        if (args.NextWorld == WorldSwitchEventArgs.WorldType.Normal)
        {
            spiritWorld = false;
        }
        else
        {
            spiritWorld = true;
        }
    }


    public void SetAttack()
    {
        if (spiritWorld)
        {
            var position = new Vector3(0, 0, 11);
            spikes.transform.DOLocalMove(positionVectorExtend, spiritTime);
        }
        else
        {
            var position = new Vector3(0, 0, 11);
            spikes.transform.DOLocalMove(positionVector, normalTime);
        }
    }

    public void RetrieveAttack()
    {
        if (spiritWorld)
        {
            var position = new Vector3(0, 0, 0);
            spikes.transform.DOLocalMove(positionVector, spiritRetrieveTime);
        }
        else
        {
            var position = new Vector3(0, 0, 0);
            spikes.transform.DOLocalMove(positionVectorExtend, normalRetrieveTime);
        }
    }
}