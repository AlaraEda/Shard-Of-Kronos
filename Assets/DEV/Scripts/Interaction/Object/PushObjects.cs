using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PushObjects : MonoBehaviour
{
    //  private GameObjectRunTime playerSet;
    //private GameObject playerObject;
    private const int playerLayer = 10;
    //mass van de object
    public float ObMass;
    //hoeveel de mass wordt in secondes tijdens het pushe
    public float PushAtMass;
    //secondes bij het pushen. In de x aantal seconden wordt de obmass omgerekent naar pushatmass
    public float pushingTime;
    private float ForceToPush = 250;
    private Rigidbody rb;
    private Rigidbody rbplayer;
    private Vector3 dir;

    float _pushingTime = 0;

    //checkt if f is pressed
    private bool fpressedrelease;
    void Start()
    {
        rbplayer = GameObject.Find("Player (Don't change in Hierarchy!)").GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.Log("notfoundrigidbody");
        rb.mass = ObMass;

    }
    void Update()
    {

        //vel = rb.velocity.magnitude;
        if (rb.isKinematic == false)
        {
            _pushingTime += Time.deltaTime;
            if (_pushingTime >= pushingTime)
            {
                _pushingTime = pushingTime;
            }
            rb.mass = Mathf.Lerp(ObMass, PushAtMass, _pushingTime / pushingTime);
            rb.AddForce(dir * ForceToPush, ForceMode.Force);
        }
        else
        {
            rb.mass = ObMass;
            _pushingTime = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == playerLayer)
        {
            //rbplayer.constraints = RigidbodyConstraints.FreezePositionZ;
            //rbplayer.constraints = RigidbodyConstraints.FreezeRotation;
            dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
        }
    }

    public void CheckIfPressed(bool boolcheck)
    {
        fpressedrelease = boolcheck;
        rb.isKinematic = false;

        if (fpressedrelease == true)
        {
            rb.isKinematic = true;
        }
    }
}
