using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [HideInInspector]
    public GrabBase hand;
    // 잡았다.
    virtual public void Catch(GrabBase whereHand)
    {
        // 잡은 손을 기억하겠다.
        hand = whereHand;
    }

    // 놓았다.
    virtual public void Release()
    {
        // 나를 잡고있는 손아 나을 잊어줘
        if (hand)
        {
            hand.ForgotGrabObject();
        }
        hand = null;
    }

}
