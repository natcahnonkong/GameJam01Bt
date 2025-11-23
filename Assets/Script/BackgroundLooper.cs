using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public float speed = 2f;
    public float resetX = -42f;
    public float startX = 42f;

    [Header("BG Prefabs (3 Levels)")]
    public GameObject BG_Level1;
    public GameObject BG_Level2;
    public GameObject BG_Level3;

    [Header("Change BG when HP lower than")]
    public int changeToLevel2 = 2;
    public int changeToLevel3 = 1;

    private Player player;
    private int currentLevel = 1;

    private SpriteRenderer sr;


    void Start()
    {
        player = FindObjectOfType<Player>();
        sr = GetComponent<SpriteRenderer>();

        // ถ้า BG ปัจจุบันไม่มี SpriteRenderer ให้เพิ่มอัตโนมัติ
        if (sr == null)
            sr = gameObject.AddComponent<SpriteRenderer>();

        // เริ่มด้วย BG Level1
        ApplyPrefab(BG_Level1);
        currentLevel = 1;
    }


    void Update()
    {
        if (!Player.GameStarted) return;

        // เปลี่ยนเป็น Level 2
        if (player.currentHealth <= changeToLevel2 && currentLevel == 1)
        {
            ApplyPrefab(BG_Level2);
            currentLevel = 2;
        }

        // เปลี่ยนเป็น Level 3
        if (player.currentHealth <= changeToLevel3 && currentLevel == 2)
        {
            ApplyPrefab(BG_Level3);
            currentLevel = 3;
        }

        // Move BG
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= resetX)
        {
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }


    /// <summary>
    /// เปลี่ยน BG prefab โดยใช้ Sprite และลอกค่าจำเป็นจาก Prefab
    /// </summary>
    void ApplyPrefab(GameObject prefab)
    {
        if (prefab == null) return;

        SpriteRenderer prefabSR = prefab.GetComponent<SpriteRenderer>();

        if (prefabSR != null)
        {
            sr.sprite = prefabSR.sprite;
            sr.color = prefabSR.color;
            sr.flipX = prefabSR.flipX;
            sr.flipY = prefabSR.flipY;
            transform.localScale = prefab.transform.localScale;
        }
    }
}
