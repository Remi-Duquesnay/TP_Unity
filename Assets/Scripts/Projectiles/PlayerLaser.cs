using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private int damage = 10;

    public int Damage { get => damage; }
    public float Speed { get => speed; }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DamageDealer entity = collision.gameObject.GetComponent<DamageDealer>();
            entity.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}
