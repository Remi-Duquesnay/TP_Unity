using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave")]
public class EnemyWave : ScriptableObject
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemyCount;
    [SerializeField] private GameObject movePattern;
    [SerializeField] private float enemyInterval;


    public GameObject Enemy => enemy;
    public int EnemyCount => enemyCount;
    public GameObject MovePattern => movePattern;
    public float EnemyInterval => enemyInterval;
}
