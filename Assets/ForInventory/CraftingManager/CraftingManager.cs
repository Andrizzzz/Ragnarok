using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class CraftingManager : MonoBehaviour
    {
        private Item currentItem;
        public Image customCursor;
        public void OuMouseDownItem(Item item)
        {
            if(currentItem != null)
            {
                currentItem = item;
                customCursor.gameObject.SetActive(true);
                customCursor.sprite = currentItem.GetComponent<Image>().sprite;
            }
        }
    }
}
