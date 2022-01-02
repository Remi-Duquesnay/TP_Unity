using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void Spawn(GameObject enemy, Transform SpawnPoint)
    {
        GameObject entity = Instantiate(enemy, SpawnPoint);
        entity.transform.SetParent(null);
    }
}
