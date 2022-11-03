using DG.Tweening;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private Transform castPoint;
    [SerializeField] private Transform castEndPoint;
    [SerializeField] float agroRange;

    [SerializeField] PlayerController playerController;

    [SerializeField] LineRenderer lr;


    private void Update()
    {
        if (CanSeePlayer(agroRange))
        {
            Debug.Log("Turret See Player");
            playerController.PlayerDead();
        }
    }

    private bool CanSeePlayer(float distance)
    {
        bool val = false;
        var castDist = -distance;

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, castEndPoint.position, 1 << LayerMask.NameToLayer("Action"));

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
            lr.SetPosition(1, castEndPoint.position);
        }

        return val;
    }

}
