using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject InventoryMenu;
        private bool menuActivated;

        public void ToggleInventoryMenu()
        {
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);
            Time.timeScale = menuActivated ? 0 : 1; // Pause time when inventory is activated, resume when deactivated
        }

        // Start is called before the first frame update
        void Start()
        {
            // You can add initialization code here if needed
        }

        // Update is called once per frame
        void Update()
        {
            // This script no longer handles keyboard input
        }

        // AddItem method to add an item to the inventory
        public void AddItem(string itemName, int quantity, Sprite itemSprite)
        {
            Debug.Log("itemName = " + itemName + " quantity = " + quantity + " itemSprite = " + itemSprite);
        }
    }
}

