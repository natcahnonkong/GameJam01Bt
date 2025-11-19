using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public float speed = 2f;
    public float resetX = 10f;   // ตำแหน่งที่ BG หลุดจากซ้าย
    public float startX = 10f;   // ตำแหน่งเริ่มใหม่ (ต่อท้ายอีกอัน)

    void Update()
    {
        if (!Player.GameStarted)
        {
            return;
        }

        // เลื่อน BG ไปทางซ้าย
        transform.position += Vector3.left * speed * Time.deltaTime;

        // ถ้าหลุดจากซ้าย → วาร์ปไปขวาเพื่อ loop
        if (transform.position.x <= resetX)
        {
            Vector3 newPos = new Vector3(startX, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
