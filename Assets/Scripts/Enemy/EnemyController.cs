using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float firePeriodMinTime = 0.5f;
    [SerializeField] private float firePeriodMaxTime = 1.5f;
    [SerializeField] private GameObject explosionAnim;

    [Header("Points given for killing it")]
    [SerializeField] private int scorePoints;

    WaveController waveController;
    private IEnumerator fireCoroutine;
    private GameObject[] waypoints;
    private int currentWaypointTarget;
    private IEnemyFire enemyFire;
    private EnemyMovement movement;
    private GameObject target;
    private Rigidbody2D targetRb;
    private Vector2 targetPos;
    private Rigidbody2D rb;
    private bool coroutineRunning = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        waypoints = GameObject.FindGameObjectsWithTag("WaveWaypoint");
        waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
    }


    void Start()
    {
        enemyFire = GetComponent<IEnemyFire>();
        movement = GetComponent<EnemyMovement>();
        currentWaypointTarget = 0;
        target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
            targetRb = target.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        currentWaypointTarget = movement.MoveToNextWaypoint(moveSpeed, waypoints, currentWaypointTarget);
        if (targetRb != null)
        {
            targetPos = targetRb.position;
            rb.rotation = GetAimRotation(targetPos);
        }
        else
        {
            if (coroutineRunning)
                StopCoroutine(fireCoroutine);
        }

    }

    private void OnBecameVisible()
    {
        fireCoroutine = FireContinuously();
        StartCoroutine(fireCoroutine);
    }

    IEnumerator FireContinuously()
    {
        coroutineRunning = true;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(firePeriodMinTime, firePeriodMaxTime)); // I yield before doing the action otherwise it start shooting before it is fully entered the screen.
            enemyFire.Fire();
        }
    }

    private float GetAimRotation(Vector2 targetPos)
    {
        Vector2 aimPosition = targetPos - rb.position;
        float aimAngle = (Mathf.Atan2(aimPosition.y, aimPosition.x) * Mathf.Rad2Deg) + 90f;
        return aimAngle;
    }

    public void Die()
    {
        waveController.removeEnemy();
        ScoreManager.Instance.AddToScore(scorePoints);
        GameObject explosion = Instantiate(explosionAnim, transform.position, Quaternion.identity);
        explosion.transform.SetParent(null);
        Destroy(gameObject);
        Destroy(explosion, 2.01f);
    }
}
