using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    private InventoryManager inventoryManager;

    [SerializeField]
    public int maxNumberOfItems;

    // ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    // UI Elements
    [SerializeField]
    public Image itemImage;
    [SerializeField]
    public TMP_Text quantityText;

    // Item Description UI Elements
    public Image ItemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    // Selection Indicator
    public GameObject selectedShader;
    public bool thisItemIsSelected;

    // UI Buttons
    public Button useButton;
    public Button dropButton;
   

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        // Hide buttons initially
        useButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);


        // Attach button click listeners
        useButton.onClick.AddListener(UseItem);
        dropButton.onClick.AddListener(DropItem);
   
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if it's a left-click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Let the UseButton handle the action
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        if (isFull)
            return quantity;

        // Update item data
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;

        // Update UI
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;

        this.quantity += quantity;

        // Update quantity text
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void UseItem()
    {
        Debug.Log("Item used: " + itemName);
        inventoryManager.UseItem(itemName);
        quantity--;
        quantityText.text = quantity.ToString();
        if (quantity <= 0)
        {
            EmptySlot();
        }
    }

    public void DropItem()
    {
        Debug.Log("Item dropped: " + itemName);
        // Call a method in the InventoryManager to handle dropping the item
        inventoryManager.DropItem(itemName);
        // Clear the slot
        EmptySlot();
    }



    public void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";
        ItemDescriptionImage.sprite = emptySprite;
    }

    public void ShowButtons()
    {
        // Show buttons
        useButton.gameObject.SetActive(true);
        dropButton.gameObject.SetActive(true);
    }
}
