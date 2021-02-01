using System.Collections.Generic;
using DEV.Scripts.BowAndArrow;
using DEV.Scripts.Enemy;
using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.AI;

public class PlayerArrowIceAoEColArea : MonoBehaviour
{
    private PlayerArrowIceAoE arrowScript;
    
    private SettingsEditor settings;
    private Settings.BowAndArrowSettingsModel bowAndArrowSettings;

    private List<EnemyBase> slowedEnemies = new List<EnemyBase>();

    private void Awake()
    {
        settings = SettingsEditor.Instance;
        arrowScript = GetComponentInParent<PlayerArrowIceAoE>();
        bowAndArrowSettings = settings.bowAndArrowSettingsModel;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyBase = other.gameObject.GetComponent<EnemyBase>();
        if (enemyBase)
        {
           enemyBase.SetEnemySlow(true);
           slowedEnemies.Add(enemyBase);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemyBase = other.gameObject.GetComponent<EnemyBase>();
        if (enemyBase)
        {
            enemyBase.SetEnemySlow(false);
            slowedEnemies.Remove(enemyBase);

        }
    }

    public void ResetSlowEffect()
    {
        foreach (var e in slowedEnemies)
        {
            e.SetEnemySlow(false);
        }
    }
}