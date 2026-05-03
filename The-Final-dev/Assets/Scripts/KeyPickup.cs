using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject keyIconUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                inventory.hasKey = true;

                if (keyIconUI != null)
                {
                    keyIconUI.SetActive(true);
                }

                Destroy(gameObject);
            }
        }
    }
}