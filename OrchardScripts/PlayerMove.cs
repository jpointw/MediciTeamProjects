using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;
    public float speed = 2.5f;

   
    void Start()
    {
        cc = GetComponent<CharacterController>();

    }
    void Update()
    {
        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        float h = axis.x;
        float v = axis.y;
        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();

        cc.Move (dir * speed * Time.deltaTime);
    }
}
