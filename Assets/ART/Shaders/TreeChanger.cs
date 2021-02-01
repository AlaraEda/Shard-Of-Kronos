using System;
using DEV.Scripts.Managers;
using DEV.Scripts.Managers.WorldSwitching;
using DEV.Scripts.WorldSwitching;
using DG.Tweening;
using UnityEngine;

namespace ART.Shaders
{
    public class TreeChanger : MonoBehaviour
    {
        // private MeshRenderer meshRenderer;
        // private int indexMat;
        //
        // private float timeAfterForceField;
        //
        // private WorldSwitchManager worldSwitchManager;
        //
        // private GlobalSpiritShaderManager globalSpiritShaderManager;
        //
        // private void Awake()
        // {
        //     globalSpiritShaderManager = SceneContext.Instance.globalSpiritShaderManager;
        //     worldSwitchManager = SceneContext.Instance.worldSwitchManager;
        //     
        //     if (worldSwitchManager != null)
        //     {
        //         SceneContext.Instance.worldSwitchManager.OnWorldSwitchEvent += OnWorldSwitchEvent;
        //     }
        // }
        //
        // private void Start()
        // {
        //     meshRenderer = GetComponentInChildren<MeshRenderer>();
        //
        //     if (meshRenderer.materials[0].name.Contains("Lea"))
        //     {
        //         indexMat = 0;
        //     }
        //
        //     if (meshRenderer.materials[1].name.Contains("Lea"))
        //     {
        //         indexMat = 1;
        //     }
        //   
        //     //SceneContext.Instance.worldSwitchManager.OnWorldSwitchEvent += OnWorldSwitchEvent;
        // }
        //
        //
        // public void ResetTree()
        // {
        //     meshRenderer.materials[indexMat].DOFloat(0.5f, "_Alpha", 3);
        // }
        //
        // private void OnWorldSwitchEvent(object sender, WorldSwitchEventArgs args)
        // {
        //     if (globalSpiritShaderManager.shaderState == GlobalSpiritShaderManager.TreeEnum.SINGLE)
        //     {
        //         if (args.NextWorld == WorldSwitchEventArgs.WorldType.Normal)
        //         {
        //             Debug.Log("test");
        //             meshRenderer.materials[indexMat].DOFloat(0.5f, "_Alpha", 1);
        //         }
        //         else
        //         {
        //             // meshRenderer.materials[indexMat].DOFloat(0.5f, "_Alpha", 1);
        //         }
        //     }
        //     
        //    
        // }
        //
        // private void FixedUpdate()
        // {
        //     if (timeAfterForceField == 5 && globalSpiritShaderManager.shaderState == GlobalSpiritShaderManager.TreeEnum.SINGLE)
        //     {
        //         timeAfterForceField = 0;
        //         SetTreeNotEffected();
        //     }
        // }
        //
        // public void SetTreeNotEffected()
        // {
        //     meshRenderer.materials[indexMat].DOFloat(0.78f, "_Alpha", 1);
        // }
        //
        // public void SetMaterial()
        // {
        //     if (globalSpiritShaderManager.shaderState == GlobalSpiritShaderManager.TreeEnum.SINGLE)
        //     {
        //         DOTween.To(() => timeAfterForceField, x => timeAfterForceField = x, timeAfterForceField, 5);
        //         Debug.Log("Setmat");
        //         meshRenderer.materials[indexMat].DOFloat(0.78f, "_Alpha", 3);
        //     }
        //    
        // }
        //
        // void OnApplicationQuit()
        // {
        //     //meshRenderer.materials[indexMat].SetFloat("_Alpha", 0);
        // }
    }
}