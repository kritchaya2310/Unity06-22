using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStarter : MonoBehaviour
{
    [SerializeField] private GameObject timerStarter;
    [SerializeField] private GameObject timerBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timerBar.gameObject.SetActive(true);
            //Destroy(gameObject);
        }
    }
}
