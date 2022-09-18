using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestButton : MonoBehaviour
{

    public Transform hand;
    public LineRenderer lr;
    public OVRInput.Controller controller;
    Button btn;
    void Start()
    {

    }

    void Update()
    {
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo))
        {
            lr.SetPosition(1, ray.origin + ray.direction * 100);
            if (OVRInput.GetDown(OVRInput.Button.One, controller))
            {
                btn = hitinfo.transform.GetComponent<Button>();
                if (btn)
                {
                    PointerEventData eData = new PointerEventData(EventSystem.current);
                    btn.OnPointerDown(eData);

                }
            }
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 100);
        }
        if (OVRInput.GetUp(OVRInput.Button.One, controller))
        {
            
            if (btn)
            {
                PointerEventData eData = new PointerEventData(EventSystem.current);
                btn.OnPointerUp(eData);
                if (Physics.Raycast(ray, out hitinfo))
                {
                    Button currentBtn = hitinfo.transform.GetComponent<Button>();
                    if (btn == currentBtn)
                    {
                        btn.OnPointerClick(eData);
                    }
                    }
            }

        }
    }
}
