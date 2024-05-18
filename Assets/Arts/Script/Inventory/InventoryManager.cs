using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Lance
{
    public class InventoryManager : MonoBehaviour
    {
        // Singleton instance
        private static InventoryManager instance;

        // Reference to the InventoryMenu GameObject
        public GameObject InventoryMenu;

        // Reference to the CraftingPanel GameObject
        public GameObject CraftingPanel;

        // Reference to the button to toggle the CraftingPanel
        public GameObject buttonToCraftingPanel;

        // Reference to the inventory button
        public Button inventoryButton;

        // Array of ItemSlot components
        public ItemSlot[] itemSlot;

        // Flag to track if the InventoryMenu is activated
        private bool menuActivated;

        // Flag to track if the CraftingPanel is activated
        private bool craftingPanelActive;

        // Screen width and height
        private float screenWidth;
        private float screenHeight;

        // Public property to access the singleton instance
        public static InventoryManager Instance
        {
            get { return instance; }
        }

        // Awake method to initialize the singleton instance and prevent destruction on load
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Prevents the GameObject from being destroyed when loading a new scene
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start method to initialize variables and event listeners
        void Start()
        {
            menuActivated = InventoryMenu.activeSelf;
            craftingPanelActive = CraftingPanel.activeSelf;
            ToggleButtonVisibility();

            inventoryButton.onClick.AddListener(ToggleInventory);

            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }

        // Update method to handle touch input
        void Update()
        {
            // Touch input handling
            // (You can keep your existing touch input logic here)
        }

        // Method to toggle the InventoryMenu
        public void ToggleInventory()
        {
            Debug.Log("Inventory button pressed");

            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);

            if (craftingPanelActive)
            {
                ToggleCraftingPanel();
            }

            Time.timeScale = menuActivated ? 0f : 1f;

            ToggleButtonVisibility();
        }

        // Method to toggle the CraftingPanel
        public void ToggleCraftingPanel()
        {
            craftingPanelActive = !craftingPanelActive;
            CraftingPanel.SetActive(craftingPanelActive);
        }

        // Method to add an item to the inventory
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
                if (!itemSlot[i].isFull)
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

        // Method to toggle button visibility
        private void ToggleButtonVisibility()
        {
            buttonToCraftingPanel.SetActive(menuActivated);
        }

        // Helper method to check if a touch is over a UI element
        private bool IsPointerOverUIObject(Touch touch)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}
