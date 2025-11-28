using UnityEngine;

public class AnimalCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().CollectAnimal();
            Destroy(gameObject);
        }
    }
}
