using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class KeepObjectAtOrigin : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0,0,0);
    }
}
