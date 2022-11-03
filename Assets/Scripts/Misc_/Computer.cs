using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public void Interracted()
    {
        AudioManager.instance.PlaySFX(4);
        Destroy(gameObject);
        
    }
}
