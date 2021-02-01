using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3OpenDoor01 : MonoBehaviour
{
    
    public GameObject door;
 
    public BeamPillarPuzzle[] pillars;
    private bool alreadyOpened;

    void Update()
    {
        if (!alreadyOpened)
        {
            int alivePillars = 0;
            foreach (var pillar in pillars)
            {
                if (!pillar.pillarIsDead) alivePillars++;
            }

            if (alivePillars == 0)
            {
                door.SetActive(false);
            }
        }
    }
}
