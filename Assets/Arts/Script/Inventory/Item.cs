using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;
    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite sprite;

    [TextArea]
    [SerializeField]
    public string itemDescription;

    private InventoryManager inventoryManager;

    

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        LoadItem();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
                ClearItem(); // Clear PlayerPrefs for this item
            }
            else
            {
                quantity = leftOverItems;
                SaveItem(); // Save the remaining quantity
            }
        }
    }

    private void SaveItem()
    {
        PlayerPrefs.SetString(gameObject.name + "_itemName", itemName);
        PlayerPrefs.SetInt(gameObject.name + "_quantity", quantity);
        PlayerPrefs.SetString(gameObject.name + "_itemDescription", itemDescription);

        if (sprite != null)
        {
            PlayerPrefs.SetString(gameObject.name + "_spritePath", sprite.name);
        }
        else
        {
            PlayerPrefs.SetString(gameObject.name + "_spritePath", string.Empty);
        }

        PlayerPrefs.Save();
    }

    private void LoadItem()
    {
        if (PlayerPrefs.HasKey(gameObject.name + "_itemName"))
        {
            itemName = PlayerPrefs.GetString(gameObject.name + "_itemName");
            quantity = PlayerPrefs.GetInt(gameObject.name + "_quantity");
            itemDescription = PlayerPrefs.GetString(gameObject.name + "_itemDescription");
            string spritePath = PlayerPrefs.GetString(gameObject.name + "_spritePath");

            if (!string.IsNullOrEmpty(spritePath))
            {
                sprite = Resources.Load<Sprite>(spritePath);
            }
        }
    }

    private void ClearItem()
    {
        PlayerPrefs.DeleteKey(gameObject.name + "_itemName");
        PlayerPrefs.DeleteKey(gameObject.name + "_quantity");
        PlayerPrefs.DeleteKey(gameObject.name + "_itemDescription");
        PlayerPrefs.DeleteKey(gameObject.name + "_spritePath");
        PlayerPrefs.Save();
    }
}
