using UnityEngine;

public class Accel : MonoBehaviour
{
    public static float SpeedMultiplier = 1f;

    [Header("Speed Up Settings")]
    public float increaseRate = 0.1f;    // เพิ่ม 0.1 ต่อวินาที
    public float maxSpeed = 10f;          // ไม่ให้เร็วเกิน 3x

    void Update()
    {
        if (!Player.GameStarted) return;

        if (SpeedMultiplier < maxSpeed)
        {
            SpeedMultiplier += increaseRate * Time.deltaTime;
        }
    }
}
