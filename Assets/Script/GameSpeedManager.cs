using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    public static float SpeedMultiplier = 1f;

    [Header("Speed Up Settings")]
    public float increaseRate = 0.1f;
    public float maxSpeed = 10f;

    void Update()
    {
        if (!Player.GameStarted)
        {
            Debug.Log("ยังไม่เริ่ม SpeedMultiplier = " + GameSpeedManager.SpeedMultiplier);
            return;
        }

        GameSpeedManager.SpeedMultiplier += increaseRate * Time.deltaTime;

        Debug.Log("SpeedMultiplier NOW = " + GameSpeedManager.SpeedMultiplier);
    }

}
