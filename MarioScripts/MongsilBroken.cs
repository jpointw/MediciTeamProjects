using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MongsilBroken : MonoBehaviour
{
    GameObject player;
    GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player)
        {
            particle = Resources.Load("Assets/Resources/Explosion") as GameObject;
            Debug.Log(col);
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
