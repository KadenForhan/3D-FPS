using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitePlatform : MonoBehaviour
{

    [SerializeField] GameObject platformPF;
    GameObject currentPlatform;

    bool inPlatformDelay = false;
    [SerializeField] float delay = 1.2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPlatformDelay)
        {
            StartCoroutine("PlatformDelay");
        }
    }

    IEnumerator PlatformDelay()
    {
        inPlatformDelay = true;
        yield return new WaitForSeconds(delay);
        SpawnPlatform();
        inPlatformDelay = false;

    }

    void SpawnPlatform()
    {
        currentPlatform = Instantiate(platformPF);
    }

}
