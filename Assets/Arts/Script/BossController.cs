using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject portal; // Reference to the portal GameObject in the Inspector

    // Other boss-related variables and functions...

    // Call this function when the boss is defeated
    public void DefeatBoss()
    {
        // Check if the portal GameObject is assigned
        if (portal != null)
        {
            // Activate the portal
            portal.SetActive(true);
        }
        else
        {
            Debug.LogError("Portal GameObject is not assigned!");
        }

        // Destroy the boss GameObject
        Destroy(gameObject);
    }
}
