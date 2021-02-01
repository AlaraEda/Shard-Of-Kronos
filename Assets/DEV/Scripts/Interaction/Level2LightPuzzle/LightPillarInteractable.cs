using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.Puzzles.LightReflector;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DEV.Scripts.Interaction
{
    public class LightPillarInteractable : Interactable, ILightReflector
    {
        public float[] availableYRotations;
        public LightReflector reflector;

        private int currentRotIndex;
        private bool isAwake;
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> currentRotAction;

        public IList<ILightReflector> LightSources => reflector.LightSources;

        public ILightReflector LightTarget
        {
            get => reflector.LightTarget;
            set => reflector.LightTarget = value;
        }

        public GameObject EmitterGameObject => reflector.EmitterGameObject;

        public override void OnPlayerInteract()
        {
            if (IsLocked) return;
            if (currentRotAction != null && currentRotAction.active) return;
            currentRotIndex = availableYRotations.Length - 1 == currentRotIndex ? 0 : currentRotIndex + 1;
            var rotVector = new Vector3(transform.rotation.x, availableYRotations[currentRotIndex], transform.rotation.z);
            currentRotAction = transform.DORotate(rotVector, 4).OnComplete(EnableEmitter);;
            reflector.enabled = false;
            reflector.line.enabled = false;
            reflector.light.SetActive(false);
        }

        protected override void Awake()
        {
            base.Awake();
            // Get current rototation and see which element it corrosponds to in the list
            for (int i = 0; i < availableYRotations.Length; i++)
            {
                var rotY = availableYRotations[i];
                if (Mathf.Approximately(rotY, transform.rotation.y))
                {
                    currentRotIndex = i;
                    break;
                }
            }

            isAwake = true;
        }

        private void OnDisable()
        {
            // Resume rotation
            if (!IsLocked)
            {
                currentRotAction?.Complete();
                // var rotY = availableYRotations[currentRotIndex];
                // if (!Mathf.Approximately(rotY, transform.rotation.y))
                // {
                //     var curRot = transform.rotation;
                //     curRot.y = availableYRotations[currentRotIndex];
                //     transform.rotation = curRot;
                //     reflector.line.enabled = true;
                //     reflector.light.SetActive(true);
                //     reflector.enabled = true;
                // }
            }
        }

        private void EnableEmitter()
        {
            reflector.line.enabled = true;
            reflector.light.SetActive(true);
            reflector.enabled = true;
        }
    }
}