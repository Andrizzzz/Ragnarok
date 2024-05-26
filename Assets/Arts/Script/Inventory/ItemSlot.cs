using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
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

    private float lastClickTime;
    private const float doubleClickThreshold = 0.5f; // Adjust the threshold as needed
    private bool isDragging = false;

    Transform parentAfterDrag;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        LoadItem();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Time.time - lastClickTime < doubleClickThreshold)
            {
                UseItem();
            }
            else
            {
                SingleClick();
            }
            lastClickTime = Time.time;
        }
    }

    public void SingleClick()
    {
        Debug.Log("Item single clicked: " + itemName);
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        ItemDescriptionImage.sprite = itemSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemSprite != null && itemName != string.Empty)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Follow the cursor
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            transform.SetParent(parentAfterDrag);
            transform.localPosition = Vector3.zero; // Reset position within the parent
            DropItem();
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
        this.quantity -= 1;
        quantityText.text = quantity.ToString();
        if (quantity <= 0)
        {
            EmptySlot();
        }
        SaveItem();
    }

    public void DropItem()
    {
        Debug.Log("Item dropped: " + itemName);
        inventoryManager.DropItem(itemName);

        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        // Manipulate SpriteRenderer
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Ground";

        // Collider
        itemToDrop.AddComponent<BoxCollider2D>();

        // Location
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(.5f, 0, 0);
        itemToDrop.transform.localScale = new Vector3(.7f, .7f, .7f);

        // Subtract the item from slot
        this.quantity -= 1;
        quantityText.text = quantity.ToString();

        if (quantity <= 0)
        {
            EmptySlot();
        }
        else
        {
            // Update UI without emptying the slot completely
            UpdateUI();
        }

        SaveItem();
    }

    public void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";
        ItemDescriptionImage.sprite = emptySprite;

        // Reset item data
        itemName = "";
        itemDescription = "";
        quantity = 0;
        itemSprite = null;
        isFull = false;

        SaveItem();
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
