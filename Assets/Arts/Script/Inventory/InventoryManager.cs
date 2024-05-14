using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject InventoryMenu;
        private bool menuActivated;

        // Start is called before the first frame update
        void Start()
        {
            menuActivated = InventoryMenu.activeSelf;
        }

        // This method can be called by the button to toggle the inventory
        public void ToggleInventory()
        {
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);
            Time.timeScale = menuActivated ? 0f : 1f; // If menu is activated, set timeScale to 0, else set it to 1
        }

        // This method adds an item to the inventory
        public void AddItem(string itemName, int quantity, Sprite itemsprite)
        {
            // Here you would write code to add the item to the inventory
            // For now, let's just print a message
            Debug.Log("Adding item: " + itemName + ", Quantity: " + quantity);
        }
    }
}




