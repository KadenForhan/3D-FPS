using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pistol : Item
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private int ammoInGun = 0;
    public int AmmoInGun
    {
        set
        {
            ammoInGun = value;
            ammoText.text = ammoInGun.ToString();
        }
        get => ammoInGun;
    }

    [SerializeField] private int maxAmmoForGun = 12;
    [SerializeField] private float reloadDelay = 1f;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Animator animator;
    [SerializeField] GameManager gameManager;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] LayerMask itemMask;

    [SerializeField] TextMeshProUGUI ammoText;



    new void Start()
    {
        ammoInGun = maxAmmoForGun;
        ammoText.enabled = false;
        ammoText.text = ammoInGun.ToString();
        base.Start();
    }

    void UpdateAmmoText()
    {
        ammoText.text = ammoInGun.ToString();
    }

    public override void PickedUp()
    {
        ammoText.enabled = true;
    }
    override public void inputPressed(string input)
    {
        if (player.GetComponent<PlayerInventory>().inventoryScreenEnabled)
        {
            return;
        }

        if (input == "Fire1")
        {
            if (ammoInGun > 0)
            {
                if (!animator.GetBool("isInteracting"))
                {
                    Shoot();
                    return;
                }
            }
        }

        if (input == "R")
        {
            if (player.GetComponent<PlayerInventory>().ammoInInventory > 0)
            {
                if (ammoInGun < maxAmmoForGun)
                {
                    if (!animator.GetBool("isInteracting"))
                    {
                        StartCoroutine(Reload());
                        return;
                    }
                }
            }
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        animator.Play("GunShootAnim", 0, 0.0f);
        AmmoInGun --;
        // UpdateAmmoText();
        


        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, itemMask))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }



            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.3f);
        }
    }

    IEnumerator Reload()
    {
        animator.SetBool("isInteracting", true);
        animator.Play("GunReloadAnim", 0, 0.0f);
        yield return new WaitForSeconds(0);
        // yield return new WaitForSeconds(reloadDelay);
        // while (player.GetComponent<PlayerInventory>().ammoInInventory > 0)
        // {
        //     if
        // }
        while (ammoInGun < maxAmmoForGun)
        {
            if (player.GetComponent<PlayerInventory>().ammoInInventory > 0)
            {
                AmmoInGun += 1;
                player.GetComponent<PlayerInventory>().ammoInInventory -=1;
            }
            else break;
        }
        // UpdateAmmoText();
        // ammoInGun = maxAmmoForGun;
    }
}
