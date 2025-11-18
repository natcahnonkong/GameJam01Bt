using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public float maxFallSpeed = -10f;
    [Header("Rotation Settings")]
    public float upRotation = 35f;
    public float downRotation = -60f;
    public float rotationLerpSpeed = 5f;
    private Rigidbody2D rb;
    [Header("PlayerStat")]
    private bool isAlive = true;
    public int maxHealth = 1;
    public int currentHealth;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("FlappyPlayerController: Rigidbody2D ไม่ถูกติดกับ Player!");
        }
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDmg(int dmg)
    {
        if (!isAlive) return;

        currentHealth -= dmg;

        Debug.Log("Player took damage. HP = " + currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {

        // คลิก Mouse 0 เพื่อกระโดด
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // ปรับมุมหมุนตามความเร็วแกน Y
        RotateByVelocity();
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        // จำกัดความเร็วตกไม่ให้เร็วเกินไป
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }

    private void Jump()
    {
        // รีเซ็ตความเร็วแกน Y ก่อน แล้วใส่แรงกระโดดขึ้นไป
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void RotateByVelocity()
    {
        // ถ้ากำลังกระโดดขึ้น -> หมุนขึ้น
        // ถ้ากำลังตก -> หมุนลง
        float targetAngle;

        if (rb.linearVelocity.y > 0.1f)
        {
            targetAngle = upRotation;
        }
        else if (rb.linearVelocity.y < -0.1f)
        {
            targetAngle = downRotation;
        }
        else
        {
            targetAngle = 0f;
        }

        // ค่อย ๆ หมุนไปหาเป้าหมาย
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationLerpSpeed * Time.deltaTime
        );
    }

    public void Die()
    {
        isAlive = false;

        Debug.Log("Player Died!");

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
        }


    }
}