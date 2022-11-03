using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserLeft : MonoBehaviour
{
    [SerializeField] Transform castPoint; //start lr
    [SerializeField] float agroRange;

    [SerializeField] private PlayerController playerController;

    [SerializeField] LineRenderer lr;
    [SerializeField] SpriteRenderer sr;

    void Update()
    {
        if (CanSeePlayer(agroRange))
        {
            playerController = FindObjectOfType<PlayerController>();
            playerController.PlayerDead();
        }
    }

    private bool CanSeePlayer(float distance)
    {
        bool val = false;
        var castDist = -distance;

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }

            lr.SetPosition(0, castPoint.position);
            lr.SetPosition(1, hit.point);
        }
        else
        {
            lr.SetPosition(0, castPoint.position);
            lr.SetPosition(1, endPos);
        }

        return val;
    }
}
