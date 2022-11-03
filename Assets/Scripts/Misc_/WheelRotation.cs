using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public void Rotation()
    {
        AudioManager.instance.PlaySFX(2);
        transform.DORotate(new Vector3(0, 0, 180f), 1);
    }
}
