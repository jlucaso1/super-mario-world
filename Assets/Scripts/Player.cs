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
  int playerLevel = 0;
  Rigidbody2D rb;
  bool needRescale = false;
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
    RescaleToSprite(true);
  }
  void Update()
  {
    if (needRescale)
    {
      RescaleToSprite();
      needRescale = false;
    }
    if (Input.GetButtonUp("Crouch"))
    {
      isCrouching = false;
      needRescale = true;
      rb.constraints = RigidbodyConstraints2D.FreezeRotation;
      animator.SetBool("isCrouching", isCrouching);
    }
    if (Input.GetButtonDown("Crouch"))
    {
      isCrouching = true;
      needRescale = true;
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
      animator.SetBool("isJumping", true);
    }

  }
  void FixedUpdate()
  {
    controller.Move(horizontal_move * speed * Time.fixedDeltaTime, isJumping);
    isJumping = false;
  }
  public void onLand()
  {
    animator.SetBool("isJumping", false);
  }
  public void RescaleToSprite(bool force = false)
  {
    if (spriteRenderer.sprite.bounds.size.y == boxCollider.size.y) { Debug.Log("y iguais"); ; return; }
    Vector2 newSize = new Vector2(0.43f, spriteRenderer.sprite.bounds.size.y);
    boxCollider.size = newSize;
  }
  public void GrowUp()
  {
    playerLevel++;
    animator.SetInteger("playerLevel", playerLevel);
    animator.SetTrigger("changePlayerLevel");
    RescaleToSprite(true);
    Debug.Log("Resized");
  }
}
