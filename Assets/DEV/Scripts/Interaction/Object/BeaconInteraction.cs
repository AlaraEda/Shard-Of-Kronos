using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DEV.Scripts.Input;
using DEV.Scripts.Managers;
using DEV.Scripts.Puzzles.LightReflector;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeaconInteraction : Interactable
{
    public ParticleSystem particleForExplosion;
    public Transform parent;
    public LightReflector light;
    public GameObject creditsObj;
    public Image imageBg;
    public Image kronosLogo;
    public Image chronicleLogo;
    public GameObject chronicleLogo2;
    public Text thanksForPlaying;
    public GameObject text1;
    public GameObject text2;
    private SpawnFracturedMeteoorr spawnFracturedMeteoorr;
    public CinemachineVirtualCamera cinematicCam;
    private Sequence camSeq;

    [SerializeField] private MovementHandeler movementHandler;          

    //Alles dat op false moet wanneer de loading screen verschijnt
    public GameObject GeneralUIPauseMenu;
    public GameObject GeneralUIHudCanvas;
    
    protected override void Awake()
    {
        base.Awake();
        var tempBgColor = imageBg.color;
        tempBgColor.a = 0;
        imageBg.color = tempBgColor;

        var tempThanksColor = thanksForPlaying.color;
        tempThanksColor.a = 0;
        thanksForPlaying.color = tempThanksColor;

        var tempKronosColor = kronosLogo.color;
        tempKronosColor.a = 0;
        kronosLogo.color = tempKronosColor;

        var tempChronicleColor = chronicleLogo.color;
        tempChronicleColor.a = 0;
        chronicleLogo.color = tempChronicleColor;
        
        creditsObj.SetActive(false);
    }

    public void Start()
    {
        
        spawnFracturedMeteoorr = SceneContext.Instance.spawnFracturedMeteoorr;
    }
    public override void OnPlayerInteract()
    {

        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        //toetsen op keyboard uitzetten
        movementHandler = SceneContext.Instance.playerTransform.gameObject.GetComponent<MovementHandeler>();
        movementHandler.changingPlayerPosition = true;
        SceneContext.Instance.playerController.inputController.OnDisable();

        //Set General UI to false so that you won't see it when the screen is loading
        GeneralUIPauseMenu.SetActive(false);
        GeneralUIHudCanvas.SetActive(false);

        camSeq = DOTween.Sequence();
        cinematicCam.Priority = 100;
        yield return new WaitForSeconds(2);
        spawnFracturedMeteoorr.SpawnFracturedObject();
        light.alwaysEmit = false;
        SceneContext.Instance.hudContext.DisplayHint("The meteor has been destroyed. The lands are safe once again!");

        foreach (Transform item in parent)
        {
            var rb = item.gameObject.AddComponent<Rigidbody>();
            if (rb !=null)
            {
                particleForExplosion.Play();
                rb.mass = 0.25f;
            }
           
        }

        SceneContext.Instance.levelAudioManager.PlayEndMusic();
        yield return new WaitForSeconds(10);
        creditsObj.SetActive(true);
        text1.SetActive(false);
        text2.SetActive(false);
        chronicleLogo2.SetActive(false); //
        camSeq.Append(cinematicCam.gameObject.transform.DORotate(new Vector3(40, -242, 4), 3));
        camSeq.Append(thanksForPlaying.DOFade(1, 3));
        camSeq.Append(kronosLogo.DOFade(1,3));
        camSeq.Append(chronicleLogo.DOFade(1,3));
        camSeq.Append(cinematicCam.gameObject.transform.DOMove(new Vector3(-180, 250,206), 10));
        yield return new WaitForSeconds(10);
        imageBg.DOFade(1, 5);
        yield return new WaitForSeconds(7);
        camSeq.Append(thanksForPlaying.DOFade(0, 3));
        camSeq.Append(kronosLogo.DOFade(0,3));
        camSeq.Append(chronicleLogo.DOFade(0,3));
        yield return new WaitForSeconds(3);
        chronicleLogo.transform.position += new Vector3(0, 140, 0); 
        text1.SetActive(true);
        text2.SetActive(true);
        text1.gameObject.transform.DOLocalMoveY(2300, 30);
        text2.gameObject.transform.DOLocalMoveY(2300, 30);
        yield return new WaitForSeconds(20);
        camSeq.Append(kronosLogo.DOFade(1,3));
        //camSeq.Append(chronicleLogo.DOFade(1,3));
        chronicleLogo2.SetActive(true); 
        yield return new WaitForSeconds(10);
        Application.Quit();
    }
}
