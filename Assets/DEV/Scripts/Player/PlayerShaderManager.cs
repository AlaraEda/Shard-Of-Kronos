/*
 * This script handles the shader on the player.
 * This shader is responsible for player feedback (red flash) when the player has been hit.
 * It is subscribed to the OnPlayerHealthChange event to know when to update the shader.
 * There are 3 different materials linked to the player (different texture) that all use this same shader.
 *
 * The linked shader graph is called: 'PlayerShader.shadergraph'
 *
 * Made by: Mats.
 */
using System.Collections;
using DEV.Scripts.Managers;
using DEV.Scripts.Player;
using DG.Tweening;
using UnityEngine;

public class PlayerShaderManager : MonoBehaviour
{
    // Declaration of used fields
    private GameObject meshParent;
    private GameObject bow1Object;
    private GameObject bow2Object;
    private GameObject playerObject;

    private Material bowMat1;
    private Material bowMat2;
    private Material[] playerMat;

    private int bowMat1ID;
    private int bowMat2ID;
    private int[] playerMatID;
    
    /// <summary>
    /// Assigns values to declared fields, and sets the '_HitAmount' parameter of the shaders to 0.
    /// </summary>
    private void Awake()
    {
        meshParent = SceneContext.Instance.playerTransform.GetChild(0).gameObject;
        bow1Object = meshParent.transform.GetChild(0).gameObject;
        bow2Object = meshParent.transform.GetChild(1).gameObject;
        playerObject = meshParent.transform.GetChild(4).gameObject;

        bowMat1 = bow1Object.GetComponent<SkinnedMeshRenderer>().material;
        bowMat2 = bow2Object.GetComponent<SkinnedMeshRenderer>().material;
        playerMat = playerObject.GetComponent<SkinnedMeshRenderer>().materials;

        SceneContext.Instance.playerController.OnPlayerHealthChangeEvent += OnPlayerHealthChanging;
        
        foreach (var mat in playerMat)
        {
            mat.SetFloat("_HitAmount", 0f);
        }
        bowMat1.SetFloat("_HitAmount", 0f);
        bowMat2.SetFloat("_HitAmount", 0f);
    }

    /// <summary>
    /// Method that is subscribed to the OnPlayerHealthChangeEvent, to start a coroutine when the player takes damage.
    /// </summary>
    /// <param name="sender">Object that triggered the event.</param>
    /// <param name="args">Arguments passed when the event was triggered.</param>
    private void OnPlayerHealthChanging(object sender, PlayerHealthChangeEventArgs args)
    {
        StartCoroutine(FlashPlayerWhenHit(1.0f, .2f));
    }

    /// <summary>
    /// Method to change the '_HitAmount' parameter from the shader graph over time, to make it seem like a red flash.
    /// </summary>
    /// <param name="endValue">The end value to lerp towards.</param>
    /// <param name="duration">The time it takes for the lerp to complete.</param>
    /// <returns>Used for delaying the method by the value of the parameter 'duration'</returns>
    private IEnumerator FlashPlayerWhenHit(float endValue, float duration)
    {
        foreach (var mat in playerMat)
        {
            mat.DOFloat(endValue, "_HitAmount", duration);
        }
        bowMat1.DOFloat(endValue, "_HitAmount", duration);
        bowMat2.DOFloat(endValue, "_HitAmount", duration);
        yield return new WaitForSeconds(duration);
        foreach (var mat in playerMat)
        {
            mat.DOFloat(0, "_HitAmount", duration);
        }
        bowMat1.DOFloat(0, "_HitAmount", duration);
        bowMat2.DOFloat(0, "_HitAmount", duration);
    }
}
