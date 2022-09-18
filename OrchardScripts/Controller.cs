using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //컨트롤러가 통제할 컨트롤 타겟 변수 선언.
    //이 컨트롤타겟이 바뀌면 컨트롤러가 조작하는 대상이 바뀌게 된다.
    public Controlable controlTarget;

    public void ChangeControlTarget(Controlable origin, Controlable target)
    {
        //카메라를 오리진위치에서 타겟위치로 이동
        StartCoroutine(MoveCameraArm(origin, target));
        //인스펙터에 있는 컨트롤 타겟을 차량으로 바꿔준다.
        controlTarget = target;

    }
    public IEnumerator MoveCameraArm(Controlable origin, Controlable target)
    {
        if(origin.cameraArm != null)
        {
            var cameraArm = origin.cameraArm;
            Vector3 startPos = cameraArm.position;
            Quaternion startRot = cameraArm.rotation;
            float timer = 0f;
            while(timer <= 1f)
            {
                yield return null;
                timer += Time.deltaTime * 3f;
                cameraArm.position = Vector3.Lerp(startPos, target.cameraArmSocket.position, timer);
                cameraArm.rotation = Quaternion.Slerp(startRot, target.cameraArmSocket.rotation, timer);
            }
            cameraArm.SetParent(target.cameraArmSocket);
            target.cameraArm = cameraArm;
        }
    }

        void Start()
    {
        
    }

    void Update()
    {
        
    }
}
