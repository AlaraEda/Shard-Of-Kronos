using UnityEngine;

namespace DEV.Scripts.Input
{
    /// <summary>
    /// This Interface handles the global data of Value (x,y,z) and Name(the name of the script)
    /// </summary>
    public interface IMovementModifier
    {
        //returns vector3 Value
        Vector3 Value { get; }
        string Name { get; set; }

    }
}