using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerController : Controller
{
    
    void Start()
    {
    }

    void Update()
    {
        InputMoveAxis();
        InputRotateAxis();
        InputInteractAction();
       /* InputJumpAction();
*/
    }
    private void InputMoveAxis()
    //w,a,s,d �Է��̳� �̵��Է��� �޾Ƽ� ControlTarget�� Move�Լ��� �������ִ� InputMoveAxis�Լ�
    {
        controlTarget.Move(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch));
    }
    private void InputRotateAxis()
    //���콺 �Է��� �޾Ƽ� ControlTarget�� Rotate�Լ��� �����ϴ� InputRotateAxis�Լ�
    {

        controlTarget.Rotate(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch));
    }
    private void InputInteractAction()
    //"E"Ű�� ������ controlTarget�� Interact �Լ��� ȣ���ϴ� 
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)) ;
        {
            controlTarget.Interact();
        }
    }
    /*private void InputJumpAction()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            controlTarget.Jump();
        }
    }*/
}
