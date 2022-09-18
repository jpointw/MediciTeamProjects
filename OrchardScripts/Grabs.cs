using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabs : GrabBase
{
    LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    
    public float radius = 0.5f;
    public OVRInput.Controller hand;
    GameObject grabObject = null;
    public float kAdujstForce = 3;
    void Update()
    {
        if (grabObject == null)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            lr.SetPosition(0, ray.origin);
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(ray, out hitInfo);
            if (isHit)
            {
                lr.SetPosition(1, hitInfo.point);
            }
            else
            {
                lr.SetPosition(1, ray.origin + ray.direction * 100);
            }

            if (isHit && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, hand))
            {
                Grabbable mg = hitInfo.transform.GetComponent<Grabbable>();
               

                if (mg)
                {
                    mg.Release();
                   
                    grabObject = hitInfo.transform.gameObject;
                    grabObject.GetComponent<Rigidbody>().isKinematic = true;
                    grabObject.transform.parent = transform;
                    
                    
                    mg.Catch(this);
                    lr.enabled = false;
                }
                
            }
        }
        else  // else if (seed != null)
        {
            grabObject.transform.position = Vector3.Lerp(grabObject.transform.position, transform.position, Time.deltaTime * 4);
            grabObject.transform.rotation = Quaternion.Lerp(grabObject.transform.rotation, transform.rotation, Time.deltaTime * 4);
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, hand))
            {
                grabObject.transform.parent = null;
                Rigidbody rb = grabObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * kAdujstForce;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);

                Grabbable mg = grabObject.GetComponent<Grabbable>();
                if (mg)
                {
                    mg.Catch(null);
                }
                

                grabObject = null;
                lr.enabled = true;
            }
        }
    }
    override public void ForgotGrabObject()
    {
        grabObject = null;
        lr.enabled = true;
    }
}