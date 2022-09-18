using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [HideInInspector]
    public GrabBase hand;
    // ��Ҵ�.
    virtual public void Catch(GrabBase whereHand)
    {
        // ���� ���� ����ϰڴ�.
        hand = whereHand;
    }

    // ���Ҵ�.
    virtual public void Release()
    {
        // ���� ����ִ� �վ� ���� �ؾ���
        if (hand)
        {
            hand.ForgotGrabObject();
        }
        hand = null;
    }

}
