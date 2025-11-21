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
    public Sprite transformedSprite;   // ⭐ Sprite ใหม่
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



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0;   // รอคลิกแรกก่อนถึงจะตก
    }

    private void Start()
    {
        currentHealth = maxHealth;
        Hp_UI = FindObjectOfType<Hp_UI>();

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

        // ช่วงล็อค Y ห้ามกระโดด
        if (isYLocked) return;

        if (Input.GetMouseButtonDown(0))
            Jump();
    }


    // ---------------------------------------------------
    // START GAME
    // ---------------------------------------------------
    void StartGame()
    {
        GameStarted = true;
        rb.gravityScale = 2f;
        Jump();
        Hp_UI ui = FindObjectOfType<Hp_UI>();
        if (ui != null)
            ui.UpdateHearts();
    }

    // ---------------------------------------------------
    // JUMP
    // ---------------------------------------------------
    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }


    // ---------------------------------------------------
    // DAMAGE SYSTEM
    // ---------------------------------------------------
    public void TakeDamage(int dmg)
    {
        if (isInvincible) return;

        currentHealth -= dmg;

        // ⭐ อัปเดต UI หัวใจ
        if (Hp_UI != null)
            Hp_UI.UpdateHearts();

        // เปลี่ยนสไปรต์เมื่อถึง HP ที่กำหนด
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
    // ⭐ เปลี่ยน Sprite
    // ---------------------------------------------------
    void TransformSprite()
    {
        if (transformedSprite != null)
        {
            sr.sprite = transformedSprite;
        }
    }


    // ---------------------------------------------------
    // INVINCIBLE + BLINK
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
        rb.linearVelocity = Vector2.zero;

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

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        // เปิด GameOverUI
        GameOverUI ui = FindObjectOfType<GameOverUI>();
        if (ui != null)
            ui.ShowGameOver();

        // ปิดสคริปต์เพื่อไม่ให้ขยับเอง
        this.enabled = false;
    }
}
