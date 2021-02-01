using System;
using DEV.Scripts.Input;
using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class PuzzlePillarInteractable : Interactable
    {
        
        // Serialized private vars
        [SerializeField] private int pillarIndex;
        
        //Settings
        private GameObjectRunTime playerSet;

        // GameObjects and components
        private GameObject puzzlePillarParent;
        private PuzzleTurnPillars puzzlePillarScript;
        
        // Private vars used in this script
        private int wantedPillarRotation;
        private bool pillarIsInCorrectRotation;
        private int timesRotated=1;


        protected override void Awake()
        {
            base.Awake();
            // Setting the Settings vars
            playerSet = Resources.Load<GameObjectRunTime>("ObjectRunTime");
            puzzlePillarParent = GetComponentInParent<PuzzleTurnPillars>().gameObject;
            puzzlePillarScript = puzzlePillarParent.GetComponent<PuzzleTurnPillars>();
            
        }

        private void Start()
        {
            //Debug.Log(gameObject.transform.parent.parent.name + " needs Rot: " +wantedPillarRotation);
        }

        public override void OnPlayerInteract()
        {
            puzzlePillarScript.InteractWithPillar(pillarIndex);
            
        }
        

        public void AddTimesRotated(int amount)
        {
            timesRotated += amount;
            if (timesRotated > 3)
            {
                timesRotated = 1;
            }
        }

        public void SetWantedRotation(int wantedRot)
        {
            wantedPillarRotation = wantedRot;
        }

        public int GetCurrentTimesRotated()
        {
            return timesRotated;
        }

        public void SetPillarIsInCorrectRotation(bool set)
        {
            pillarIsInCorrectRotation = set;
        }
    }
    
    
}