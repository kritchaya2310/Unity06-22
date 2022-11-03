using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange;

    [SerializeField] Transform castPoint;
    [SerializeField] Transform leftPoint, rightPoint;

    [SerializeField] private float moveSpeed;
    [SerializeField] float moveTime, waitTime;
    private float moveCount, waitCount;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Animator anim;

    [SerializeField] private LineRenderer lr;

    private bool movingRight;

    PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
    }

    void Update()
    {
        if (CanSeePlayer(agroRange))
        {
            anim.SetBool("Seeing", true);
            Debug.Log("See Player");
            playerController.PlayerDead();
        }
        else
        {
            if (moveCount > 0)
            {
                moveCount -= Time.deltaTime;

                if (movingRight)
                {
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

                    sr.flipX = true;

                    if (transform.position.x > rightPoint.position.x)
                    {
                        movingRight = false;
                    }
                }
                else
                {
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

                    sr.flipX = false;

                    if (transform.position.x < leftPoint.position.x)
                    {
                        movingRight = true;
                    }
                }

                if (moveCount <= 0)
                {
                    waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
                }

                anim.SetBool("isMoving", true);
            }
            else if (waitCount > 0)
            {
                waitCount -= Time.deltaTime;
                rb.velocity = new Vector2(0f, rb.velocity.y);

                if (waitCount <= 0)
                {
                    moveCount = Random.Range(moveTime * .75f, waitTime * 1.25f); ;
                }
                anim.SetBool("isMoving", false);
            }
        }

    }

    private bool CanSeePlayer(float distance)
    {
        bool val = false;
        var castDist = -distance;

        if(movingRight)
        {
            castDist = distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if(hit.collider != null)
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
