using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGunShoot : MonoBehaviour
{
    [SerializeField] private int ammo = 100;
    [SerializeField] private float range = 50f; 

    [SerializeField] private Transform gunPoint;

    private bool isShooting = true;
    private bool isReloading = false;

    [SerializeField] private float delay = 0.01f;

    // Update is called once per frame
    void Update()
    {
        if (ammo >= 100)  
        {
            isReloading = false;
        }
            if (Input.GetButton("Fire1"))
            {
                if (ammo == 100)
                {
                    isShooting = true;
                    ammo -= 1;
                    Shoot();
                }
            }
                

        if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
        }

        if (!isShooting && !isReloading && ammo < 100)  StartCoroutine(Reload());
    }

    private void Shoot()
    {
        RaycastHit hit;
        Ray shootRay = new Ray(gunPoint.position, gunPoint.forward);

        if (Physics.Raycast(shootRay, out hit, range))
                {
                    if (hit.collider.tag != "Floor")
                    {
                        Destroy(hit.collider.gameObject);
                    }    
                }
    }


    IEnumerator Reload()
    {
        // if (!isReloading)
        // {
        isReloading = true;
        yield return new WaitForSeconds(1);
        // }
        while (ammo < 100)
        {
            yield return new WaitForSeconds(delay);
            ammo ++;
        }
    }

    
}
