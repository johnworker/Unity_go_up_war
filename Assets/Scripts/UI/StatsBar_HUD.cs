using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Herohunk
{
    public class StatsBar_HUD : StatsBar
    {
        [SerializeField] TextMeshProUGUI percentText;

        private void SetPercentText()
        {
            percentText.text = Mathf.RoundToInt(targetFillAmount * 100f) + "%";
        }

        public override void Initialize(float currentValue, float maxValue)
        {
            base.Initialize(currentValue, maxValue);
            SetPercentText();
        }

        protected override IEnumerator BufferedFillingCoroutine(Image image)
        {
            SetPercentText();
            return base.BufferedFillingCoroutine(image);
        }
    }
}