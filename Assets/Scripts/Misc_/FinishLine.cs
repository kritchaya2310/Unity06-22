using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager = FindObjectOfType<GameManager>();
            StartCoroutine(gameManager.LoadNextLevel(1f));
        }
    }
}
