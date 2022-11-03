using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallImpact : MonoBehaviour
{
    private GameManager gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager = FindObjectOfType<GameManager>();
        if (collision.relativeVelocity.magnitude > 0)
        {
            AudioManager.instance.PlaySFX(5);
        }
    }
}
