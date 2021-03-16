using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Rigidbody2D rbball;

    public Rigidbody2D rbenemy;

    public Transform showpoint;

    private float Speed = 10f;


    // Update is called once per frame
    void Update()
    {
        
        //It'll only run when the ball is moving towards the right side
        if (rbball.velocity.x > 0)
        {
            //Gets the predicted path of the ball as well as shows the predicted path the ball shall be in
            showpoint.position = PredictCourse(rbenemy.position, rbball.position, rbball.velocity);

            //Move the enemy to block the ball
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, showpoint.transform.position.y), Speed * Time.deltaTime);
        }
        
    }

    public Vector3 PredictCourse(Vector3 enemy, Vector3 ballpos, Vector3 ballvelocity)
    {
        // Distance between ball and the enemy
        // then to change the value to float
        Vector3 D = enemy - ballpos;
        float d = D.magnitude;

        //Speed of the ball
        float floatballvelocity = ballvelocity.magnitude;

        //Using the Quadratic equation we can predict the path of the ball 
        // m = (ax)^2 + bx + c

        float a = Mathf.Pow(Speed, 2) - Mathf.Pow(floatballvelocity, 2);

        float b = 2 * Vector3.Dot(D, ballvelocity);

        float c = -Vector3.Dot(D, D);

        //Using the Quadratic formula  x = (  -b+sqrt( ((b)^2) * 4*a*c )  ) / 2a
        // We can determine the time T it'll take to reach the right/enemy side
        float t = (-(b) + Mathf.Sqrt(Mathf.Pow(b, 2) - (4 * (a * c)))) / (2 * a);

        // Calculate and return the prediction point as vector
        // by calculating between distance of the ball and the prediction point by the time it'll take to reach the end and its speed 
        return ((t * ballvelocity) + ballpos);
    }
}
