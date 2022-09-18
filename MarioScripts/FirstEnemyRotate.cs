using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirstEnemyRotate : MonoBehaviour
{
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    int targetIndex = 0;
    FirstEnemyRotate state;

    // Update is called once per frame
    private void Update()
    {
        UpdatePatrol();
    }
    private void UpdatePatrol()
    {
        Vector3 target = ChangePoint.instance.wayPoints[targetIndex].transform.position;
        agent.destination = target;
        Vector3 realTargetPos = target;
        realTargetPos.y = transform.position.y;
        float dist = Vector3.Distance(realTargetPos, transform.position);
        if (dist <= 0.1f)
        {
            targetIndex++;
            if (targetIndex >= ChangePoint.instance.wayPoints.Length)
            {
                targetIndex = 0;
            }
        }
    }
}
