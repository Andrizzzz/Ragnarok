using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject DarkPanel;
    public ItemSO[] itemSOs;
    public ItemSlot[] itemSlot;

    private bool menuActivated;

    private void Start()
    {
        menuActivated = false;
        InventoryMenu.SetActive(false);
        DarkPanel.SetActive(false);

        Load();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Debug.Log("Inventory button pressed");
            ToggleInventory();
        }
    }

    public void UseItem(string itemName)
    {
        Debug.Log("UseItem called for: " + itemName);
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                itemSOs[i].UseItem();
            }
        }
        Save();
    }

    public void DropItem(string itemName)
    {
        Debug.Log("Dropping item: " + itemName);

        // Find the item slot associated with the item name
        ItemSlot slotToRemove = null;
        foreach (ItemSlot slot in itemSlot)
        {
            if (slot.itemName == itemName)
            {
                slotToRemove = slot;
                break;
            }
        }

        if (slotToRemove != null)
        {
            slotToRemove.EmptySlot();

            // Update UI to reflect dropped item
            UpdateSlotUI(slotToRemove);
        }
        else
        {
            Debug.LogWarning("Item slot not found for item: " + itemName);
        }
        Save();
    }

    public void SetItemSlotButtonsActive(bool active)
    {
        foreach (ItemSlot slot in itemSlot)
        {
            slot.useButton.gameObject.SetActive(active);
            slot.dropButton.gameObject.SetActive(active);
        }
    }



    public void ToggleInventory()
    {
        menuActivated = !menuActivated;
        InventoryMenu.SetActive(menuActivated);
        DarkPanel.SetActive(menuActivated);

        if (menuActivated)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
            SetItemSlotButtonsActive(false); // Set item slot buttons inactive when inventory is closed
        }
    }



    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull && (itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0))
            {
                itemSlot[i].itemName = itemName;
                itemSlot[i].itemSprite = itemSprite;
                itemSlot[i].itemDescription = itemDescription;
                itemSlot[i].quantity += quantity; // Increase the quantity by the given amount

                UpdateSlotUI(itemSlot[i]);
                Save();

                // If the quantity becomes zero, empty the slot
                if (itemSlot[i].quantity == 0)
                {
                    itemSlot[i].EmptySlot();
                }

                return;
            }
        }
        Save();
    }



    public void DeselectAllSlot()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemIsSelected = false;
        }
    }

    public void SwapItems(ItemSlot slot1, ItemSlot slot2)
    {
        // Swap item data
        string tempName = slot1.itemName;
        Sprite tempSprite = slot1.itemSprite;
        string tempDescription = slot1.itemDescription;
        int tempQuantity = slot1.quantity;

        slot1.itemName = slot2.itemName;
        slot1.itemSprite = slot2.itemSprite;
        slot1.itemDescription = slot2.itemDescription;
        slot1.quantity = slot2.quantity;

        slot2.itemName = tempName;
        slot2.itemSprite = tempSprite;
        slot2.itemDescription = tempDescription;
        slot2.quantity = tempQuantity;

        UpdateSlotUI(slot1);
        UpdateSlotUI(slot2);
        Save();
    }

    public Sprite emptySprite; // Declare emptySprite variable

    private void UpdateSlotUI(ItemSlot slot)
    {
        slot.quantityText.text = slot.quantity.ToString();
        slot.quantityText.enabled = slot.quantity > 0;

        if (slot.quantity > 0)
        {
            slot.itemImage.sprite = slot.itemSprite;
            slot.itemImage.enabled = true;
        }
        else
        {
            slot.itemImage.sprite = emptySprite; // Use emptySprite for empty slots
            slot.itemImage.enabled = false;
        }
    }




    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    private void Save()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i] != null)
            {
                PlayerPrefs.SetString("ItemName" + i, itemSlot[i].itemName);
                PlayerPrefs.SetInt("ItemQuantity" + i, itemSlot[i].quantity);
                PlayerPrefs.SetString("ItemDescription" + i, itemSlot[i].itemDescription);

                // Ensure itemSprite is not null before accessing its name
                if (itemSlot[i].itemSprite != null)
                {
                    PlayerPrefs.SetString("ItemSpritePath" + i, itemSlot[i].itemSprite.name);
                }
                else
                {
                    PlayerPrefs.SetString("ItemSpritePath" + i, string.Empty);
                }
            }
        }
        PlayerPrefs.Save();
    }


    private void Load()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            string itemNameKey = "ItemName" + i;
            string itemQuantityKey = "ItemQuantity" + i;
            string itemDescriptionKey = "ItemDescription" + i;
            string itemSpritePathKey = "ItemSpritePath" + i;

            if (PlayerPrefs.HasKey(itemNameKey))
            {
                itemSlot[i].itemName = PlayerPrefs.GetString(itemNameKey, string.Empty);
                itemSlot[i].quantity = PlayerPrefs.GetInt(itemQuantityKey, 0);
                itemSlot[i].itemDescription = PlayerPrefs.GetString(itemDescriptionKey, string.Empty);
                string spritePath = PlayerPrefs.GetString(itemSpritePathKey, string.Empty);

                if (!string.IsNullOrEmpty(spritePath) && itemSlot[i].quantity > 0)
                {
                    itemSlot[i].itemSprite = GetItemSprite(spritePath);
                }
                else
                {
                    itemSlot[i].itemSprite = null;
                }

                UpdateSlotUI(itemSlot[i]);
            }
        }
    }


    public Sprite GetItemSprite(string spriteName)
    {
        Sprite sprite = Resources.Load<Sprite>(spriteName);
        if (sprite == null)
        {
            Debug.LogWarning("Sprite not found for item: " + spriteName);
        }
        return sprite;
    }
}
