using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{

    public int refelctions;
    public float maxlength;

    public CircleCollider2D cd;

    private LineRenderer lineRenderer;
    private Ray2D ray;
    private RaycastHit2D hit;
    private Vector3 direction;
    private Rigidbody2D rb;
    private int balllayer;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.right * 10f;
        balllayer = LayerMask.NameToLayer("Water");

    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.CircleCast(transform.position, cd.radius, rb.velocity);

        Debug.DrawRay(transform.position, rb.velocity);

        // If it hits something...
        if (hit.collider != null)
        {
            Debug.Log("awd");
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("123");
            }
        }
    }
}
