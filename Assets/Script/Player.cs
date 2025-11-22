using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 5f;

    [Header("Start Game Settings")]
    public static bool GameStarted = false;

    [Header("Health Settings")]
    public int maxHealth = 3;
    public int transformAtHP = 2;
    public Sprite transformedSprite;
    public int currentHealth;

    [Header("Invincible Settings")]
    public float invincibleDuration = 1f;
    public float blinkSpeed = 0.1f;
    private bool isInvincible = false;

    [Header("Reset Y Settings")]
    public float resetY = 0f;
    public float lockYDuration = 1f;
    public bool isYLocked = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Hp_UI Hp_UI;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip jump_sfx;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rb.gravityScale = 0;   // ยังไม่ตกจนกว่าจะคลิกครั้งแรก
    }

    private void Start()
    {
        currentHealth = maxHealth;
        Hp_UI = FindObjectOfType<Hp_UI>();

        // อัปเดตหัวใจตอนเริ่มฉาก
        if (Hp_UI != null)
            Hp_UI.UpdateHearts();

        // รีเซตสถานะต่างๆ เมื่อเริ่ม Scene
        isInvincible = false;
        isYLocked = false;
    }

    private void Update()
    {
        // ยังไม่เริ่มเกม → รอคลิกแรก
        if (!GameStarted)
        {
            if (Input.GetMouseButtonDown(0))
                StartGame();
            return;
        }

        // ระหว่างล็อค Y ห้ามกระโดด
        if (isYLocked) return;

        // กดเพื่อกระโดด
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }


    // ---------------------------------------------------
    // START GAME
    // ---------------------------------------------------
    void StartGame()
    {
        GameStarted = true;
        rb.gravityScale = 2f;

        Jump();  // กระโดดครั้งแรกเมื่อเริ่มเกม

        // เล่นเสียงตอนเริ่มเกมด้วย
        if (audioSource != null && jump_sfx != null)
            audioSource.PlayOneShot(jump_sfx);

        if (Hp_UI != null)
            Hp_UI.UpdateHearts();
    }


    // ---------------------------------------------------
    // JUMP
    // ---------------------------------------------------
    void Jump()
    {
        Debug.Log("JUMP CALLED");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        if (audioSource == null)
            Debug.Log("AUDIO SOURCE = NULL");
        if (jump_sfx == null)
            Debug.Log("JUMP SFX = NULL");

        audioSource.Play();
    }



    // ---------------------------------------------------
    // DAMAGE SYSTEM
    // ---------------------------------------------------
    public void TakeDamage(int dmg)
    {
        if (isInvincible) return;

        currentHealth -= dmg;

        if (Hp_UI != null)
            Hp_UI.UpdateHearts();

        if (currentHealth == transformAtHP)
            TransformSprite();

        if (currentHealth > 0)
        {
            StartCoroutine(InvincibleRoutine());
            StartCoroutine(ResetAndLockY());
        }
        else
        {
            Die();
        }
    }


    // ---------------------------------------------------
    // เปลี่ยน Sprite เมื่อ HP ลดถึงค่าที่กำหนด
    // ---------------------------------------------------
    void TransformSprite()
    {
        if (transformedSprite != null)
            sr.sprite = transformedSprite;
    }


    // ---------------------------------------------------
    // INVINCIBLE + กระพริบ
    // ---------------------------------------------------
    IEnumerator InvincibleRoutine()
    {
        isInvincible = true;

        float timer = 0f;

        while (timer < invincibleDuration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkSpeed);
            timer += blinkSpeed;
        }

        sr.enabled = true;
        isInvincible = false;
    }


    // ---------------------------------------------------
    // RESET Y + LOCK Y
    // ---------------------------------------------------
    IEnumerator ResetAndLockY()
    {
        isYLocked = true;

        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        transform.position = new Vector3(transform.position.x, resetY, transform.position.z);

        yield return new WaitForSeconds(lockYDuration);

        rb.gravityScale = 2f;
        isYLocked = false;
    }


    // ---------------------------------------------------
    // DIE
    // ---------------------------------------------------
    void Die()
    {
        Debug.Log("Player Died!");

        GameStarted = false;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        GameOverUI ui = FindObjectOfType<GameOverUI>();
        if (ui != null)
            ui.ShowGameOver();

        this.enabled = false;
    }
}
