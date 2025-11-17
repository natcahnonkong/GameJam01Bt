using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;   // แรงกระโดดตอนคลิก
    [SerializeField] private float maxFallSpeed = -10f; // ความเร็วตกสูงสุด (ป้องกันตกเร็วเกินไป)

    [Header("Rotation Settings")]
    [SerializeField] private float upRotation = 35f;    // องศาเงยหัวตอนกระโดด
    [SerializeField] private float downRotation = -60f; // องศาก้มหน้าตอนตก
    [SerializeField] private float rotationLerpSpeed = 5f; // ความเร็วปรับหมุน

    private Rigidbody2D rb;
    private bool isAlive = true; // เผื่ออนาคตจะใช้ตอนชนตาย

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("FlappyPlayerController: Rigidbody2D ไม่ถูกติดกับ Player!");
        }
    }

    private void Update()
    {
        if (!isAlive) return;

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
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }

    private void Jump()
    {
        // รีเซ็ตความเร็วแกน Y ก่อน แล้วใส่แรงกระโดดขึ้นไป
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void RotateByVelocity()
    {
        // ถ้ากำลังกระโดดขึ้น -> หมุนขึ้น
        // ถ้ากำลังตก -> หมุนลง
        float targetAngle;

        if (rb.velocity.y > 0.1f)
        {
            targetAngle = upRotation;
        }
        else if (rb.velocity.y < -0.1f)
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

    // ถ้าอยากให้ตายเมื่อชนอะไรบางอย่าง ใช้ OnCollisionEnter2D หรือ OnTriggerEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // isAlive = false;
        // rb.velocity = Vector2.zero;
        // TODO: ใส่โค้ด Game Over หรือ Restart ที่นี่
    }
}
