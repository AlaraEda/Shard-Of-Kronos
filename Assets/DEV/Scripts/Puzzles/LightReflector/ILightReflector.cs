using System.Collections.Generic;
using UnityEngine;

namespace DEV.Scripts
{
    public interface ILightReflector
    {
        IList<ILightReflector> LightSources { get; }
        ILightReflector LightTarget { get; set; }
        
        GameObject EmitterGameObject { get; }
    }
}