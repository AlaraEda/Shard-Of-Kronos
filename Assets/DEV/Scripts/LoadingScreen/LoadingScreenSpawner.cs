using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenSpawner : MonoBehaviour
{
    private int random;
    public Sprite[] Sprite_Background;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, Sprite_Background.Length);
        //GetComponent<SpriteRenderer>().sprite = Sprite_Pic[random];
        GetComponent<Image>().sprite = Sprite_Background[random];
    }
}
