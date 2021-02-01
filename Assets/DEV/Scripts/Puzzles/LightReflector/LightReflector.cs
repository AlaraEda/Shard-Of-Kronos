using System;
using System.Collections.Generic;
using System.Linq;
using DEV.Scripts.Extensions;
using DEV.Scripts.Managers;
using UnityEngine;

namespace DEV.Scripts.Puzzles.LightReflector
{
    public class LightReflector : MonoBehaviour, ILightReflector
    {
        public GameObject light;
        public LineRenderer line;
        public bool alwaysEmit;

        public LightReflector target;
        public LightReflector[] sources;

        private Ray rayOrigin;
        RaycastHit lightRayHit;
        private Color rayHitColor = Color.red;
        private float maxBeamDistance = 1000;

        public IList<ILightReflector> LightSources { get; set; }
        public ILightReflector LightTarget { get; set; }

        public GameObject EmitterGameObject => gameObject;

        protected virtual bool IsEmitting => LightSources.Count > 0 || alwaysEmit;


        protected virtual void Awake()
        {
            if (light == null) throw new NullReferenceException($"light instance is not assigned in inspector ({transform.parent.gameObject.name})");
            
            LightSources = new List<ILightReflector>();
            rayOrigin = new Ray();
        }

        protected virtual void Update()
        {
            light.SetActive(IsEmitting);
            line.enabled = IsEmitting;
            if (IsEmitting)
            {
               
                rayOrigin.origin = transform.position;
                rayOrigin.direction = transform.right; //emmiter.position - transform.position;
                var isHit = Physics.Raycast(rayOrigin, out lightRayHit, maxBeamDistance);

                if (isHit)
                {
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, lightRayHit.point);
                    var reflector = lightRayHit.collider.gameObject.GetComponent<ILightReflector>();

                    if (reflector != null)
                    {
                        rayHitColor = Color.green;
                        if (!reflector.LightSources.Contains(this) && !IsSourceParent(LightSources, reflector))
                        {
                            reflector.LightSources.Add(this);
                            if (LightTarget != null && reflector.EmitterGameObject != LightTarget.EmitterGameObject)
                            {
                                LightTarget.LightSources.Remove(this);
                            }

                            LightTarget = reflector;
                        }
                    }
                    else
                    {
                        rayHitColor = Color.red;
                        DisableLightTarget();
                    }
                    Debug.DrawLine(rayOrigin.origin, lightRayHit.point, rayHitColor, 0);
                }
                else
                {
                    rayHitColor = Color.red;
                    line.SetPosition(0, rayOrigin.origin);
                    line.SetPosition(1, rayOrigin.origin + rayOrigin.direction * maxBeamDistance);
                    DisableLightTarget();
                    Debug.DrawRay(rayOrigin.origin, rayOrigin.direction * 1000, rayHitColor, 0);
                }
                
            }
            else DisableLightTarget();
        }

        private void DisableLightTarget()
        {
            if (LightTarget != null)
            {
                LightTarget.LightSources.Remove(this);
                LightTarget = null;
            }
        }
        
        /// <summary>
        /// Recursive function for checking if one of the lightsources is also the current light target
        /// Helps prevent situations where a bunch of pillars are powering each other without an actual valid source
        /// </summary>
        /// <param name="sources">The current light sources of this reflector</param>
        /// <param name="target">The current object that is recieving light from us</param>
        /// <param name="depthLength">(Use default value for this!!) The maximum recursion length</param>
        /// <returns></returns>
        private bool IsSourceParent(IList<ILightReflector> sources, ILightReflector target, int depthLength = 0)
        {
            if (depthLength >= 5) return false;
            foreach (ILightReflector source in sources)
            {
                if (source.EmitterGameObject == target.EmitterGameObject)
                {
                    return true;
                }

                return IsSourceParent(source.LightSources, target, depthLength + 1);
            }

            return false;
        }
    }
}