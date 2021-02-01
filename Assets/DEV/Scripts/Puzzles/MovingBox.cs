using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts;
using DG.Tweening;
using UnityEngine;

/// <Summary>
/// This script is part of the "Ancient Slide Puzzle" of level3 Island 5. 
/// In this script we handle the direction the box needs to move in and what 
/// happens if the box collides with a purple/red plane or a green plane.
///</Summary>
public class MovingBox : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(6, 3);
    public float slideTime = 1;
    public float moveDistance = 4;
    public OpenableDoor doorToOpen;

    private float maxDistanceX;
    private float maxDistanceZ;

    private bool isMoving;
    private Vector3 respawnPoint;
    private Vector3 lastMovingDir;
    private Sequence slideSeq;
    private Sequence fallSeq;
    private GameObject gfx;
    private SlidePuzzleMain puzzleMain;
    private bool puzzleIsCompleted;


    private void Awake()
    {
        respawnPoint = transform.position;
        maxDistanceX = moveDistance * gridSize.x;
        maxDistanceZ = moveDistance * gridSize.y;
        gfx = transform.GetChild(0).gameObject;
        puzzleMain = transform.parent.GetComponent<SlidePuzzleMain>();
    }

    //The box consists of 6 sides. OnHit checks through the string incomingFacade, which side of the box has been hit. 
    //IncomingFacade tells SlideBox() in which direction the box needs to move.  
    public void OnHit(string incomingFacade, Material mat)
    {
        if (!isMoving)
        {
            string incoming = incomingFacade;
            StartCoroutine(SlideBox(incoming, mat));
        }
    }

    //When the box collides with one of the purple/red planes on the ground,
    //the box gets teleported back to it's start position.
    public IEnumerator OnHitDestroy()
    {
        if (!isMoving)
        {
            isMoving = true;
            yield return new WaitForSeconds(slideTime + .1f);
            fallSeq = DOTween.Sequence();
            fallSeq.Append(transform.DOMove((transform.position - new Vector3(0, moveDistance * 2, 0)), slideTime));
            yield return new WaitForSeconds(slideTime + .1f);
            transform.DOScale(.1f, 0.05f);
            gfx.SetActive(false);
            transform.position = respawnPoint;
            yield return new WaitForSeconds(.3f);
            gfx.SetActive(true);
            transform.DOScale(1, slideTime);
            isMoving = false;
        }
    }

    //When the box collides with the green plane on the ground, the puzzle is finished.
    //And a door opens. 
    public IEnumerator FinishPuzzle()
    {
        if (!isMoving)
        {
            isMoving = true;
            yield return new WaitForSeconds(slideTime + .1f);
            
            transform.DOScale(.01f, slideTime * 2);
            yield return new WaitForSeconds((slideTime * 2) + .1f);
       
            Destroy(gameObject);
            isMoving = false;
            puzzleIsCompleted = true;
            puzzleMain.puzzleIsCompleted = true;
            if (doorToOpen != null)
            {
                doorToOpen.OpenDoor(true);
            }
        }
    }

    //If the box is not moving, move the box. This function tells the box in what direction the box needs to move. 
    private IEnumerator SlideBox(string incomingFacade, Material incomingMat)
    {
        if (!isMoving)
        {
            isMoving = true;
            slideSeq = DOTween.Sequence();
            //Debug.Log(incomingFacade);

            //
            switch (incomingFacade)
            {
                case "Z-":
                    lastMovingDir = new Vector3(0, 0, -1);
                    if (!(transform.localPosition.z + moveDistance <= maxDistanceZ * -1))
                    {
                        var mat = incomingMat;
                        slideSeq.Append(
                            transform.DOMove((lastMovingDir * moveDistance + transform.position), slideTime));
                        slideSeq.Join(mat.DOFloat(1f, "_IFade", slideTime));
                        slideSeq.Append(mat.DOFloat(1f, "_IFade", slideTime));
                        slideSeq.Append(mat.DOFloat(0f, "_IFade", 0.1f));
                    }

                    break;


                case "Z+":
                    Debug.Log("Try move");
                    Debug.Log(transform.localPosition.z + moveDistance);
                    lastMovingDir = new Vector3(0, 0, 1);
                    if (transform.localPosition.z + moveDistance <= 0)
                    {
                        var mat = incomingMat;
                        Debug.Log("Do Move");
                        slideSeq.Append(
                            transform.DOMove((lastMovingDir * moveDistance + transform.position), slideTime));
                        slideSeq.Join(mat.DOFloat(1f, "_IFade", slideTime));
                        slideSeq.Append(mat.DOFloat(0f, "_IFade", 0.1f));
                    }

                    break;

                case "X-":
                    lastMovingDir = new Vector3(-1, 0, 0);
                    if (transform.localPosition.x - moveDistance >= 0)
                    {
                        var mat = incomingMat;
                        slideSeq.Append(
                            transform.DOMove((lastMovingDir * moveDistance + transform.position), slideTime));
                        slideSeq.Join(mat.DOFloat(1f, "_IFade", slideTime));
                        slideSeq.Append(mat.DOFloat(0f, "_IFade", 0.1f));
                    }

                    break;

                case "X+":
                    lastMovingDir = new Vector3(1, 0, 0);
                    if (transform.localPosition.x + (moveDistance * 2) <= maxDistanceX)
                    {
                        var mat = incomingMat;
                        slideSeq.Append(
                            transform.DOMove((lastMovingDir * moveDistance + transform.position), slideTime));
                        slideSeq.Join(mat.DOFloat(1f, "_IFade", slideTime));
                        slideSeq.Append(mat.DOFloat(0f, "_IFade", 0.1f));
                    }

                    break;
            }

            yield return slideSeq.WaitForCompletion();
            isMoving = false;
        }
    }

    //Start Position of the box. 
    public void SetPositionAndStopCoroutine(string incomingFacade, Vector3 posWall)
    {
        slideSeq.Kill();
        StopCoroutine(SlideBox(incomingFacade, null));
        transform.localPosition = new Vector3(posWall.x - (lastMovingDir.x * moveDistance), 0,
            posWall.z - (lastMovingDir.z * moveDistance));
        Debug.Log(posWall);
    }
}