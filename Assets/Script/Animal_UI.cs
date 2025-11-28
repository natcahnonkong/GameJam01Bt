using UnityEngine;

public class Animal_UI : MonoBehaviour
{
    public GameObject animalIconPrefab;   // Prefab ไอคอนสัตว์

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        UpdateAnimals();
    }

    public void UpdateAnimals()
    {
        if (player == null) return;

        // ลบของเก่าทั้งหมดก่อน
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // เพิ่มตามจำนวนสัตว์ที่เก็บ
        for (int i = 0; i < player.animalsCollected; i++)
        {
            Instantiate(animalIconPrefab, transform);
            // 👆 ใส่ parent = transform → Horizontal Layout Group จะจัดให้เอง
        }
    }
}
