using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// This script activates its self when initialized by GhostSettings.cs
/// After a short amount of time it removes this object/script
/// </summary>
public class GhostTrail : MonoBehaviour
{
    private int checkIndex;
    private Transform transformCopy;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    public Material material;

    private void Awake()
    {
        transformCopy = SceneContext.Instance.ghostSpawner;
        checkIndex = transformCopy.childCount;

        if (checkIndex != 0)
        {
            skinnedMeshRenderer.materials = SceneContext.Instance.ghostSettings.skinnedMeshRenderer.materials;

            foreach (var t in skinnedMeshRenderer.materials)
            {
                t.DOFloat(0.1f, "_FadeGhost", 0.5f);
            }

            StartCoroutine(DeleteGhostSeq());
        }
    }

    private IEnumerator DeleteGhostSeq()
    {
        yield return new WaitForSeconds(1);

        Sequence sequence = DOTween.Sequence();

        foreach (var t in skinnedMeshRenderer.materials)
        {
            t.DOFloat(1f, "_FadeGhost", 0.5f);
        }

        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }
}