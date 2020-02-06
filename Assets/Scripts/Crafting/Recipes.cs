using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipes : MonoBehaviour
{
    public static Recipes instance;

    [Header("Creatable Objects")]
    public GameObject[] creatableItems = new GameObject[10];

    [Header("Interface Elements")]
    public Text selectedItemTitle;
    public Text selectedItemText;
    public Image selectedItemImage;

    private Item[] playerInventory;

    // Ressources available
    private int stone = 0;
    private int stick = 0;

    // Ressources needed for active recipe
    private int stoneNeeded = 0;
    private int stickNeeded = 0;

    private string selectedItem = "";

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        playerInventory = PlayerInventory.instance.inventoryItems;
        HowManyItemsInInventory();
    }

    public void Axe()
    {
        // Ressources needed
        stoneNeeded = 1;
        stickNeeded = 2;

        selectedItem = creatableItems[1].ToString();
        selectedItemTitle.text = "Simple Axe";
        selectedItemText.text = "This is a simple axe to chop trees.";
        selectedItemImage.sprite = PlayerInventory.instance.inventoryIcons[2];

        SoundManager.instance.PlaySound(2);
    }

    public void CampFire()
    {
        // Ressources needed
        stoneNeeded = 5;
        stickNeeded = 3;

        selectedItem = creatableItems[0].ToString();
        selectedItemTitle.text = "Campfire";
        selectedItemText.text = "Campfires gives heat, light and you can cook food on them.";
        selectedItemImage.sprite = PlayerInventory.instance.inventoryIcons[3];

        SoundManager.instance.PlaySound(2);
    }

    public void CraftButton()
    {
        string item = selectedItemTitle.text;
        if (item != "Default Item Name")
        {
            switch (item)
            {
                case "Campfire":
                    PlayerInventory.instance.AddItem(Item.ItemType.CampFire, 1);
                    break;
                case "Simple Axe":
                    PlayerInventory.instance.AddItem(Item.ItemType.Axe, 1);
                    break;
            }
        }
    }

    public void HowManyItemsInInventory()
    {
        stone = 0;
        stick = 0;

        for (int i = 0; i < playerInventory.Length; i++)
        {
            
            Item item = playerInventory[i];

            if (item != null)
            {
                switch (item.itemType)
                {
                    case Item.ItemType.Stone:
                        stone += item.amount;
                        break;
                    case Item.ItemType.Stick:
                        stick += item.amount;
                        break;
                }
            }
        }
    }
}
