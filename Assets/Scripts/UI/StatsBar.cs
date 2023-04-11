using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Herohunk
{
    public class StatsBar : MonoBehaviour
    {
        Image fillImageBack;

        Image fillImageFront;

        float currentFillAmount;

        float targetFillAmount;

        Canvas canvas;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
        }

        public void Initialize(float currentValue, float maxValue)
        {
            
        }
    }
}