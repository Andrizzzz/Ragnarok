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
            Stats playerStats = player.GetComponent<Stats>();
            if (playerStats != null && statToChange == StatToChange.health)
            {
                playerStats.IncreaseHealth(amountOfChangeStat);
            }
        }
    }

    public enum StatToChange
    {
        none,
        health,
        stamina
    };
}
