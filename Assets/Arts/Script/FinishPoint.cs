using Lance.CoreSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    public Stats playerStats;
    public InventoryManager inventoryManager;

    public void SwitchToScene(string sceneName)
    {
        // Save data before switching scenes
        playerStats.SaveHealth();
        inventoryManager.SaveInventory();

        // Switch scene
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // go to next level
            SceneController.instance.NextLevel();
        }
    }
}
