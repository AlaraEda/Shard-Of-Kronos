using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxCollider : MonoBehaviour
{
    private MovingBox movingBox;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material mat;

    private bool hit;

    private void Awake()
    {
        movingBox = transform.parent.GetComponent<MovingBox>();
        meshRenderer = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
        mat.DOFloat(0f, "_IFade", 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        switch (other.gameObject.name)
        {
            case var a when a.Contains("Arrow"):

                //Destroy(other.gameObject);
                movingBox.OnHit(this.name, mat);
                Debug.Log(this.name);
                break;
        }

        if (other.gameObject.tag.Contains("Wall"))
        {
            movingBox.SetPositionAndStopCoroutine(name, other.gameObject.transform.parent.parent.parent.localPosition);
        }
    }


    private void OnTriggerStay(Collider other)
    {
//        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Contains("Hole"))
        {
            StartCoroutine(movingBox.OnHitDestroy());
        }
        else if (other.gameObject.name.Contains("Finish"))
        {
            StartCoroutine(movingBox.FinishPuzzle());
        }
    }
}