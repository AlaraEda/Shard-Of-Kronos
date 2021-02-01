using UnityEngine;

namespace DEV.Scripts.BowAndArrow
{
    public class PlayerArrowIceAoE : PlayerArrowBase
    {
        private SphereCollider aoeEffectCollider;
        private PlayerArrowIceAoEColArea colAreaScript;
        private ParticleSystem iceParticles;

        public override void Awake()
        {
            base.Awake();
            ArrowType = ArrowType.IceAoE;
            ArrowDamage = 25.0f;
            iceParticles = GetComponentInChildren<ParticleSystem>();
            aoeEffectCollider = transform.GetChild(1).GetChild(2).GetComponent<SphereCollider>();
            colAreaScript = GetComponentInChildren<PlayerArrowIceAoEColArea>();
            aoeEffectCollider.gameObject.SetActive(false);
            iceParticles.gameObject.SetActive(false);
        }

        public void StartIceAoEEffect()
        {
            aoeEffectCollider.gameObject.SetActive(true);
            iceParticles.gameObject.SetActive(true);
            Invoke(nameof(StopIceAoEEffect), BowAndArrowSettings.iceAoEArrowActiveTime);
        }

        private void StopIceAoEEffect()
        {
            aoeEffectCollider.gameObject.SetActive(false);
            iceParticles.gameObject.SetActive(false);
            colAreaScript.ResetSlowEffect();
        }
    }
}