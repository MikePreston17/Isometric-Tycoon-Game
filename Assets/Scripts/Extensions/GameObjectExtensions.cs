using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    internal static class GameObjectExtensions
    {
        public static T GetAbstract<T>(this GameObject gameObject) where T : class
        {
            return gameObject.GetComponents<T>().FirstOrDefault();
        }

        public static T GetInterface<T>(this GameObject gameObject) where T : class
        {
            var type = typeof(T);

            if (!type.IsInterface)
            {
                Debug.LogError(type.ToString() + ": is not an actual interface!");
                return null;
            }

            return gameObject.GetComponents<T>().FirstOrDefault();
        }

        public static IEnumerable GetInterfaces<T>(this GameObject gameObject) where T : class
        {
            var type = typeof(T);

            if (!type.IsInterface)
            {
                Debug.LogError(type.ToString() + ": is not an actual interface!");
                return Enumerable.Empty<T>();
            }

            return gameObject.GetComponents<T>();
        }
    }
}
