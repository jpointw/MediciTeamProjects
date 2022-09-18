using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controlable : MonoBehaviour
    //abstract��? �߻�Ŭ������ �ν��Ͻ�(��ü)�� ���� �� ���� Ư���� Ŭ�����̴�.
    //�߻�Ŭ������ ������ �߻�޼��带 ����� �̰��� ����� ���ؼ� �Ļ�Ŭ�������� �����ϵ��� �ϴ� ���̴�.
{
    //�Լ��� abstract�� �����ϸ� Controlable�� ��ӹ޴� Ŭ�������� �� �Լ��� ���������� ����ؾ��Ѵ�.
    //����Ŭ,ĳ���� ��Ʈ�ѷ��� �ִ� ī�޶�ϼ��ϰ� ī�޶� ���� ���⼭ ��ӽ����ش�.
    public Transform cameraArmSocket;
    public Transform cameraArm;
    public abstract void Move(Vector2 input);

    public abstract void Rotate(Vector2 input);

    public abstract void Interact();

    public abstract void Jump();


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
