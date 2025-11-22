using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Hp_UI : MonoBehaviour
{
    public GameObject heartPrefab;            // Prefab รูปหัวใจ
    public Transform heartContainer;          // กล่องใส่หัวใจ
    public Player player;       // อ้างอิง player

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        
    }

    public void UpdateHearts()
    {
        // ลบหัวใจทั้งหมดก่อน
        foreach (var h in hearts)
            Destroy(h);

        hearts.Clear();

        // สร้างหัวใจตามจำนวน HP
        for (int i = 0; i < player.currentHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            hearts.Add(newHeart);
        }
    }
}
