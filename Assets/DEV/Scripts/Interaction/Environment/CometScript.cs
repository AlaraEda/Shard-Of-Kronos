using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DEV.Scripts.Managers;
using UnityEngine;
using DG.Tweening;

public class CometScript : MonoBehaviour
{
    //The comet
    private Transform comet;

    //first index = Transform Parent
    private Transform transformOjb;

    //Array of comets for .path
    public Vector3[] pathTransform;

    private float durationFall;

    //create a Tween
    private Tween myTween;

    [Header("Settings Comet")] [SerializeField]
    private PathType pathType = PathType.CatmullRom;

    private GameObject objectToDrop;
    private Camera cam;
    public CinemachineImpulseSource impulseSource;
    


    void Start()
    {
        cam = SceneContext.Instance.mainCamera;
        comet = transform.GetChild(0).gameObject.transform;
        //transformOjb = transform.GetChild(0).transform;
    }

    public IEnumerator TriggerComet()
    {
        comet.transform.DOPath(pathTransform, 2, pathType).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(2);
        impulseSource.GenerateImpulse(cam.transform.forward);
    }
}