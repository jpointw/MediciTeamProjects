using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingpongEnemy : MonoBehaviour
{
    public float speed = 5;
    float currentTime;
    float dir = 0.5f;
    public Vector3 velocity;
    float currentDelayTime;
    float delayTime = 2;

    enum State
    {
        Move,
        Delay,
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Move;
        velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Move)
        {
            currentTime += dir * Time.deltaTime;
            velocity = (dir == 0.5f ? transform.forward : transform.forward) * speed;
            transform.position += velocity * Time.deltaTime;
            if (dir == 0.5f)
            {
                if (currentTime > 1)
                {
                    dir = -0.5f;
                    currentTime = 1;
                    state = State.Delay;
                    currentDelayTime = 0;
                }
            }
            else
            {
                if (currentTime < 0)
                {
                    dir = 0.5f;
                    currentTime = 0;
                    state = State.Delay;
                    currentDelayTime = 0;
                }
            }
        }
        else if (state == State.Delay)
        {
            velocity = Vector3.zero;
            transform.Rotate(Vector3.up * Time.deltaTime * 90);
            currentDelayTime += Time.deltaTime;
            if (currentDelayTime > delayTime)
            {
                state = State.Move;
            }
        }
    }
}
