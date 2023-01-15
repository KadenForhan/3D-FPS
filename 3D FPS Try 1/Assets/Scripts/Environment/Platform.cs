using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] float speed = 0.1f;
    [SerializeField] Vector3 spawnVector3;
    [SerializeField] int randomX = 5;
    [SerializeField] int randomY = 5;
    bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(spawnVector3.x + Random.Range(-randomX, randomX), spawnVector3.y + Random.Range(-randomY, randomY), spawnVector3.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed;

        if (transform.position.z > 10)
        {
            Destroy(gameObject);
        }
    }
}
