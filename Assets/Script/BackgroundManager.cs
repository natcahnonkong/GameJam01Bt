using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("BG Prefabs (ลำดับการเปลี่ยน)")]
    public GameObject BG_Level1;   // ปกติ
    public GameObject BG_Level2;   // เปลี่ยนครั้งที่ 1
    public GameObject BG_Level3;   // เปลี่ยนครั้งที่ 2

    [Header("เปลี่ยน BG เมื่อ HP เหลือเท่านี้")]
    public int changeToLevel2_HP = 2;
    public int changeToLevel3_HP = 1;

    private Player player;

    private GameObject currentBG1;
    private GameObject currentBG2;

    [Header("ตำแหน่งเริ่ม BG1 + BG2")]
    public Vector3 bg1StartPos = new Vector3(0, 0, 0);
    public Vector3 bg2StartPos = new Vector3(20f, 0, 0);

    private int currentLevel = 1;


    void Start()
    {
        player = FindObjectOfType<Player>();


    }

    void Update()
    {
        if (player == null) return;

        // เปลี่ยนเป็น Level 2
        if (player.currentHealth <= changeToLevel2_HP && currentLevel == 1)
        {
            ReplaceBG(BG_Level2);
            currentLevel = 2;
        }

        // เปลี่ยนเป็น Level 3
        if (player.currentHealth <= changeToLevel3_HP && currentLevel == 2)
        {
            ReplaceBG(BG_Level3);
            currentLevel = 3;
        }
    }

    void SpawnBG(GameObject prefab)
    {
        currentBG1 = Instantiate(prefab, bg1StartPos, Quaternion.identity);
        currentBG2 = Instantiate(prefab, bg2StartPos, Quaternion.identity);
    }

    void ReplaceBG(GameObject newPrefab)
    {
        if (currentBG1 != null) Destroy(currentBG1);
        if (currentBG2 != null) Destroy(currentBG2);

        SpawnBG(newPrefab);
    }
}
