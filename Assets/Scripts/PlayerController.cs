using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Camera cam;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] GameObject projectile;
    [SerializeField] float firePeriod = 0.2f;
    [SerializeField] GameObject explosionAnim;
    [SerializeField] BtnManager sceneManager;

    private IEnumerator fireCoroutine;
    Rigidbody2D projectileRb;
    float xMin;
    float yMin;
    float xMax;
    float yMax;
    float width;
    float height;


    void Start()
    {
        width = spriteRenderer.bounds.size.x / 2;
        height = spriteRenderer.bounds.size.y / 2;
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + width;
        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + height;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 1, 0)).x - width;
        yMax = cam.ViewportToWorldPoint(new Vector3(1, 1, 0)).y - height;
    }

    void Update()
    {
        Move();
        StartFire();
    }

    private void Move()
    {
        Vector2 mouseWorldPos = GetMousePosition();
        float rotation = GetAimRotation(mouseWorldPos); // get the angle of rotation the player need to face the cursor
        rb.rotation = rotation;

        // Previously : float x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //              float y = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        //              transform.position = new Vector3(Mathf.Clamp(transform.position.x + x, xMin, xMax), Mathf.Clamp(transform.position.y + y, yMin, yMax));
        // But i changed it to have the movement be relative to the cursor rather than to the world axis
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime; // transform.up move on the Green axis
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime; // transform.right move on the red axis
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
        }

        //Check if the player is "outside" of the screen and put it back right against the border (can't use Mathf.Clamp because of the transform.Up/Right)
        if (transform.position.x < xMin)
        {
            transform.position = new Vector3(xMin, transform.position.y);
        }
        else if (transform.position.x > xMax)
        {
            transform.position = new Vector3(xMax, transform.position.y);
        }
        if (transform.position.y < yMin)
        {
            transform.position = new Vector3(transform.position.x, yMin);
        }
        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax);
        }
    }


    // return the angle from the player position to the cursor
    private float GetAimRotation(Vector2 mouseWorldPos)
    {

        Vector2 aimPosition = mouseWorldPos - rb.position;
        float aimAngle = (Mathf.Atan2(aimPosition.y, aimPosition.x) * Mathf.Rad2Deg) - 90f;
        return aimAngle;
    }

    // return the mouse position in the world (not the screen position)
    private Vector2 GetMousePosition()
    {
        Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        return mouseWorldPosition;
    }

    private void StartFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = FireContinously();
            StartCoroutine(fireCoroutine);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }
    private IEnumerator FireContinously()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(firePeriod);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, transform.rotation);
        projectileRb = laser.GetComponent<Rigidbody2D>();
        projectileRb.velocity = laser.transform.up * laser.GetComponent<PlayerLaser>().Speed;
    }

    public void Die()
    {
        GameObject explosion = Instantiate(explosionAnim, transform.position, Quaternion.identity);
        explosion.transform.SetParent(null);
        sceneManager.Action("died");
        Destroy(explosion, 2.01f);
        Destroy(gameObject);
    }
}
