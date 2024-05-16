using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private string itemName;

        [SerializeField]
        private int quantity;

        [SerializeField]
        private Sprite sprite;

        [TextArea]
        [SerializeField]
        private string itemDescription;

        private InventoryManager inventoryManager;

        private void Start()
        {
            GameObject inventoryCanvas = GameObject.Find("Canvas");

            if (inventoryCanvas != null)
            {
                inventoryManager = inventoryCanvas.GetComponent<InventoryManager>();
            }
            else
            {
                Debug.LogError("InventoryCanvas not found!");
                inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
            }

            int itemLayer = LayerMask.NameToLayer("Item");
            int enemyLayer = LayerMask.NameToLayer("Enemy");

            if (itemLayer != -1 && enemyLayer != -1)
            {
                Physics2D.IgnoreLayerCollision(itemLayer, enemyLayer);
            }
            else
            {
                Debug.LogError("One or both of the specified layers do not exist.");
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (inventoryManager != null)
                {
                    // Add the item to the inventory
                    inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);

                    // Add the item to the crafting panel
                    inventoryManager.AddItemToCraftingPanel(itemName, quantity, sprite, itemDescription);

                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogError("Inventory manager not assigned");
                }
            }
        }
    }
}
