using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory;
    void Start()
    {
        playerInventory = gameObject.GetComponent<PlayerInventory>();
        // playerInventory.inventory = new GameObject[playerInventory.maxInventorySize];
        // playerInventory.iconArray = new GameObject[playerInventory.maxInventorySize];
        playerInventory.start();
    }

    void Update()
    {
        #region WeaponInput
        if (Input.GetButtonDown("Fire1"))
        {
            if (playerInventory.itemIsInHand)
            {
                playerInventory.itemInHand.inputPressed("Fire1");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerInventory.itemIsInHand)
            {
                playerInventory.itemInHand.inputPressed("R");
            }
        }
        #endregion
        
        #region InventoryInput

        if (Input.GetKeyDown("e"))
        {
            playerInventory.inputPressed("e");
            // playerInventory.ePressed = true;
        }

        // if (Input.GetKeyUp("e"))
        // {
        //     playerInventory.ePressed = false;
        // }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (playerInventory.inventory[0] != null)
            {
                playerInventory.inputPressed("Alpha1");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (playerInventory.inventory[1] != null)
            {
                playerInventory.inputPressed("Alpha2");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (playerInventory.inventory[2] != null)
            {
                playerInventory.inputPressed("Alpha3");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (playerInventory.inventory[3] != null)
            {
                playerInventory.inputPressed("Alpha4");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (playerInventory.inventory[4] != null)
            {
                playerInventory.inputPressed("Alpha5");
            }
        }

        // To acces inventory screen
        if (Input.GetKeyDown("tab"))
        {
            playerInventory.inputPressed("tab");
        }

        #endregion
    }
}
