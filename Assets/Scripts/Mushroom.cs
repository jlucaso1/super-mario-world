using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float speed = 4f;
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
            direction = direction * -1;
        }


        //Debug.DrawRay(transform.position, direction * laserLength, Color.red);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player.instance.GrowUp();
            Destroy(gameObject);
        }
    }
}
