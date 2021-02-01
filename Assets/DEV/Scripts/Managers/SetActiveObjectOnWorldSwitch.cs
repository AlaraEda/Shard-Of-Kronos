using DEV.Scripts.Managers;
using UnityEngine;

namespace DEV.Scripts
{
    public class SetActiveObjectOnWorldSwitch : MonoBehaviour
    {
        public GameObject normalObject;
        public GameObject spiritObject;

        void Update()
        {
            //var isNormal = SceneContext.Instance.worldSwitchManager.CurrentLayer == LayerMask.GetMask("PersonA");
            normalObject.SetActive(SceneContext.Instance.worldSwitchManager.worldIsNormal);
            spiritObject.SetActive(!SceneContext.Instance.worldSwitchManager.worldIsNormal);
        }
    }
}