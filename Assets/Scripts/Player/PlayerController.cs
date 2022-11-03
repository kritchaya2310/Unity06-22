using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private PlayerAnimatorController animatorController;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;

    [Header("Player Values")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float timeBetweenJumps = 0.1f;
    [SerializeField] private float coyoteTimeDuration = 0.15f;

    [Header("Ground Checks")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float extraGroundCheckDistance = 0.5f;

    // Input Values
    private float _moveInput;
    private float _climbInput;

    // Boolean flags. Booleans for checking conditions.
    public bool _isGrounded;

    public float cooldown;
    float lastSwitchGravity;

    private bool _canJump;
    private bool _canDoubleJump;
    private bool IsMoving;
    private bool top;
    public Image cooldowmBar;

    // Private variables
    private float _coyoteTimeTimer;
    private float _lastJumpTimer;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CheckGround();
        CheckCanJump();
        SetAnimatorParameters();
    }

    private void FindGameManager()
    {
        if (_gameManager != null) return;

        _gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region Action

    public void Move()
    {
        rb.velocity = new Vector2(_moveInput * movementSpeed, rb.velocity.y);
    }

    private void FlipPlayerSprite()
    {
        player.localScale = _moveInput switch
        {
            > 0f => new Vector3(1, 1, 1),
            < 0f => new Vector3(-1, 1, 1),
            _ => player.localScale
        };
    }

    public void TryJumping()
    {
        if (_lastJumpTimer <= timeBetweenJumps) return;


        if(_isGrounded && !_canJump)
        {
            _canDoubleJump = false;
        }

        if(_isGrounded || _canDoubleJump)
        {
            Jump(jumpForce);
            _canDoubleJump = !_canDoubleJump;
        }
    }

    public void Jump(float force, float additionalTimeWait = 0f)
    {
        AudioManager.instance.PlaySFX(0);    
        _canJump = false;
        _canDoubleJump = true;
        _lastJumpTimer = 0f - additionalTimeWait;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(force * transform.up, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        var playerColliderBounds = playerCollider.bounds;

        var vector2Down = Vector2.down;
        var vector2Top = Vector2.up;

        if(top == false)
        {
            var raycastHit = Physics2D.BoxCast(playerColliderBounds.center,playerColliderBounds.size,0f,
                                            vector2Down, extraGroundCheckDistance,groundLayers);
            _isGrounded = raycastHit.collider != null;
        }
        else
        {
            var raycastHit = Physics2D.BoxCast(playerColliderBounds.center, playerColliderBounds.size, 0f,
                                            vector2Top, extraGroundCheckDistance, groundLayers);
            _isGrounded = raycastHit.collider != null;
        }

    }

    private void CheckCanJump()
    {
        _lastJumpTimer = Mathf.Min(_lastJumpTimer, timeBetweenJumps) + Time.deltaTime;

        if (_isGrounded)
        {
            _canJump = true;
            _coyoteTimeTimer = 0f;
            return;
        }

        _coyoteTimeTimer = Mathf.Min(_coyoteTimeTimer, coyoteTimeDuration) + Time.deltaTime;

        if (_coyoteTimeTimer <= coyoteTimeDuration) return;

        _canJump = false;
    }

    private void SetAnimatorParameters()
    {
        animatorController.SetAnimatorParameters(rb.velocity, _isGrounded);
    }

    public void PlayerDead()
    {
        AudioManager.instance.PlaySFX(1);
        FindGameManager();
        StartCoroutine(_gameManager.ProcessPlayerDeath(1f));
        anim.SetBool("getCatch", true);
    }

    public void SwitchGravity()
    {
        /*cooldowmBar.fillAmount = 0;
        if (Time.time - lastSwitchGravity < cooldown)
        {
            return;
        }
        
        lastSwitchGravity = Time.time;*/

        rb.gravityScale *= -1;
        if(top == false)
        {
            sr.flipX = true;
            rb.transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            sr.flipX = false;
            rb.transform.eulerAngles = Vector3.zero;
        }
        top = !top;
    }

    #endregion

    #region Input

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<float>();
        FlipPlayerSprite();
    }

    private void OnJump(InputValue value)
    {
        if (!value.isPressed) return;
        TryJumping();
    }

    /*private void OnGravity(InputValue value)
    {
        if (!value.isPressed) return;
        //SwitchGravity();
    }*/

    #endregion
}
