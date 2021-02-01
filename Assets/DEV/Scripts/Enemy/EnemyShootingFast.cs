using DEV.Scripts.Enemy;
using DEV.Scripts.Managers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyShootingFast : EnemyShooting
{
    [Header("Set Enemy Scriptable Object")] [SerializeField]
    private SO_EnemyShooting enemyBaseSo;

    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    

    public override void Awake()
    {
        base.Awake();
        SetBaseScriptableObjectsPrefs(enemyBaseSo);
        SetChildScriptableObjectsPrefs();
        DrawGizmos = true;
    }

    private void SetChildScriptableObjectsPrefs()
    {
        ShotSpeed = enemyBaseSo.shotSpeed;
    }

    protected override void DeleteEnemy()
    {
        SceneContext.Instance.ActiveEnemies.Remove(this);
        meshRenderer.materials[0]
            .DOFloat(0, Shader.PropertyToID("_fadeRock"), 2)
            .OnComplete(() => Destroy(gameObject));
    }

}