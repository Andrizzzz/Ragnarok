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

        LoadInventory();
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

                return leftOverItems;
            }
        }
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

        // Update UI for both slots
        UpdateSlotUI(slot1);
        UpdateSlotUI(slot2);
    }

    private void UpdateSlotUI(ItemSlot slot)
    {
        // Update UI elements for the given slot
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

    public void SaveInventory()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            PlayerPrefs.SetString($"ItemName_{i}", itemSlot[i].itemName);
            PlayerPrefs.SetInt($"ItemQuantity_{i}", itemSlot[i].quantity);
            // Assuming item sprites are identified by a name or index, save an identifier instead of the sprite directly
            PlayerPrefs.SetString($"ItemSprite_{i}", itemSlot[i].itemSprite.name);
            PlayerPrefs.SetString($"ItemDescription_{i}", itemSlot[i].itemDescription);
        }
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            string itemName = PlayerPrefs.GetString($"ItemName_{i}", "");
            int quantity = PlayerPrefs.GetInt($"ItemQuantity_{i}", 0);
            string itemSpriteName = PlayerPrefs.GetString($"ItemSprite_{i}", "");
            string itemDescription = PlayerPrefs.GetString($"ItemDescription_{i}", "");

            if (!string.IsNullOrEmpty(itemName))
            {
                Sprite itemSprite = Resources.Load<Sprite>(itemSpriteName);
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
            }
            else
            {
                itemSlot[i].EmptySlot();
            }
        }
    }
}
