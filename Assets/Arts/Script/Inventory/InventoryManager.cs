using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
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

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return;
            }
        }
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

