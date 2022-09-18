using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controlable : MonoBehaviour
    //abstract란? 추상클래스는 인스턴스(객체)를 만들 수 없는 특별한 클래스이다.
    //추상클래스의 목적은 추상메서드를 만들고 이것을 상속을 통해서 파생클래스에서 구현하도록 하는 것이다.
{
    //함수를 abstract로 선언하면 Controlable을 상속받는 클래스들은 이 함수를 강제적으로 사용해야한다.
    //비이클,캐릭터 컨트롤러블에 있던 카메라암소켓과 카메라 암을 여기서 상속시켜준다.
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
