using Lance.CoreSystem;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public StatToChange statToChange;
    public int amountOfChangeStat;

    public void UseItem()
    {
        Debug.Log("Using item: " + itemName);

        // Find the player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Stats playerStats = player.GetComponentInChildren<Stats>();
            if (playerStats != null && statToChange == StatToChange.health)
            {
                Debug.Log("Player Stats found.");
                playerStats.IncreaseHealth(amountOfChangeStat);
            }
            else
            {
                Debug.LogWarning("Player Stats component not found or statToChange is not health.");
            }
        }
        else
        {
            Debug.LogWarning("Player GameObject not found.");
        }
    }


    public enum StatToChange
    {
        none,
        health,
        stamina
    };
}
