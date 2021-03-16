using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10.0f;

    private Rigidbody2D rb;

    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.left * speed;
    }

    // Update is called once per frame
    void Update()
    {
        /*/doesnt work when bouncing
        Vector2 predictedPoint = new Vector2(transform.position.x, transform.position.y) + rb.velocity * Time.deltaTime;
        Debug.Log(predictedPoint);
        */

        Raycasting();

    }


    void SpawnBall()
    {
        rb.velocity = Vector2.left * speed;
    }

    //This allows the ball to change its Y direction 
    //by calculating the move direction of the player
    //as well as the velocity of the player
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Debug.Log("hit player");
            Vector2 vel;
            vel.x = rb.velocity.x;
            vel.y = (rb.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rb.velocity = vel;
        }
    }


    //When the ball goes through either of the sides
    //then reset the ball after 1 second
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Side"))
        {
            rb.velocity = Vector2.zero;
            transform.position = Vector2.zero;
            Invoke("SpawnBall", 1);
        }
    }

    void Raycasting()
    {
        //Debug.DrawRay(transform.position, rb.velocity);

        hit = Physics2D.Raycast(transform.position, rb.velocity, 5f);

        //If something was hit.
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, hit.point);
            //If the object hit is less than or equal to 6 units away from this object.
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Enemy In Range!");
            }
        }
    }
}
