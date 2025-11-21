using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    [Header("Pipe Prefabs")]
    public GameObject[] pipePrefabs;   // ⭐ เก็บท่อหลายแบบใน array

    [Header("Spawn Settings")]
    public float spawnInterval = 1.5f;
    public float minY = -1f;
    public float maxY = 2f;

    private float timer = 0f;

    void Update()
    {
        // เกมยังไม่เริ่ม → หยุด spawn
        if (!Player.GameStarted) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandomPipe();
            timer = 0f;
        }
    }

    void SpawnRandomPipe()
    {
        if (pipePrefabs.Length == 0) return;

        // ⭐ เลือกท่อสุ่ม 1 อัน
        int index = Random.Range(0, pipePrefabs.Length);
        GameObject randomPipe = pipePrefabs[index];

        // สุ่มตำแหน่ง Y
        float y = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, y, 0);

        // สร้างท่อแบบสุ่ม
        Instantiate(randomPipe, spawnPos, Quaternion.identity);
    }
}
