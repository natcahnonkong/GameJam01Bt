using UnityEngine;

public class TakeDmg : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDmg(1);
        }
    }
}
