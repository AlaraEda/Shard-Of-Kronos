using System;
using DEV.Scripts.BowAndArrow;
using UnityEngine;

namespace DEV.Scripts.EnemyPillar
{
    public class BeamPillarDieDetection : MonoBehaviour
    {
        private BeamPillarPuzzle beamPillar;

        private void Awake()
        {
            beamPillar = GetComponentInParent<BeamPillarPuzzle>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerArrowBase>() != null)
            {
                beamPillar.DestroyPillar();
            }
        }
    }
}
