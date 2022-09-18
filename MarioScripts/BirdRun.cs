using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class BirdRun : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    GameObject target;
    bool distanceCheck = false;
    float currentTime = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Player");
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance < 2.5f)
        {
            distanceCheck = true;
        }
        if (distanceCheck == true)
        {
            anim.Play("BirdFly");
            Vector3 dir = new Vector3(0.1f, 0.1f, 0.1f);
            transform.position += dir * Time.deltaTime * 50;
            currentTime += Time.deltaTime;
            if (currentTime > 2f)
            {
                Destroy(gameObject);
            }
        }
    }
}
