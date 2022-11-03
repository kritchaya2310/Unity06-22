using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject notificationBox;

    public GameObject exitInteract;
    //public Animator anim;

    void Update()
    {
        if (isInRange)
        {

            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInRange = true;
            NotifyPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
            DeNotifyPlayer();
        }
    }

    public void NotifyPlayer()
    {
        notificationBox.SetActive(true);
    }

    public void DeNotifyPlayer()
    {
        notificationBox.SetActive(false);
        if (exitInteract != null)
        {
            Instantiate(exitInteract, notificationBox.transform.position, Quaternion.identity);
        }
    }
}
