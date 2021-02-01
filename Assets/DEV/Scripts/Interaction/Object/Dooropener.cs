using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DEV.Scripts.Input;

public class Dooropener : Interactable
{

    public Transform door;


    public Vector3 closedPosition = new Vector3(0f, 8.7f, -51.8f);
    public Vector3 openPosition = new Vector3(0f, 18f, -51.8f);


    public float openSpeed = 5;
    private bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            door.position = Vector3.Lerp(door.position, 
                openPosition, Time.deltaTime * openSpeed);
        }
        else
        {
            door.position = Vector3.Lerp(door.position,
                closedPosition, Time.deltaTime * openSpeed);
        }
    }

    public void CloseDoor()
    {
        open = false;
    }
    public void OpenDoor()
    {
        open = true;
    }


    public override void OnPlayerInteract()
    {
        if (open==false)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
        
        //Destroy(gameObject);
        //OpenDoor();
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")// && Keyboard.current.eKey.isPressed)
        {
            //OnPlayerInteract();
            //if (Keyboard.current.eKey.isPressed)
            //{
                OpenDoor();
                Debug.Log("open");
            //}
            
        }
    }
    */
   /*
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")//&&Keyboard.current.eKey.isPressed)
        {
            CloseDoor();
            Debug.Log("Closed");
        }
    }
   */
}
