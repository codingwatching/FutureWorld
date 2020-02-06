using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropItems : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    Item tempItem;
    bool hasTempItem = false;

    Item tempSwitchItem;

    [SerializeField] Image tempItemImage;
    [SerializeField] Text tempText;

    [SerializeField] private Canvas canvas;

    void Start()
    {
        // References
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        TempItemDisplay();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;
        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (hasTempItem)
        {
            //For every result returned, output the name of the gameObject on the canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                switch (result.gameObject.name)
                {
                    case "Quickslot1":
                        TempToInventoryItem(0);
                        break;
                    case "Quickslot2":
                        TempToInventoryItem(1);
                        break;
                    case "Quickslot3":
                        TempToInventoryItem(2);
                        break;
                    case "Quickslot4":
                        TempToInventoryItem(3);
                        break;
                    case "Quickslot5":
                        TempToInventoryItem(4);
                        break;
                    case "Quickslot6":
                        TempToInventoryItem(5);
                        break;
                    case "Quickslot7":
                        TempToInventoryItem(6);
                        break;
                    case "Quickslot8":
                        TempToInventoryItem(7);
                        break;
                    case "Quickslot9":
                        TempToInventoryItem(8);
                        break;
                }
            }
        }
        else // If there is no temp item
        {
            //For every result returned, output the name of the gameObject on the canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                switch (result.gameObject.name)
                {
                    case "Quickslot1":
                        InventoryToTempItem(0);
                        break;
                    case "Quickslot2":
                        InventoryToTempItem(1);
                        break;
                    case "Quickslot3":
                        InventoryToTempItem(2);
                        break;
                    case "Quickslot4":
                        InventoryToTempItem(3);
                        break;
                    case "Quickslot5":
                        InventoryToTempItem(4);
                        break;
                    case "Quickslot6":
                        InventoryToTempItem(5);
                        break;
                    case "Quickslot7":
                        InventoryToTempItem(6);
                        break;
                    case "Quickslot8":
                        InventoryToTempItem(7);
                        break;
                    case "Quickslot9":
                        InventoryToTempItem(8);
                        break;
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (hasTempItem)
        {
            //For every result returned, output the name of the gameObject on the canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                switch (result.gameObject.name)
                {
                    case "Quickslot1":
                        TempToInventoryItem(0);
                        break;
                    case "Quickslot2":
                        TempToInventoryItem(1);
                        break;
                    case "Quickslot3":
                        TempToInventoryItem(2);
                        break;
                    case "Quickslot4":
                        TempToInventoryItem(3);
                        break;
                    case "Quickslot5":
                        TempToInventoryItem(4);
                        break;
                    case "Quickslot6":
                        TempToInventoryItem(5);
                        break;
                    case "Quickslot7":
                        TempToInventoryItem(6);
                        break;
                    case "Quickslot8":
                        TempToInventoryItem(7);
                        break;
                    case "Quickslot9":
                        TempToInventoryItem(8);
                        break;
                }
            }
        }
    }

    public void TempToInventoryItem(int index)
    {
        if (PlayerInventory.instance.inventoryItems[index] == null)
        {
            // When target inventory slot is empty
            PlayerInventory.instance.inventoryItems[index] = tempItem;
            PlayerInventory.instance.DisplayInventory();
            tempItem = null;
            hasTempItem = false;
        }
        else
        {
            // When there is an item in the target inventory slot
            tempSwitchItem = PlayerInventory.instance.inventoryItems[index];
            PlayerInventory.instance.inventoryItems[index] = tempItem;
            tempItem = tempSwitchItem;
            tempText.text = tempSwitchItem.amount.ToString();
            PlayerInventory.instance.DisplayInventory();
        }
        
    }

    public void InventoryToTempItem(int index)
    {
        if (PlayerInventory.instance.inventoryItems[index] != null)
        {
            tempItem = new Item(PlayerInventory.instance.inventoryItems[index].itemType, PlayerInventory.instance.inventoryItems[index].amount);
            tempText.text = tempItem.amount.ToString();
            PlayerInventory.instance.RemoveItemOnPosition(tempItem.amount, index);
            hasTempItem = true;
        }
    }

    public void TempItemDisplay()
    {
        if (hasTempItem)
        {
            tempItemImage.enabled = true;

            if (tempItem.amount > 1)
            {
                tempText.enabled = true;
            }
            else
            {
                tempText.enabled = false;
            }

            switch (tempItem.itemType)
            {
                case Item.ItemType.Axe:
                    tempItemImage.sprite = PlayerInventory.instance.inventoryIcons[2];
                    break;
                case Item.ItemType.Stone:
                    tempItemImage.sprite = PlayerInventory.instance.inventoryIcons[1];
                    break;
                case Item.ItemType.Stick:
                    tempItemImage.sprite = PlayerInventory.instance.inventoryIcons[0];
                    break;
                case Item.ItemType.CampFire:
                    tempItemImage.sprite = PlayerInventory.instance.inventoryIcons[3];
                    break;
            }

            tempItemImage.transform.position = Input.mousePosition;
        }
        else
        {
            tempItemImage.enabled = false;
            tempText.enabled = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Don't delete this method, needs to be here to handle the drag item placement
    }
}
