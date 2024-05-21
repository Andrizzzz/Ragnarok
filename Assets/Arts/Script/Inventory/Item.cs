using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Item : MonoBehaviour
    {
    [SerializeField]
    private string ItemName;
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
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(ItemName, quantity, sprite, itemDescription);
            Destroy(gameObject);
        }
    }


}
    
      
