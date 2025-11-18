using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    public GameObject Obj;
    public float spawnInterval = 1.5f;
    public float minY = -1f;
    public float maxY = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    void SpawnPipe()
    {
        float y = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(transform.position.x, y, 0);
        Instantiate(Obj, spawnPos, Quaternion.identity);
    }
}