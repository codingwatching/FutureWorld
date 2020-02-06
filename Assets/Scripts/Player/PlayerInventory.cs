using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    public Item[] inventoryItems;

    public Sprite[] inventoryIcons = new Sprite[0];
    public Image[] inventorySlots = new Image[9];
    public Text[] slotCounterText = new Text[9];

    [SerializeField] GameObject inventoryPanel;

    private void Awake()
    {
        inventoryPanel.SetActive(true);
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        inventoryItems = new Item[9];
        DisplayInventory();
    }


    void Update()
    {
        
    }

    public void AddItem(Item.ItemType itemType,  int amount)
    {
        bool itemAdded = false;

        // Check if there is already the same item in the slot and stack is not full
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] != null)
            {
                if (inventoryItems[i].itemType == itemType && inventoryItems[i].amount < inventoryItems[i].maxStack)
                {
                    inventoryItems[i].amount += amount;
                    itemAdded = true;
                    DisplayInventory();
                    break;
                }
            }
        }

        // There is no same item
        if (itemAdded == false)
        {
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i] == null)
                {
                    inventoryItems[i] = new Item(itemType, amount);

                    DisplayInventory();
                    break;
                }
            }
        }
    }

    public void AddItemOnPosition(Item.ItemType itemType, int amount, int index)
    {
        inventoryItems[index] = new Item(itemType, amount);
    }

    public void RemoveItem(Item.ItemType itemType, int amount)
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] != null && inventoryItems[i].itemType == itemType)
            {
                inventoryItems[i].amount -= amount;
                DisplayInventory();
                break;
            }
        }
    }

    public void RemoveItemOnPosition(int amount, int index)
    {
        inventoryItems[index].amount -= amount;
        DisplayInventory();
    }

    public void DisplayInventory()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] != null)
            {
                if (inventoryItems[i].amount == 0)
                {
                    // If there amount of the item is 0, don't show the image and empty inventory array slot
                    inventoryItems[i] = null;
                    inventorySlots[i].GetComponent<Image>().enabled = false;
                    slotCounterText[i].text = "";
                }
                else
                {
                    // Display the right sprite and counter when there are items in the slot
                    switch (inventoryItems[i].itemType)
                    {
                        case Item.ItemType.Stick:
                            inventorySlots[i].sprite = inventoryIcons[0];
                            slotCounterText[i].text = inventoryItems[i].amount.ToString();
                            inventorySlots[i].GetComponent<Image>().enabled = true;
                            break;
                        case Item.ItemType.Stone:
                            inventorySlots[i].sprite = inventoryIcons[1];
                            slotCounterText[i].text = inventoryItems[i].amount.ToString();
                            inventorySlots[i].GetComponent<Image>().enabled = true;
                            break;
                        case Item.ItemType.Axe:
                            inventorySlots[i].sprite = inventoryIcons[2];
                            slotCounterText[i].text = inventoryItems[i].amount.ToString();
                            inventorySlots[i].GetComponent<Image>().enabled = true;
                            break;
                        case Item.ItemType.CampFire:
                            inventorySlots[i].sprite = inventoryIcons[3];
                            slotCounterText[i].text = inventoryItems[i].amount.ToString();
                            inventorySlots[i].GetComponent<Image>().enabled = true;
                            break;
                    }
                }

                if (inventoryItems[i] != null && inventoryItems[i].amount < 2)
                {
                    slotCounterText[i].text = "";
                }
            }
            else
            {
                // If there is no item in the slot
                inventorySlots[i].GetComponent<Image>().enabled = false;
                slotCounterText[i].text = "";
            }
        }
        
    }
}
