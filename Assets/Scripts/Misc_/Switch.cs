using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public GameObject objectToSwitch;

    private SpriteRenderer theSR;

    public GameObject InteractArea;

    public UnityEvent interactAction;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hasSwitched)
        {
            if (deactivateOnSwitch)
            {
                objectToSwitch.SetActive(false);
            }
            else
            {
                objectToSwitch.SetActive(true);
            }

            theSR.sprite = downSprite;
            hasSwitched = true;
        }
    }*/

    public void UnLockDoor()
    { 
        objectToSwitch.SetActive(false);
        InteractArea.SetActive(false);
        if(interactAction != null)
        {
            interactAction.Invoke();
        }
    }
}
