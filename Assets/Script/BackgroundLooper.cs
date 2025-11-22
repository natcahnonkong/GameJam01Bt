using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public float speed = 2f;
    public float resetX = 10f;
    public float startX = 10f;

    [Header("Background Sprites")]
    public Sprite normalBG;
    public Sprite lowHP_BG;          // BG ตอน HP ต่ำ
    public int changeAtHP = 2;       // เปลี่ยน BG เมื่อ HP เหลือเท่านี้

    private SpriteRenderer sr;
    private Player playerRef;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerRef = FindObjectOfType<Player>(); // อ้างถึง Player
    }

    void Update()
    {
        if (!Player.GameStarted)
        {
            return;
        }

        // ⭐ เปลี่ยน BG ตาม HP ของ Player
        if (playerRef != null)
        {
            if (playerRef.currentHealth <= changeAtHP)
                sr.sprite = lowHP_BG;
            else
                sr.sprite = normalBG;
        }

        // เลื่อนพื้นหลังตามปกติ
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= resetX)
        {
            Vector3 newPos = new Vector3(startX, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
