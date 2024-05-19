using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Lance
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
        

        public string itemName;
        public int quantity;
        public Sprite itemSprite;
        public bool isFull;
        public string itemDescription;
        public Sprite emptySprite;

        [SerializeField]
        private TMP_Text quantityText;

        [SerializeField]
        private Image itemImage;

        public Image itemDescriptionImage;
        public TMP_Text ItemDescriptionNameText;
        public TMP_Text ItemDescriptionText;

        public GameObject selectedShader;
        public bool thisItemSelected;

        private InventoryManager inventoryManager;

        private void Start()
        {
            // Find the InventoryManager in the scene and assign it to the inventoryManager field
            inventoryManager = GameObject.FindObjectOfType<InventoryManager>();

            if (inventoryManager == null)
            {
                Debug.LogError("InventoryManager not found in the scene.");
            }

            // Initially turn off the selected panel
            DeselectSlot();
        }

        public void AddItem(string itemName, int quantity, Sprite itemsprite, string itemDescription)
        {
            this.itemName = itemName;
            this.quantity = quantity;
            this.itemSprite = itemsprite;
            this.itemDescription = itemDescription;
            isFull = true;

            UpdateUI();
        }

        public void IncreaseQuantity(int quantity)
        {
            this.quantity += quantity;
            UpdateUI();
        }

        private void UpdateUI()
        {
            quantityText.text = this.quantity.ToString();
            quantityText.enabled = true;
            itemImage.sprite = this.itemSprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Check if left mouse button is clicked
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftClick();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick();
            }
        }

        public void OnLeftClick()
        {
            if (inventoryManager == null)
            {
                Debug.LogError("InventoryManager is not assigned.");
                return;
            }

            // Check if the clicked object is the inventory
            if (gameObject.CompareTag("Inventory"))
            {
                // If the slot is already selected, deselect it
                if (thisItemSelected)
                {
                    DeselectSlot();
                }
                // Otherwise, do nothing
                return;
            }
            else
            {
                // Deselect all slots before selecting the current one
                inventoryManager.DeselectAllSlots();

                // Select the current slot
                SelectSlot();
            }
        }




        public void OnRightClick()
        {
            // Implement right-click functionality here
        }

        // Method to select the current slot
        private void SelectSlot()
        {
            selectedShader.SetActive(true);
            thisItemSelected = true;
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite != null ? itemSprite : emptySprite;
        }

        // Method to deselect the current slot
        public void DeselectSlot()
        {
            selectedShader.SetActive(false);
            thisItemSelected = false;
            ItemDescriptionNameText.text = "";
            ItemDescriptionText.text = "";
            itemDescriptionImage.sprite = emptySprite;
        }
    }
}