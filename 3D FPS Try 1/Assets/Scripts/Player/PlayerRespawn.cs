using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Transform respawnTransform;
    [SerializeField] Transform player;

    void Update()
    {
        player.position = respawnTransform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log(respawnTransform.position);
            player.position = respawnTransform.position;
        }
    }
}
