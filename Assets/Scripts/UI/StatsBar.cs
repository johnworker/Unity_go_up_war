using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Herohunk
{
    public class StatsBar : MonoBehaviour
    {
        [SerializeField, Header("��説�A��")] Image fillImageBack;

        [SerializeField, Header("�e�説�A��")] Image fillImageFront;

        [SerializeField, Header("�O�_�����R")] bool delayFill = true;
        
        [SerializeField, Header("�����R�ɶ�")] float fillDelay = 0.5f;

        [SerializeField, Header("���A����R�t��")] float fillSpeed = 0.1f;

        [Header("��e���A����")] float currentFillAmount;

        [Header("�ؼЪ��A����")] protected float targetFillAmount;

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

            // �p�G���A���
            if(currentFillAmount > targetFillAmount)
            {
                // �e���Ϥ���R�� = �ؼж�R��
                fillImageFront.fillAmount = targetFillAmount;
                // �᭱�Ϥ���R�ȺC�C���
                bufferedFillingCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageBack));

                return;
            }

            // �p�G���A�W�[
            if(currentFillAmount < targetFillAmount)
            {
                // �᭱�Ϥ���R�� = �ؼж�R��
                fillImageBack.fillAmount = targetFillAmount;
                // �e���Ϥ���R�ȺC�C�W�[
                bufferedFillingCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageFront));
            }
        }

        // �w�R��R��{
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