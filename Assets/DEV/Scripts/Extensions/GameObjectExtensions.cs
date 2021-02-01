using System;
using UnityEngine;

namespace DEV.Scripts.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Execute a method with parameter T if target gameobject contains a component of type T
        /// This method will allow you to manipulate a component without having to check if it exists
        /// The parameter of 'onExistsAction' is the fetched component
        /// </summary>
        /// <param name="go"></param>
        /// <param name="onExistsAction">The function that will run if component exists. The parameter is the target component</param>
        /// <typeparam name="T">Represents the component type</typeparam>
        public static T OnComponentExists<T>(this GameObject go, Action<T> onExistsAction)
        {
            T component = go.GetComponent<T>();
            if (component != null) onExistsAction(component);
            return component;
        }
    }
}
