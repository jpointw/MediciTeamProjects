using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePoint : MonoBehaviour
{
    public static ChangePoint instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject[] wayPoints;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

