using UnityEngine;
using UnityEngine.SceneManagement;

public class LockedDoor : MonoBehaviour
{
    public string nextSceneName = "Level2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.hasKey)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("Door is locked. Find the key.");
            }
        }
    }
}