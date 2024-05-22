using Lance.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public AttributesToChange attributesToChange = new AttributesToChange();

    public int amountOfChangeStat;
    public int amountOfChangeAttributes;

    public void UseItem()
    {
        Debug.Log("Using item: " + itemName);
        if (statToChange == StatToChange.health)
        {
            GameObject.Find("Stats").GetComponent<Stats>().IncreaseHealth(amountOfChangeStat);
        }
    }

    public enum StatToChange 
    { 
        none,
        health,
        stamina
     };

    public enum AttributesToChange
    {
        none,
        Strength,
        Defense,
        Agility
        
    };

}
