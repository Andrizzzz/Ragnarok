using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
    public class LootTables 
    {
    public GameObject itemPrefabs;

    [Range(0, 100)] public float dropChance;
    }

