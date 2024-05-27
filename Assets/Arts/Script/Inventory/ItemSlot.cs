using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private InventoryManager inventoryManager;

    public GameObject itemPrefab;
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

    // Use and Drop Buttons
    public Button useButton;
    public Button dropButton;

    private static ItemSlot selectedSlot; // Track the currently selected slot

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        LoadItem();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SingleClick();

            // Deselect the previously selected slot (if any)
            if (selectedSlot != null && selectedSlot != this)
            {
                selectedSlot.DeselectItem();
            }
            selectedSlot = this; // Update the currently selected slot
        }
    }

    public void SingleClick()
    {
        Debug.Log("Item single clicked: " + itemName);
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        ItemDescriptionImage.sprite = itemSprite;
        selectedShader.SetActive(true);
        thisItemIsSelected = true;

        // Display Use and Drop buttons
        useButton.gameObject.SetActive(true);
        dropButton.gameObject.SetActive(true);

        // Set the functions for Use and Drop buttons
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(UseItem);
        dropButton.onClick.RemoveAllListeners();
        dropButton.onClick.AddListener(DropItem);
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
            SaveItem();
            return extraItems;
        }

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        SaveItem();

        return 0;
    }

    public void UseItem()
    {
        Debug.Log("Item used: " + itemName);
        inventoryManager.UseItem(itemName);

        // Check if quantity is already zero before decrementing
        if (quantity > 0)
        {
            this.quantity -= 1;
            quantityText.text = quantity.ToString();
            if (quantity <= 0)
            {
                EmptySlot();
            }
            SaveItem();
        }
    }


    public void DropItem()
    {
        Debug.Log("Item dropped: " + itemName);
        inventoryManager.DropItem(itemName);
        // Logic for dropping the item...
        quantity -= 1;
        SaveItem();
        if (quantity <= 0)
        {
            EmptySlot();
            DeselectItem(); // Deselect the item slot when the item is gone
        }
    }

    public void EmptySlot()
    {
        itemName = string.Empty;
        quantity = 0;
        itemDescription = string.Empty;
        itemSprite = null;
        isFull = false;

        itemImage.sprite = emptySprite;
        itemImage.enabled = false;

        quantityText.text = "0";
        quantityText.enabled = false;

        SaveItem();
        DeselectItem(); // Deselect the item slot when the slot is emptied
    }



    // This method is called when the item is deselected
    // This method is called when the item is deselected
    public void DeselectItem()
    {
        selectedShader.SetActive(false);
        thisItemIsSelected = false;
        // Hide Use and Drop buttons when item is deselected
        useButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);
    }


    private void SaveItem()
    {
        PlayerPrefs.SetString("ItemName" + GetInstanceID(), itemName);
        PlayerPrefs.SetInt("ItemQuantity" + GetInstanceID(), quantity);
        PlayerPrefs.SetString("ItemDescription" + GetInstanceID(), itemDescription);

        if (itemSprite != null)
        {
            PlayerPrefs.SetString("ItemSpritePath" + GetInstanceID(), itemSprite.name);
        }
        else
        {
            PlayerPrefs.SetString("ItemSpritePath" + GetInstanceID(), string.Empty);
        }

        PlayerPrefs.Save();
    }

    private void LoadItem()
    {
        int instanceID = GetInstanceID();
        string itemNameKey = "ItemName" + instanceID;
        string itemQuantityKey = "ItemQuantity" + instanceID;
        string itemDescriptionKey = "ItemDescription" + instanceID;
        string itemSpritePathKey = "ItemSpritePath" + instanceID;

        if (PlayerPrefs.HasKey(itemNameKey))
        {
            itemName = PlayerPrefs.GetString(itemNameKey, string.Empty);
            quantity = PlayerPrefs.GetInt(itemQuantityKey, 0);
            itemDescription = PlayerPrefs.GetString(itemDescriptionKey, string.Empty);
            string spritePath = PlayerPrefs.GetString(itemSpritePathKey, string.Empty);

            if (!string.IsNullOrEmpty(spritePath))
            {
                itemSprite = inventoryManager.GetItemSprite(spritePath);
            }
            else
            {
                itemSprite = null;
            }

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (itemSprite != null)
        {
            itemImage.sprite = itemSprite;
            itemImage.enabled = true;
        }
        else
        {
            itemImage.enabled = false;
        }

        quantityText.text = quantity.ToString();
        quantityText.enabled = quantity > 0;
    }
}
