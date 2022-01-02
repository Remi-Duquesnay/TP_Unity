using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Canvas needed only for enemies")]
    [SerializeField] private GameObject canvas;
    [SerializeField] HealthBar healthBar;
    [SerializeField] private float maxHealth = 0;
    [Header("If Has Shield = false then maxShieldHealth should be \"0\"")]
    [SerializeField] private bool hasShield = false;
    [SerializeField] private float maxShieldHealth = 0;
    [SerializeField] private GameObject shield;
    private CircleCollider2D shieldCollider;

    [SerializeField] private GameObject shieldLayer1;
    [SerializeField] private GameObject shieldLayer2;
    [SerializeField] private GameObject shieldLayer3;
    private SpriteRenderer shielLayer1Sr;
    private SpriteRenderer shielLayer2Sr;
    private SpriteRenderer shielLayer3Sr;

    private float health;
    private float shieldHealth;


    private void Start()
    {
        health = maxHealth;
        shieldHealth = maxShieldHealth;
        healthBar.SetHealth((1 / maxHealth) * health);
        if (hasShield)
        {
            healthBar.SetShieldHealth((1 / maxShieldHealth) * shieldHealth);
            shieldCollider = shield.GetComponent<CircleCollider2D>();
            shielLayer1Sr = shieldLayer1.GetComponent<SpriteRenderer>();
            shielLayer2Sr = shieldLayer2.GetComponent<SpriteRenderer>();
            shielLayer3Sr = shieldLayer3.GetComponent<SpriteRenderer>();

        }

        if (gameObject.CompareTag("Enemy"))
            canvas.GetComponent<Canvas>().enabled = false;

    }

    public void ApplyDamage(int amount)
    {
        if (hasShield && shieldHealth > 0)
        {
            shieldHealth -= amount;
            if(shieldHealth <= (maxShieldHealth / 3)*2)
            {
                shielLayer3Sr.enabled = false;
            }
            
            if (shieldHealth <= (maxShieldHealth / 3))
            {
                shielLayer2Sr.enabled = false;
            }
            
            if (shieldHealth <= 0)
            {
                shieldCollider.enabled = false;
                shielLayer1Sr.enabled = false;
            }
                

            healthBar.SetShieldHealth((1 / maxShieldHealth) * shieldHealth);
        }
        else
        {
            health -= amount;
        }


        if (health != maxHealth & gameObject.CompareTag("Enemy"))
        {
            canvas.GetComponent<Canvas>().enabled = true;
        }

        healthBar.SetHealth((1 / maxHealth) * health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DamageDealer>() != null && !collision.gameObject.CompareTag(gameObject.tag))
        {
            if (hasShield && shieldHealth > 0)
            {
                shieldHealth = 0;
                shieldCollider.enabled = false;
                shielLayer1Sr.enabled = false;
                shielLayer2Sr.enabled = false;
                shielLayer3Sr.enabled = false;
            }
            else
            {
                Die();
            }
        }

    }

    private void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<EnemyController>().Die();
        }
        else if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<PlayerController>().Die();
        }

    }
}
