using UnityEngine;

namespace Herohunk
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        // 聲明靜態半行實例
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this as T;
        }
    }
}
