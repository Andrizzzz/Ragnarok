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


            if (selectedSlot != null && selectedSlot != this)
            {
                selectedSlot.DeselectItem();
            }
            selectedSlot = this;

        }
    }

    public void SingleClick()
    {


        if (selectedSlot != null && selectedSlot != this)
        {
            selectedSlot.DeselectItem();
        }


        if (thisItemIsSelected)
        {
            DeselectItem();
            selectedSlot = null;
            return;
        }

        Debug.Log("Item single clicked: " + itemName);
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        ItemDescriptionImage.sprite = itemSprite;
        selectedShader.SetActive(true);
        thisItemIsSelected = true;


        if (!string.IsNullOrEmpty(itemName) && quantity > 0)
        {
            useButton.gameObject.SetActive(true);
            dropButton.gameObject.SetActive(true);
        }
        else
        {
            useButton.gameObject.SetActive(false);
            dropButton.gameObject.SetActive(false);
        }


        useButton.onClick.RemoveAllListeners();
        dropButton.onClick.RemoveAllListeners();


        useButton.onClick.AddListener(UseItem);
        dropButton.onClick.AddListener(DropItem);

        selectedSlot = this; 

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

        if (!thisItemIsSelected)
            return;

        Debug.Log("Item used: " + itemName);
        inventoryManager.UseItem(itemName);


        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if (quantity <= 0)
        {
            EmptySlot();
            DeselectItem(); 

        }
        SaveItem();
    }

    public void DropItem()
    {
        if (!thisItemIsSelected)
            return;

        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5;
        sr.sortingLayerName = "PLAYER";

        itemToDrop.AddComponent<BoxCollider2D>();

        itemToDrop.transform.position = GameObject.Find("Player").transform.position + new Vector3(2, 0, 0);
        itemToDrop.transform.localScale = new Vector3(.5f, .5f, .5f);

        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if (quantity <= 0)
        {
            EmptySlot();
            DeselectItem(); 
        }

        Debug.Log("Item dropped: " + itemName);
        SaveItem();
    }


    public void EmptySlot()
    {
        quantityText.enabled = false;

        itemImage.sprite = emptySprite;
        ItemDescriptionText.text = "";
        ItemDescriptionNameText.text = "";
        ItemDescriptionImage.sprite = emptySprite;


        useButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);
    }

    public void DeselectItem()
    {
        selectedShader.SetActive(false);
        thisItemIsSelected = false;

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
