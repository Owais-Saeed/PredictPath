using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stores position at certain time
[System.Serializable]
public class TimePosition
{
    public float time;
    public Vector2 position;
}

public class Enemy : MonoBehaviour
{
    
    public Rigidbody2D rbenemy;

    public float Speed = 1;
    
    public Rigidbody2D rbball;

    //Predict target position 20 seconds ahead
    public float aheadsec = 20;
    
    //Time between predictions
    public float ratesec = .5f;
    
    public Vector2 TargetPosition;//Where target is going
    //Debug
    public Transform showpoint;

    // Use this for initialization
    void Start()
    {

    }

    void PositionCompare()
    {
        //enemy
        List<TimePosition> thispos = new List<TimePosition>();//Debug
        Vector2 currentpos = rbenemy.position;

        //ball
        List<TimePosition> targetpos = new List<TimePosition>();
        Vector2 currentballpos = rbball.position;

        //Calculate all posible positions ahead for target
        for (float i = 0; i < aheadsec; i += ratesec)
        {
            TimePosition nextballposotiontime = new TimePosition();
            Vector2 nextpositionball = currentballpos + (rbball.velocity * i);
            nextballposotiontime.time = i;
            nextballposotiontime.position = nextpositionball;
            targetpos.Add(nextballposotiontime);
        }


        int closestindex = 0;

        float shortesttime = float.MaxValue;
        float previoustime = shortesttime;
        bool shortestfound = false;

        //Find shortest path
        for (int j = 0; j < targetpos.Count; j++)
        {
            TimePosition thistargetPos = targetpos[j];
            if (shortestfound)
            {
                break;//Stop looping if the ideal path have been found
            }

            float closestpoint = float.MaxValue;
            float previousclosestpoint = closestpoint;

            for (float i = 0; i < aheadsec; i += ratesec)
            {
                //Calculate every time/position for this
                Vector2 NextPositionThis = currentpos + (rbenemy.velocity * i);

                float timetoreach = Vector2.Distance(rbenemy.position, thistargetPos.position) / Speed;//How much time it will take to reach this position
                float timedifference = Mathf.Abs(timetoreach - thistargetPos.time);
                if (timedifference < shortesttime)
                {
                    shortesttime = previoustime = timedifference;
                    closestindex = j;
                }
                else if (timedifference > previoustime)
                {
                    shortestfound = true;
                }
            }
        }

        TargetPosition = targetpos[closestindex].position;
        if (showpoint)
        {
            showpoint.position = TargetPosition;
        }
    }




    void Update()
    {
        if(rbball.velocity.x > 0)
        {
            PositionCompare();

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, TargetPosition.y), Speed * Time.deltaTime);
        }
        else
        {
            rbenemy.velocity = Vector2.zero;
        }
    }

    
}
