using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireTriple : MonoBehaviour, IEnemyFire
{
    [SerializeField] public GameObject projectile;
    Rigidbody2D projectileRb;

    public void Fire()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject laser = Instantiate(projectile, transform.position, Quaternion.AngleAxis(((transform.rotation.eulerAngles.z - 15f + (i * 15))-180), Vector3.forward));
            projectileRb = laser.GetComponent<Rigidbody2D>();
            projectileRb.velocity = laser.transform.up * laser.GetComponent<EnemyLaser>().Speed;
        }
    }
}
