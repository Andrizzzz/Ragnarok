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

        private InventoryManager inventoryManager;

        private void Start()
        {
            Debug.Log("Item position: " + transform.position);
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
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player entered the vicinity!"); // Check if player collision is detected
                if (inventoryManager != null)
                {
                    inventoryManager.AddItem(itemName, quantity, sprite);
                    Destroy(gameObject);
                    Debug.Log("Item added to inventory"); // Check if item is added to the inventory
                }
                else
                {
                    Debug.LogError("Inventory manager not assigned"); // Log an error if inventoryManager is null
                }
            }
        }
    }
}
