using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //
    public float speed = 0f;


    void FixedUpdate()
    {
        float cn = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, cn) * speed;
    }
}
