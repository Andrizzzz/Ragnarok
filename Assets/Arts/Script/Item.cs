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

        // Start is called before the first frame update
        void Start()
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
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Trigger enter detected with: " + other.gameObject.name);

            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered item's vicinity!");

                if (inventoryManager != null)
                {
                    inventoryManager.AddItem(itemName, quantity, sprite);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogError("InventoryManager not assigned!");
                }
            }
            else
            {
                Debug.Log("Item triggered by non-player GameObject: " + other.gameObject.name);
            }
        }
    }
}







