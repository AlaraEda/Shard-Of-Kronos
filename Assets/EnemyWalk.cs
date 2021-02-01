using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public AudioSource enemyWalkSound;
    private float waitForAudio = 0.5f;
    private bool keepPlaying = true;

    public void SoundEnemy(int num)
    {
        if (num == 1)
        {
            //enemyWalkSound.Play();
            if (keepPlaying)
            {
                var spawnsound = Instantiate(enemyWalkSound, transform);
                new WaitForSeconds(waitForAudio);
            }
            //Instantiate(enemyWalkSound,transform);
            Debug.Log("enemy is walking");
        }
    }
}
