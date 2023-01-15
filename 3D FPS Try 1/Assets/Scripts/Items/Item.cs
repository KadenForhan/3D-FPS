using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Item : MonoBehaviour
{
    public TextMeshProUGUI pickUpText;
    public bool playerInsideTrigger = false;
    public bool inHand = false;
    public bool isAmmo;
    public GameObject player;

    public float distanceFromCrosshair;

    public int ammoAmount;


    public void Start()
    {
        pickUpText.enabled = false;
    }

    public void Update()
    {
        if (playerInsideTrigger)
        {
            if (pickUpText.enabled == false)
            {
                pickUpText.enabled = true;
            }
            else
            {
                pickUpText.transform.LookAt(player.transform.GetChild(2), Vector3.up);
            }
        }
        else
        {
            if (pickUpText.enabled == true)
            {
                pickUpText.enabled = false;
            }
        }
    }

    // Called by PlayerInventory in OnTriggerStay() to check if this item is the closest to the players crosshair
    public void CheckIfClosestItemToPlayer()
    {
        distanceFromCrosshair = Vector3.Distance(gameObject.transform.position, player.transform.GetChild(2).transform.position);
        if (distanceFromCrosshair < player.GetComponent<PlayerInventory>().itemPlayerIsClosestTo.GetComponent<Item>().distanceFromCrosshair)
        {
            // Debug.Log(gameObject);
            player.GetComponent<PlayerInventory>().itemPlayerIsClosestTo = gameObject;
        }

    }

    // Called by PlayerInput when an input is pressed that is used for the gun("R" for reload, etc.)
    virtual public void inputPressed(string input)
    {

    }

    // Called by PlayerInventory when picked up
    virtual public void PickedUp()
    {

    }
}
