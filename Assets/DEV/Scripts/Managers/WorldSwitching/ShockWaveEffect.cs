using System;
using UnityEngine;

namespace DEV.Scripts
{
    /// <summary>
    /// Simple behaviour script that will increase the Gameobject scale by a certain factor and then delete it
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class ShockWaveEffect : MonoBehaviour
    {
        // Inspector fields
        [Tooltip("The speed at which the transform scale will increase per frame")]
        [SerializeField] private float scaleSpeed;
        [Tooltip("The speed at which the material will fade out")]
        [SerializeField] private float fadeSpeed;
        [Tooltip("The max size factor of the GameObject. This ")]
        [SerializeField] private float maxLocalScaleFactor;
        
        // Runtime fields
        private float maxLocalScaleX;
        private Vector3 incrementVector;
        private Material material;
        private float matTransparency;
        private static readonly int MatOffsetPropId = Shader.PropertyToID("_Offset");

        private void Awake()
        {
            var localScale = transform.localScale;
            maxLocalScaleX = (localScale * maxLocalScaleFactor).x;
            incrementVector = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
            material = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            if (transform.localScale.x <= maxLocalScaleX)
            {
                matTransparency = material.GetFloat(MatOffsetPropId);
                material.SetFloat(MatOffsetPropId, matTransparency + fadeSpeed * Time.deltaTime);
                transform.localScale += incrementVector * Time.deltaTime;
            }
            else Destroy(gameObject);
        }

        private void OnDestroy()
        {
            // Apparently, material instances need to be manually destroyed
            Destroy(material);
        }
    }
}