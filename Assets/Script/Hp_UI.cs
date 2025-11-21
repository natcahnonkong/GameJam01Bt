using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_UI : MonoBehaviour
{
    public GameObject heartPrefab;            // Prefab รูปหัวใจ
    public Transform heartContainer;          // กล่องใส่หัวใจ
    public Player player;       // อ้างอิง player

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        UpdateHearts();
    }

    public void UpdateHearts()

    {
        StartCoroutine(InitHearts());
        // ลบหัวใจทั้งหมดก่อน
        foreach (var h in hearts)
            Destroy(h);
            StartCoroutine(InitHearts());

        hearts.Clear();

        // สร้างหัวใจตามจำนวน HP
        for (int i = 0; i < player.currentHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            hearts.Add(newHeart);
        }

        IEnumerator InitHearts()
        {
            yield return null; // รอ 1 เฟรมเพื่อให้ Player โหลดครบ
            UpdateHearts();
        }
    }
}
