using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rotationLoad : MonoBehaviour
{
    public GameObject load;
    //public GameObject target;
    // private float rotZ;
    // public float RotationSpeed;
    // public bool ClockWiseRotation;

    void Start(){
        load.transform.DOLocalRotate(new Vector3(0, 0, 360), 20f, RotateMode.FastBeyond360).SetLoops(-1);
        //load.transform.DOLookAt(new Vector3(0,0,360), 1, AxisConstraint.Z, Vector3.zero).SetLoops(-1);


        //load.transform.DOLookAt(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), 1, AxisConstraint.Z, Vector3.zero);
    }
}
