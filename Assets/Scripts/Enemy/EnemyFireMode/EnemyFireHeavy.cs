using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireHeavy : MonoBehaviour, IEnemyFire
{
    [SerializeField] public GameObject projectile;
    Rigidbody2D projectileRb;

    public void Fire()
    {
        GameObject heavyLaser = Instantiate(projectile, transform.position, Quaternion.AngleAxis((transform.rotation.eulerAngles.z - 180), Vector3.forward));
        projectileRb = heavyLaser.GetComponent<Rigidbody2D>();
        projectileRb.velocity = heavyLaser.transform.up * heavyLaser.GetComponent<EnemyHeavyLaser>().Speed;
    }
}
