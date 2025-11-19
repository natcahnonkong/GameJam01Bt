using UnityEngine;

public class Moveleft : MonoBehaviour
{
    public float speed = 2f;
    public float leftLimit = -15f;

    void Update()
    {
        if (!Player.GameStarted)
        {

            return;
        }
        

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftLimit)
            Destroy(gameObject);

    }
}
