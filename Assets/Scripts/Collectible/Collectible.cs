using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerController = FindObjectOfType<PlayerController>();
            Destroy(gameObject);
            playerController.SwitchGravity();
        }
    }
}
