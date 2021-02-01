using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI text;
    private GameObject parent;

    private void Awake()
    {
        parent = Image.transform.parent.gameObject;
        parent.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Gravity>() == null) return;
        parent.SetActive(true);
        Debug.Log("enter");
        Image.DOFade(1f, 0.5f);
        text.DOFade(11f, 0.2f);

    }
}
