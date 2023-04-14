using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Herohunk
{
    public class StatsBar : MonoBehaviour
    {
        [SerializeField, Header("後方狀態條")] Image fillImageBack;

        [SerializeField, Header("前方狀態條")] Image fillImageFront;

        [SerializeField, Header("是否延遲填充")] bool delayFill = true;
        
        [SerializeField, Header("延遲填充時間")] float fillDelay = 0.5f;

        [SerializeField, Header("狀態條填充速度")] float fillSpeed = 0.1f;

        [Header("當前狀態條值")] float currentFillAmount;

        [Header("目標狀態條值")] protected float targetFillAmount;

        float t;

        WaitForSeconds waitForDelayFill;

        Coroutine bufferedFillingCoroutine;

        Canvas canvas;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;

            waitForDelayFill = new WaitForSeconds(fillDelay);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public virtual void Initialize(float currentValue, float maxValue)
        {
            currentFillAmount = currentValue / maxValue;
            targetFillAmount = currentFillAmount;
            fillImageBack.fillAmount = currentFillAmount;
            fillImageFront.fillAmount = currentFillAmount;
        }

        public void UpdateStats(float currentValue, float maxValue)
        {
            targetFillAmount = currentValue / maxValue;

            if (bufferedFillingCoroutine != null)
            {
                StopCoroutine(bufferedFillingCoroutine);
            }

            // 如果狀態減少
            if(currentFillAmount > targetFillAmount)
            {
                // 前面圖片填充值 = 目標填充值
                fillImageFront.fillAmount = targetFillAmount;
                // 後面圖片填充值慢慢減少
                bufferedFillingCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageBack));

                return;
            }

            // 如果狀態增加
            if(currentFillAmount < targetFillAmount)
            {
                // 後面圖片填充值 = 目標填充值
                fillImageBack.fillAmount = targetFillAmount;
                // 前面圖片填充值慢慢增加
                bufferedFillingCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageFront));
            }
        }

        // 緩沖填充協程
        protected virtual IEnumerator BufferedFillingCoroutine(Image image)
        {
            if (delayFill)
            {
                yield return waitForDelayFill;
            }

            t = 0f;

            while(t < 1f)
            {
                t += Time.deltaTime * fillSpeed;
                currentFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, t);
                image.fillAmount = currentFillAmount;

                yield return null;
            }
        }
    }
}