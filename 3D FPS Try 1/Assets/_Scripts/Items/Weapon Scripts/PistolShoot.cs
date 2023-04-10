using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShoot : MonoBehaviour
{

    [SerializeField] private int ammo = 12;
    [SerializeField] private Camera FPSCamera;
    [SerializeField] private float range = 50;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(FPSCamera.transform.position, FPSCamera.transform.forward * range);

        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray shootRay = new Ray(FPSCamera.transform.position, FPSCamera.transform.forward);
            
            if (Physics.Raycast(shootRay, out hit, range))
                    {
                        if (hit.collider.gameObject.layer != 3)
                        {
                            if (hit.collider.tag != "Floor")
                            {
                                Destroy(hit.collider.gameObject);
                            }    
                        }
                    }
            ammo --;
        }


    }
}
