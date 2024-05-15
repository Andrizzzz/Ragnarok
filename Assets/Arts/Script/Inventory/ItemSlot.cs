using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Lance
{
    public class ItemSlot : MonoBehaviour
    {
        public string itemName;
        public int quantity;
        public Sprite itemSprite;
        public bool isFull;

        [SerializeField]
        private TMP_Text quantityText;

        [SerializeField]
        private Image itemImage;

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
    }
}
