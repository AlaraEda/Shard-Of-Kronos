using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorClearance : MonoBehaviour
{

    public GameObject door;

    public void SetDoor()
    {
        door.transform.DOMoveY(-2, 1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
