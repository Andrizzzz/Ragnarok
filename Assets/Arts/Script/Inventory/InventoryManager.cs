using UnityEngine;
using UnityEngine.UI;

namespace Lance
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject InventoryMenu;
        public GameObject CraftingPanel;
        public GameObject buttonToCraftingPanel;
        private bool menuActivated;
        private bool craftingPanelActive;

        public ItemSlot[] itemSlot;
        public ItemSlot[] craftingPanelSlots; // Array of crafting panel slots

        public GameObject craftingPanelSlotPrefab; // Reference to the crafting panel slot prefab

        // Start is called before the first frame update
        void Start()
        {
            menuActivated = InventoryMenu.activeSelf;
            craftingPanelActive = CraftingPanel.activeSelf;
            ToggleButtonVisibility();
            InstantiateCraftingPanelSlots(); // Instantiate crafting panel slots
        }

        // Method to instantiate crafting panel slots
        private void InstantiateCraftingPanelSlots()
        {
            craftingPanelSlots = new ItemSlot[CraftingPanel.transform.childCount];

            for (int i = 0; i < CraftingPanel.transform.childCount; i++)
            {
                GameObject slotObject = Instantiate(craftingPanelSlotPrefab, CraftingPanel.transform.GetChild(i));
                craftingPanelSlots[i] = slotObject.GetComponent<ItemSlot>();
            }
        }

        // This method can be called by the button to toggle the inventory
        public void ToggleInventory()
        {
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);

            if (craftingPanelActive)
            {
                ToggleCraftingPanel();
            }

            Time.timeScale = menuActivated ? 0f : 1f;

            ToggleButtonVisibility();
        }

        private void ToggleButtonVisibility()
        {
            buttonToCraftingPanel.SetActive(menuActivated);
        }

        public void ToggleCraftingPanel()
        {
            craftingPanelActive = !craftingPanelActive;
            CraftingPanel.SetActive(craftingPanelActive);
        }

        public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
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
                if (!itemSlot[i].isFull)
                {
                    itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                    return;
                }
            }
        }

        // Method to add item to the crafting panel
        public void AddItemToCraftingPanel(string itemName, int quantity, Sprite itemSprite, string itemDescription)
        {
            for (int i = 0; i < craftingPanelSlots.Length; i++)
            {
                if (!craftingPanelSlots[i].isFull)
                {
                    craftingPanelSlots[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                    return;
                }
            }

            Debug.LogWarning("Crafting panel is full. Cannot add item.");
        }

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
