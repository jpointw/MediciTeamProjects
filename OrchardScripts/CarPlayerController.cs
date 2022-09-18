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
    //w,a,s,d 입력이나 이동입력을 받아서 ControlTarget에 Move함수에 전달해주는 InputMoveAxis함수
    {
        controlTarget.Move(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch));
    }
    private void InputRotateAxis()
    //마우스 입력을 받아서 ControlTarget에 Rotate함수에 전달하는 InputRotateAxis함수
    {

        controlTarget.Rotate(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch));
    }
    private void InputInteractAction()
    //"E"키를 누르면 controlTarget의 Interact 함수를 호출하는 
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
