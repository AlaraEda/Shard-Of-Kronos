using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoorrExplode : MonoBehaviour
{
    private float destroyDelay = 5.0f;
    public float minForce;
    public float maxForce;
    public float radius;



    void Start()
    {

    }

    public void Explode(GameObject gameObj)
    {
        foreach (Transform item in transform)
        {
            var rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }

            Destroy(item.gameObject, destroyDelay);
        }
    }
}

