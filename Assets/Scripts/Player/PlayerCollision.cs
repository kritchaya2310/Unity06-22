using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private Collider2D _playerCollider;

    void Start()
    {
        _playerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            playerController.PlayerDead();
        }
    }
}
