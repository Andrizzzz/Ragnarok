using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public ItemSO[] itemSOs;

    private bool menuActivated;

    public ItemSlot[] itemSlot;

    private void Start()
    {
        menuActivated = false;
        InventoryMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
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

    public void ToggleInventory()
    {
        menuActivated = !menuActivated;
        InventoryMenu.SetActive(menuActivated);

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
        for(int i = 0;i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemIsSelected = false;
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


}

