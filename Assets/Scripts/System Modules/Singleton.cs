using UnityEngine;

namespace Herohunk
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        // �n���R�A�b����
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this as T;
        }
    }
}
