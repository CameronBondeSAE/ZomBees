using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AlienEggLight : MonoBehaviour
{
    public Light pulseLight;
    
    public Color lightColor;
    private Color oldColor;

    public AlienEggPulse eggPulse;

    private float timePulsing;
    private int time;
    private float timeUntilHatch;

    private bool pulsing;

    private void OnEnable()
    {
        eggPulse = GetComponentInParent<AlienEggPulse>();
        timeUntilHatch = eggPulse.timeUntilHatch;
        lightColor = Color.green;

        eggPulse.TimesUpEvent += TimesUp;
        eggPulse.SafeEvent += Safe;

        pulsing = true;
        StartCoroutine(PulseLight());
    }

    private void TimesUp()
    {
        pulsing = false; 
        lightColor = Color.black;
        TweenToLight();
    }

    private void Safe()
    {
        pulsing = false;
        lightColor = Color.clear;
        TweenToLight();
    }

    private IEnumerator PulseLight()
    {
        oldColor = Color.clear;

        while (pulsing)
        {
            timePulsing = eggPulse.timePulsing;
            time = eggPulse.time;
            float scaledByAmount = eggPulse.scaledByAmount;

            float pulseDuration = Mathf.Lerp(0.1f, timePulsing, time / timeUntilHatch);
            float lightDuration = pulseDuration * 2f;

            if (time / timeUntilHatch > 0.75f)
            {
                lightColor = Color.green;
            }
            else if (time / timeUntilHatch > 0.5f)
            {
                lightColor = new Color(1f, 0.5f, 0f);
            }
            else
            {
                lightColor = Color.red;
            }

            if (oldColor != lightColor)
            {
                TweenToLight();
            }

            transform.DOScale(transform.localScale * scaledByAmount, pulseDuration) // scale up
                .SetEase(Ease.OutQuad);
        
            yield return new WaitForSeconds(pulseDuration);
        
            transform.DOScale(transform.localScale / scaledByAmount, pulseDuration) // scale down
                .SetEase(Ease.InQuad);
        
            yield return new WaitForSeconds(pulseDuration);
        }
    }

    public void TweenToLight()
    {
        pulseLight.DOColor(lightColor, 2).SetEase(Ease.OutQuad);
        oldColor = lightColor;
    }

    private void OnDisable()
    {
        eggPulse.TimesUpEvent -= TimesUp;
        eggPulse.SafeEvent -= Safe;
    }
}
