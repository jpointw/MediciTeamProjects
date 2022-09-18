using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed = 5;
    float currentTime;
    int dir = 1;
    public Vector3 velocity;

    float currentDelayTime;
    float delayTime = 2;

    enum State
    {
        Move,
        Delay,
    }
    State state;


    public enum Direction
    {
        Right,
        Up
    }
    public Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        if (direction == Direction.Right)
        {
            velocity = transform.right * speed;
        }
        else if (direction == Direction.Up)
        {
            velocity = transform.up * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Move)
        {
            currentTime += dir * Time.deltaTime;
            if (direction == Direction.Right)
            {
                velocity = (dir == 1 ? transform.right : -transform.right) * speed;
            }
            else if (direction == Direction.Up)
            {
                velocity = (dir == 1 ? transform.up : -transform.up) * speed;
            }
            
            transform.position += velocity * Time.deltaTime;
            if (dir == 1)
            {
                if (currentTime > 1)
                {
                    dir = -1;
                    currentTime = 1;
                    state = State.Delay;
                    currentDelayTime = 0;
                }
            }
            else
            {
                if (currentTime < 0)
                {
                    dir = 1;
                    currentTime = 0;
                    state = State.Delay;
                    currentDelayTime = 0;
                }
            }
        }
        else if (state == State.Delay)
        {
            velocity = Vector3.zero;
            currentDelayTime += Time.deltaTime;
            if (currentDelayTime > delayTime)
            {
                state = State.Move;
            }

        }
    }
}
