using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    [CreateAssetMenu]
    public class ItemSO : ScriptableObject
    {
        public string itemName;
        public StatToChange statToChang = new StatToChange();
        public int amountToChangeStat;

        




        public enum StatToChange
        {
            none,
            health
        };
    }
}

