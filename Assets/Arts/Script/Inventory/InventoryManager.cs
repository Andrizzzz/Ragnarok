using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Lance
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject InventoryMenu;
        public GameObject CraftingPanel;
        public GameObject buttonToCraftingPanel;
        public Button inventoryButton;
        private bool menuActivated;
        private bool craftingPanelActive;

        public ItemSlot[] itemSlot;

        private float screenWidth;
        private float screenHeight;

        void Start()
        {
            menuActivated = InventoryMenu.activeSelf;
            craftingPanelActive = CraftingPanel.activeSelf;
            ToggleButtonVisibility();

            inventoryButton.onClick.AddListener(ToggleInventory);

            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);

                    if (IsPointerOverUIObject(touch))
                    {
                        continue;
                    }

                    Vector2 touchPosition = touch.position;

                    if (touch.phase == TouchPhase.Began)
                    {
                        // Example touch zones
                        if (touchPosition.x < screenWidth / 2)
                        {
                            HandleMovementTouch(touchPosition);
                        }
                        else if (touchPosition.x > screenWidth / 2)
                        {
                            HandleJumpTouch(touchPosition);
                        }
                    }
                }
            }
        }

        private void HandleMovementTouch(Vector2 touchPosition)
        {
            Debug.Log("Movement touch at: " + touchPosition);
            // Implement your movement logic here
        }

        private void HandleJumpTouch(Vector2 touchPosition)
        {
            Debug.Log("Jump touch at: " + touchPosition);
            // Implement your jump logic here
        }

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

        private void ToggleButtonVisibility()
        {
            buttonToCraftingPanel.SetActive(menuActivated);
        }

        public void ToggleCraftingPanel()
        {
            craftingPanelActive = !craftingPanelActive;
            CraftingPanel.SetActive(craftingPanelActive);
        }

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

        public void DeselectAllSlots()
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                itemSlot[i].selectedShader.SetActive(false);
                itemSlot[i].thisItemSelected = false;
            }
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
