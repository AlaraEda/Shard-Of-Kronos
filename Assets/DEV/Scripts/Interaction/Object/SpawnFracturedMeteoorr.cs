using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEV.Scripts.Managers;
using DEV.Scripts.Input;
using System;

public class SpawnFracturedMeteoorr : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject fracturedObject;
    private MeteoorrExplode meteoorrExplode;



    public void Start()
    {
        meteoorrExplode = SceneContext.Instance.meteoorrExplode;
    }
    void Update()
    {

    }

    public void SpawnFracturedObject()
    {
        Destroy(originalObject);
        //GameObject fracturedObj = Instantiate(fracturedObject);
        //fracturedObj.GetComponent<MeteoorExplode>().Explode();
       /// meteoorrExplode.Explode(fracturedObj);

    }

}
