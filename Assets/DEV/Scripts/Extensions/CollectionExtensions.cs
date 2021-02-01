using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DEV.Scripts.Extensions
{
    public static class CollectionExtensions
    {
        public static T RandomElement<T>(this ICollection<T> collection)
        {
            int index = Random.Range(0, collection.Count);
            return collection.ElementAt(index);
        }
    }
}