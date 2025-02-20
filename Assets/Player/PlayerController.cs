using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerInput inputActions;

    public float speed = 2.7f;
    public float jumpForce = 3.5f;
    
    public GameObject bulletPrefab;
    public float bulletForce = 10f;

    bool canJump = true;
    bool canAttack = true;

    SpriteRenderer sprite;
    Animator animator;
    Rigidbody2D body;

    void Awake()
    {
        inputActions = new PlayerInput();
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        var moveInputs = inputActions.Player_Map.Movement.ReadValue<Vector2>();

        //Debug.Log("Move Inputs" + moveInputs);
        transform.position += speed * Time.deltaTime * new Vector3(moveInputs.x, 0, 0);

        animator.SetBool("b_isWalking", moveInputs.x != 0);

        if (moveInputs.x != 0)
        {
            sprite.flipX = moveInputs.x < 0;
        }

        canJump = Mathf.Abs(body.velocity.y) <= 0.001;

        HandlerJumpAction();
        HandleAttack();
    }

    private void HandlerJumpAction()
    {
        var jumpPressed = inputActions.Player_Map.Jump.IsPressed();

        if (canJump && jumpPressed)
        {
            body.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void HandleAttack()
    {
        var attackPressed = inputActions.Player_Map.Attack.IsPressed();
        if (canAttack && attackPressed)
        {
            canAttack = false;

            animator.SetTrigger("t_attack");
            
        }
    }

    public void ShootNewEgg()
    {
        var newBullet = GameObject.Instantiate(bulletPrefab);
        newBullet.transform.position = transform.position;

        var isLookRight = !sprite.flipX;
        Vector2 bulletDirection = bulletForce * new Vector2(isLookRight ? -1 : 1, 0);
        newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection, ForceMode2D.Impulse);
        newBullet.GetComponent<SpriteRenderer>().flipY = isLookRight;
    }

    public void SetCanAttack()
    {
        canAttack = true;
    }
}
