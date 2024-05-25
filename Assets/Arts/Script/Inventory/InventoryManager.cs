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
        }
        else
        {
            Debug.LogWarning("Item slot not found for item: " + itemName);
        }
        Save();
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
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);

                Save();
                return leftOverItems;
            }
        }
        Save();
        return quantity;
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

    private void UpdateSlotUI(ItemSlot slot)
    {
        slot.itemImage.sprite = slot.itemSprite;
        slot.quantityText.text = slot.quantity.ToString();
        slot.quantityText.enabled = slot.quantity > 0;
        slot.itemImage.enabled = slot.quantity > 0;
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
            PlayerPrefs.SetString("ItemName" + i, itemSlot[i].itemName);
            PlayerPrefs.SetInt("ItemQuantity" + i, itemSlot[i].quantity);
            // Assuming you have a way to save the sprite and description as well
            PlayerPrefs.SetString("ItemDescription" + i, itemSlot[i].itemDescription);
            // Sprite saving can be handled differently, e.g., saving a sprite index or path
            // PlayerPrefs.SetString("ItemSpritePath" + i, itemSlot[i].itemSprite.name);
        }
        PlayerPrefs.Save();
    }

    private void Load()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].itemName = PlayerPrefs.GetString("ItemName" + i, string.Empty);
            itemSlot[i].quantity = PlayerPrefs.GetInt("ItemQuantity" + i, 0);
            itemSlot[i].itemDescription = PlayerPrefs.GetString("ItemDescription" + i, string.Empty);
            // Load sprite if saved
            // string spritePath = PlayerPrefs.GetString("ItemSpritePath" + i, string.Empty);
            // if (!string.IsNullOrEmpty(spritePath)) 
            // {
            //     itemSlot[i].itemSprite = Resources.Load<Sprite>(spritePath);
            // }
            UpdateSlotUI(itemSlot[i]);
        }
    }
}
