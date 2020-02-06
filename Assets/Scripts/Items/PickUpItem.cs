using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private bool playerInRange = false;
    private bool isPickingUp = false;

    public enum ItemType
    {
        Stick,
        Stone,
        Axe,
    }

    [Header("Attached gameobject needs: Box Collider 2D collider and trigger")]
    public ItemType itemType;
    public int amountToPickup;

    GameObject player;
    Animator playerAnim;
    private float animTimer = 0;

    PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerAnim = player.GetComponent<Animator>();
        inventory = player.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            PlayerMovement.instance.canMove = false;
            isPickingUp = true;
            playerAnim.SetTrigger("pickUp");
        }

        if (isPickingUp)
        {
            
            animTimer += Time.deltaTime;
            if (animTimer >= 0.4)
            {
                switch (itemType)
                {
                    case ItemType.Stick:
                        inventory.AddItem(Item.ItemType.Stick, amountToPickup);
                        break;
                    case ItemType.Stone:
                        inventory.AddItem(Item.ItemType.Stone, amountToPickup);
                        break;
                    case ItemType.Axe:
                        inventory.AddItem(Item.ItemType.Axe, amountToPickup);
                        break;
                }
                Destroy(gameObject);
                PlayerMovement.instance.canMove = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

