using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Add this directive

namespace Lance
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
        public string itemName;
        public int quantity;
        public Sprite itemSprite;
        public bool isFull;

        [SerializeField]
        private TMP_Text quantityText;

        [SerializeField]
        private Image itemImage;

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
        }

        public void AddItem(string itemName, int quantity, Sprite itemsprite)
        {
            this.itemName = itemName;
            this.quantity = quantity;
            this.itemSprite = itemsprite;
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
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftClick();
            }
            if (eventData.button == PointerEventData.InputButton.Right)
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

            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }

        public void OnRightClick()
        {
            // Implement right-click functionality here
        }
    }
}
