using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image maskHealth;
    [SerializeField] private Image effect;
    [Header("Only if has a shield")]
    [SerializeField] private Image maskShield = null;
    private float shrinkSpeed = 1f;
    private float timer = 0;
    private const float TIMER_MAX = 1f;

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (effect.fillAmount >= maskHealth.fillAmount && timer <= 0)
        {
            effect.fillAmount -= shrinkSpeed * Time.deltaTime;
        }
    }

    public void SetHealth(float amount)
    {
        timer = TIMER_MAX;
        maskHealth.fillAmount = amount;
    }

    public void SetShieldHealth(float amount)
    {
        maskShield.fillAmount = amount;
    }
}
