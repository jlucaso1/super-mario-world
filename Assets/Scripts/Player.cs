using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    PlayerController controller;
    public float speed = 20f;
    public float normalSpeed = 20f;
    public float runSpeed = 40f;
    float horizontal_move = 0;
    bool isJumping = false;
    bool isRunning = false;
    bool isCrouching = false;
    public bool isDead = false;
    int playerLevel = 0;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    public static Player instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (isDead) return;
        if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            animator.SetBool("isCrouching", isCrouching);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            animator.SetBool("isCrouching", isCrouching);
        }

        if (isCrouching)
        {
            horizontal_move = 0;
            return;
        }
        horizontal_move = Input.GetAxis("Horizontal");
        animator.SetFloat("absSpeed", Mathf.Abs(horizontal_move));
        isRunning = Input.GetButton("Dash");
        if (Input.GetButton("Horizontal") && isRunning)
        {
            speed = runSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
        animator.SetBool("inGround", controller.inGround);
        animator.SetBool("isRunning", isRunning);
        if (Input.GetButtonDown("Jump") && controller.inGround)
        {
            isJumping = true;
            SoundManager.instance.PlaySound(SoundManager.Sound.Jump);
        }
        animator.SetBool("isJumping", !controller.inGround);

    }
    void FixedUpdate()
    {
        if (isDead) return;
        controller.Move(horizontal_move * speed * Time.fixedDeltaTime, isJumping);
        isJumping = false;
    }
    public void OnLand()
    {
        animator.SetBool("isJumping", false);
    }
    public void GrowUp()
    {
        bool inGround = controller.inGround;
        playerLevel++;
        animator.SetInteger("playerLevel", playerLevel);
        animator.SetTrigger("changePlayerLevel");
        SoundManager.instance.PlaySound(SoundManager.Sound.PowerUp);
        rb.AddForce(new Vector2(0, 20f));
        if (inGround)
        {
            Debug.Log(inGround);
            controller.inGround = inGround;
        }
    }
    void Death()
    {
        GameControl.instance.RestartLevel();
    }
}
