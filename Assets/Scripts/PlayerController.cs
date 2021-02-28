using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private float m_JumpForce = 400f;              // Amount of force added when the player jumps.
  [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
  [SerializeField] private bool m_AirControl = false;             // Whether or not a player can steer while jumping;
  [SerializeField] private LayerMask m_WhatIsGround;              // A mask determining what is ground to the character
  public bool inGround = false;


  private Rigidbody2D m_Rigidbody2D;
  private bool m_FacingRight = true;  // For determining which way the player is currently facing.
  private Vector3 m_Velocity = Vector3.zero;

  [Header("Events")]
  [Space]

  public UnityEvent OnLandEvent;

  // [System.Serializable]
  // public class BoolEvent : UnityEvent<bool> { }

  private void Awake()
  {
    m_Rigidbody2D = GetComponent<Rigidbody2D>();

    if (OnLandEvent == null)
      OnLandEvent = new UnityEvent();
  }


  public void Move(float move, bool jump)
  {

    //only control the player if grounded or airControl is turned on
    // if (m_Grounded || m_AirControl)
    if (inGround || m_AirControl)
    {
      // Move the character by finding the target velocity
      Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
      // And then smoothing it out and applying it to the character
      m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

      // If the input is moving the player right and the player is facing left...
      if (move > 0 && !m_FacingRight)
      {
        // ... flip the player.
        Flip();
      }
      // Otherwise if the input is moving the player left and the player is facing right...
      else if (move < 0 && m_FacingRight)
      {
        // ... flip the player.
        Flip();
      }
    }
    // If the player should jump...
    if (inGround && jump)
    {
      // Add a vertical force to the player.
      m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
    }
  }


  private void Flip()
  {
    // Switch the way the player is labelled as facing.
    m_FacingRight = !m_FacingRight;

    // Multiply the player's x local scale by -1.
    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
  }
  private void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.layer != 8) return;
    foreach (var contact in col.contacts)
    {
      if (contact.normal.y > 0)
      { //if the bottom side hit something 
        OnLandEvent.Invoke();
        inGround = true;
      }
    }
  }
  private void OnCollisionExit2D(Collision2D col)
  {
    if (col.gameObject.layer != 8 && !inGround) return;
    inGround = false;
  }
}