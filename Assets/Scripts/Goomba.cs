using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float speed = 2.2f;
    Rigidbody2D rb;
    private Vector2 direction = Vector2.right;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {




        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        float laserLength = GetComponent<BoxCollider2D>().size.x / 2 + .03f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, laserLength, LayerMask.GetMask("Foreground"));
        if (hit.collider != null)
        {
            Flip();
            direction = direction * -1;
        }
        //float laserLength = GetComponent<BoxCollider2D>().size.x / 2 + .2f;

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, -1), laserLength, LayerMask.GetMask("Foreground"));
        //Debug.DrawRay(transform.position, direction * laserLength, Color.red);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var contact = col.contacts[0];
            if (contact.normal.y < 0)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                GetComponent<Animator>().SetTrigger("Death");
                SoundManager.instance.PlaySound(SoundManager.Sound.Stomp);
            }
            else
            {
                col.gameObject.GetComponent<Player>().isDead = true;
                col.gameObject.GetComponent<Animator>().SetTrigger("Death");
            }
        }
    }
    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
