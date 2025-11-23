using UnityEngine;

public class Moveleft : MonoBehaviour
{
    public float speed = 2f;
    public float leftLimit = -15f;

    void Update()
    {
        if (!Player.GameStarted) return;

        float currentSpeed = speed * GameSpeedManager.SpeedMultiplier;

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (transform.position.x < leftLimit)
            Destroy(gameObject);
    }
}
