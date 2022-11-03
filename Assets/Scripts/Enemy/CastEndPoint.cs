using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastEndPoint : MonoBehaviour
{
    [SerializeField] private Transform tweenEndPoint;
    [SerializeField, Range(1, 10)] float moveSpeed;

    void Start()
    {
        transform.DOMove(tweenEndPoint.position, moveSpeed).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

}
