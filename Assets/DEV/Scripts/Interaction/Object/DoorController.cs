using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DEV.Scripts.Input
{
    public class DoorController : MonoBehaviour
    {
        public GameObject instructions;
        public GameObject door;

        // GameObject go = GameObject.Find("Door_Trigger");
        // GameObject door = GameObject.FindGameObjectWithTag("Door");
        private void OnTriggerStay(Collider other)
        {
           if (other.tag == "Door")
           {
               instructions.SetActive(true);
               if (Keyboard.current.eKey.isPressed)
               {       
                   Debug.Log("Door open");
                    //GetComponent<Collider>().isTrigger = true;
                    Destroy(door);
                }
           }
        }
        private void OnTriggerExit(Collider other)
        {
           if (other.tag == "Door")
            {
                instructions.SetActive(false);

            }
        }
       
        void Start()
        {
            //GameObject door = GameObject.FindGameObjectWithTag("Door");
            // GetComponent<Collider>().isTrigger = true;

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
