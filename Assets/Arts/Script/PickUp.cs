using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class PickUp : MonoBehaviour
    {
        private Inventory inventory;
        public GameObject itemButton;

        private void Start()
        {
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (!inventory.isFull[i])
                    {
                        inventory.isFull[i] = true;
                        GameObject newItemButton = Instantiate(itemButton, inventory.slots[i].transform, false);
                        newItemButton.GetComponent<CanvasGroup>().alpha = 0f; // Make the button invisible
                        Destroy(gameObject); // Destroy the picked up item from the scene
                        break;
                    }
                }
            }
        }
    }
}


