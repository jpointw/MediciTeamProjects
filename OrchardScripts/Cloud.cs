using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        
    }

 
    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, 1 * Time.deltaTime);
    }

  
}
