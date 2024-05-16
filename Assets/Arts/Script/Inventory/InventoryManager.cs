using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject InventoryMenu;
        public GameObject CraftingPanel; // Reference to the crafting panel
        public GameObject buttonToCraftingPanel; // Reference to the button for crafting panel
        private bool menuActivated;
        private bool craftingPanelActive;

        public ItemSlot[] itemSlot;

        // Start is called before the first frame update
        void Start()
        {
            menuActivated = InventoryMenu.activeSelf;
            craftingPanelActive = CraftingPanel.activeSelf; // Check initial state of crafting panel
            ToggleButtonVisibility(); // Initially hide the button
        }

        // This method can be called by the button to toggle the inventory
        public void ToggleInventory()
        {
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);

            // Close the crafting panel when inventory is toggled
            if (craftingPanelActive)
            {
                ToggleCraftingPanel();
            }

            Time.timeScale = menuActivated ? 0f : 1f; // If menu is activated, set timeScale to 0, else set it to 1

            ToggleButtonVisibility(); // Update button visibility
        }

        // Toggle visibility of the button based on inventory state
        private void ToggleButtonVisibility()
        {
            buttonToCraftingPanel.SetActive(menuActivated); // Show button if inventory is active
        }

        // Method to handle button click for toggling the crafting panel
        public void ToggleCraftingPanel()
        {
            // Toggle the visibility of the crafting panel
            craftingPanelActive = !craftingPanelActive;
            CraftingPanel.SetActive(craftingPanelActive);
        }

        // This method adds an item to the inventory
        public void AddItem(string itemName, int quantity, Sprite itemsprite, string itemDescription)
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull && itemSlot[i].itemName == itemName)
                {
                    itemSlot[i].IncreaseQuantity(quantity);
                    return;
                }
            }

            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false)
                {
                    itemSlot[i].AddItem(itemName, quantity, itemsprite, itemDescription);
                    return;
                }
            }
        }

        // Method to deselect all slots in the inventory
        public void DeselectAllSlots()
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                itemSlot[i].selectedShader.SetActive(false);
                itemSlot[i].thisItemSelected = false;
            }
        }
    }
}
