using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] public GameObject[] inventory;
    [SerializeField] private int slotInInventory;
    [SerializeField] public int maxInventorySize;

    [SerializeField] public GameObject[] iconArray;
    [SerializeField] Canvas overlayCanvas;
    [SerializeField] GameObject inventoryDisplay;
    [SerializeField] private Camera FPSCamera;
    [SerializeField] GameManager gameManager;
    public bool ePressed;
    [SerializeField] int numberOfCollidersPlayerisInsideOf;
    public GameObject itemPlayerIsClosestTo;

    public bool itemIsInHand = false;
    public Item itemInHand;

    [SerializeField] Vector3 dropPosition;

    [SerializeField] public int ammoInInventory = 12;


    void SortColliderList()
    {
        // foreach (Collider collider in collidersPlayerisInsideOf)
        // {

        // } 
    }

    // Called by PlayerInput when an input is pressed that is used for the inventory("1" to switch to item slot #1, etc.)
    public void inputPressed(string input)
    {
        if (input == "e")
        {
            if (numberOfCollidersPlayerisInsideOf > 0)
            {
                if (itemPlayerIsClosestTo.layer == 7)
                {   
                    if (itemPlayerIsClosestTo.GetComponent<Item>().isAmmo)
                    {
                        ammoInInventory += itemPlayerIsClosestTo.GetComponent<Item>().ammoAmount;
                        Debug.Log(numberOfCollidersPlayerisInsideOf + " Colliders Player is inside of");
                        numberOfCollidersPlayerisInsideOf -= 1;
                        Destroy(itemPlayerIsClosestTo);
                        itemPlayerIsClosestTo = null;
                    }
                    else PickUpItem(itemPlayerIsClosestTo);
                }
            }
        }

        if (input == "Alpha1")
        {
            SwitchItem(0);
        }

        if (input == "Alpha2")
        {
           SwitchItem(1);
        }

        if (input == "Alpha3")
        {
            SwitchItem(2);
        }
        
        if (input == "Alpha4")
        {
            SwitchItem(3);
        }

        if (input == "Alpha5")
        {
            SwitchItem(4);
        }

    }

    #region Item Triggers
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer != 8)
        {
            numberOfCollidersPlayerisInsideOf += 1;
            collider.gameObject.GetComponent<Item>().player = gameObject;
            collider.gameObject.GetComponent<Item>().playerInsideTrigger = true;
            //Chech hererererer 
            if (numberOfCollidersPlayerisInsideOf <= 1)
            {
                itemPlayerIsClosestTo = collider.gameObject;
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (numberOfCollidersPlayerisInsideOf > 0)
        {
            // numberOfCollidersPlayerisInsideOf += 1;
            collider.gameObject.GetComponent<Item>().CheckIfClosestItemToPlayer();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        collider.gameObject.GetComponent<Item>().playerInsideTrigger = false;
        numberOfCollidersPlayerisInsideOf -= 1;
    }
    #endregion

    #region Item Handling
    // Picks up item 
    private void PickUpItem(GameObject item)
    {
        // int inventoryIndex = 0;
        // while (inventoryIndex < 5)
        // {
        //     else inventoryIndex++;
        // }   

        for (int inventoryIndex = 0; inventoryIndex <= maxInventorySize-1; inventoryIndex++)
        {
            if (inventory[inventoryIndex] == null)
            {
                inventory[inventoryIndex] = item;
                GameObject iconObject = new GameObject();
                iconArray[inventoryIndex] = iconObject;
                // Collider collider = item.GetComponent<Collider>();                
                // collider.enabled = false;
                // item.GetComponent<Item>().playerInsideTrigger = false;
                // item.GetComponent<Item>().PickedUp();
                // item.GetComponent<Item>().pickUpText.enabled = false;
                PutItemInHand(item, iconObject);

                if (!itemIsInHand)
                {
                    slotInInventory = inventoryIndex;
                    itemIsInHand = true;
                    itemInHand = inventory[slotInInventory].GetComponent<Item>();
                }
                else
                {
                    item.SetActive(false);

                    // GameObject model = Item.transform.GetChild(0).gameObject;
                    // model.SetActive(false);
                }
                return;
            }
        }
        inventory[slotInInventory] = item;
        Destroy(iconArray[slotInInventory]);
        iconArray[slotInInventory] = null;
        GameObject iconobject = new GameObject();
        iconArray[slotInInventory] = iconobject ;
        PutItemInHand(item, iconobject);
        SwapOutItem(item, itemInHand.gameObject);
        itemInHand = item.GetComponent<Item>();
    }

    void PutItemInHand(GameObject item, GameObject iconObject)
    {
        numberOfCollidersPlayerisInsideOf --;
        Collider collider = item.GetComponent<Collider>();                
        collider.enabled = false;
        item.GetComponent<Item>().playerInsideTrigger = false;
        item.GetComponent<Item>().PickedUp();
        item.GetComponent<Item>().pickUpText.enabled = false;
        item.transform.SetParent(FPSCamera.transform);
        item.transform.localPosition = new Vector3(0.800000012f ,-0.639999986f ,1.62f);
        item.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        iconObject.transform.parent = overlayCanvas.transform.Find("PanelPlay").Find("Inventory Display");
        iconObject.AddComponent<Image>();
        iconObject.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        iconObject.GetComponent<Image>().sprite = item.GetComponent<Item>().icon;
        iconObject.GetComponent<Image>().color = UnityEngine.Random.ColorHSV();
        RearrangeItemIcons();
    }

    void SwapOutItem(GameObject itemIn, GameObject itemOut)
    {
        itemOut.transform.parent = null;
        while(itemOut.transform.position.y > 1)
        {
            itemOut.transform.position -= new Vector3(0, 0.0001f, 0);
        }
        // Debug.Log(transform.rotation);
        itemOut.transform.rotation = transform.rotation;
        // itemOut.transform.localPosition = new Vector3(0, -0.17f, 2.5f);
        itemOut.GetComponent<Collider>().enabled = true;

        RearrangeItemIcons();
    }

    private void SwitchItem(int inventoryIndex)
    {
        if (slotInInventory != inventoryIndex)
        {
            // Animator animator = inventory[slotInInventory].GetComponent<Animator>();
            // animator.Play("GunSwitchOutofHand", 0, 0.0f);
            inventory[slotInInventory].SetActive(false);
            slotInInventory = inventoryIndex;
            GameObject Item = inventory[inventoryIndex];
            Item.SetActive(true);
            itemInHand = Item.GetComponent<Item>();
        }
    }

    private void RearrangeItemIcons()
    {
        for (int index = 0; index <= iconArray.Length-1; index++)
        {
            if (iconArray[index] != null)
            {
                iconArray[index].transform.localPosition = new Vector3(0 + (75*index), 0, 0);
            }
        }
    }
    #endregion
    
    void ArrangeHand()
    {
        // if ((deckInHand.Count) % 2 != 0)
        //     {
        //         int midddleCard = Convert.ToInt32(Math.Floor(deckInHand.Count / 2f));
        //         for (int index = 0; index <= deckInHand.Count - 1; index++)
        //         {
        //             deckInHand[index].transform.position = new Vector2(1.7f * (index - midddleCard) - 0.9f, -3.5f);
        //         }
        //     }
        //     else
        //     {
        //         int midddleCard = deckInHand.Count / 2;
        //         for (int index = 0; index <= deckInHand.Count - 1; index++)
        //         {
        //             deckInHand[index].transform.position = new Vector2((1.7f * (index - midddleCard) - 0.05f), -3.5f);
        //         }
        //     }
    }
}
