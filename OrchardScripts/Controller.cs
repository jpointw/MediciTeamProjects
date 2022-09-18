using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //��Ʈ�ѷ��� ������ ��Ʈ�� Ÿ�� ���� ����.
    //�� ��Ʈ��Ÿ���� �ٲ�� ��Ʈ�ѷ��� �����ϴ� ����� �ٲ�� �ȴ�.
    public Controlable controlTarget;

    public void ChangeControlTarget(Controlable origin, Controlable target)
    {
        //ī�޶� ��������ġ���� Ÿ����ġ�� �̵�
        StartCoroutine(MoveCameraArm(origin, target));
        //�ν����Ϳ� �ִ� ��Ʈ�� Ÿ���� �������� �ٲ��ش�.
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
