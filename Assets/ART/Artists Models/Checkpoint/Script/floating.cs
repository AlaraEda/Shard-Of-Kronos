using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating : MonoBehaviour
{
    //Assign a GameObject in the Inspector to rotate around
    public GameObject target;
    public GameObject[] floatingCrystals;
    public GameObject[] Waypoints;

    int current = 0;
    public float speed;
    float WPradius = 1;

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        floatingCrystals[0].transform.RotateAround(target.transform.position, Vector3.up, 40 * Time.deltaTime);
        floatingCrystals[1].transform.RotateAround(target.transform.position, Vector3.up, 100 * Time.deltaTime);
        floatingCrystals[2].transform.RotateAround(target.transform.position, Vector3.up, 80 * Time.deltaTime);
        floatingCrystals[3].transform.RotateAround(target.transform.position, Vector3.up, 70 * Time.deltaTime);
        floatingCrystals[4].transform.RotateAround(target.transform.position, Vector3.up, 60 * Time.deltaTime);



   

        if(Vector3.Distance(Waypoints[current].transform.position, target.transform.position) < WPradius)
        {
            speed = 0.05f;
            current++;
            if(current >= Waypoints.Length)
            {
                current = 0;
            }
        }
        target.transform.position = Vector3.MoveTowards(target.transform.position, Waypoints[current].transform.position, Time.deltaTime * speed);

    }
}
