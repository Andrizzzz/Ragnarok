using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private InventoryManager inventoryManager;
    // ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    // ITEM SLOT
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    // ITEM DESCRIPTION
    public Image ItemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemIsSelected;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;  // Make sure itemImage is enabled when adding an item
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlot();
        selectedShader.SetActive(true);
        thisItemIsSelected = true;

        if (isFull)
        {
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            ItemDescriptionImage.sprite = itemSprite;
            ItemDescriptionImage.gameObject.SetActive(true); // Activate the image
            ItemDescriptionNameText.gameObject.SetActive(true); // Activate the name text
            ItemDescriptionText.gameObject.SetActive(true); // Activate the description text
        }
        else
        {
            ItemDescriptionNameText.text = "";
            ItemDescriptionText.text = "";
            ItemDescriptionImage.gameObject.SetActive(false); // Deactivate the description image
            ItemDescriptionNameText.gameObject.SetActive(false); // Deactivate the name text
            ItemDescriptionText.gameObject.SetActive(false); // Deactivate the description text
            itemImage.gameObject.SetActive(false); // Deactivate the item image
        }
    }

    public void OnRightClick()
    {
        // Implement the right-click functionality if needed
    }
}
