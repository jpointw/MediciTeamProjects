using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControlable : Controlable
{
    public Transform characterSeat;
    public Transform ridePosition; 

    [SerializeField]
    private WheelCollider[] wheelColliders; 
    [SerializeField]
    private GameObject[] wheelMeshes;

    public CharacterControlable driveCharacter;
    public override void Interact()
    {
       StartCoroutine(GetOut());
    }
    private IEnumerator GetOut()
    {
        var controller = FindObjectOfType<Controller>();
        var charBody = driveCharacter.GetComponent<Rigidbody>();
        var charCollider = driveCharacter.GetComponent<CapsuleCollider>();
        var cc = GetComponent<CharacterController>();

        yield return new WaitForSeconds(0.5f);
        while (Vector3.Distance(driveCharacter.transform.position,ridePosition.position)>0.1f)
        {
            yield return null;
            driveCharacter.transform.position += (ridePosition.position - driveCharacter.transform.position).normalized * Time.deltaTime * 3f;
        }

        cc.enabled = true;
        charBody.isKinematic = false;
        charCollider.isTrigger = false;

        driveCharacter.transform.SetParent(null);

        controller.ChangeControlTarget(this, driveCharacter);
        driveCharacter = null;

    }

    public override void Move(Vector2 input)
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat; // Quaternion
            Vector3 position;
            wheelColliders[i].GetWorldPose(out position, out quat);
            wheelMeshes[i].transform.SetPositionAndRotation(position, quat);
        }

        wheelColliders[0].steerAngle = wheelColliders[1].steerAngle = input.x * 20f;
        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = input.y * (2000f / 4);
        }
    }

    public override void Rotate(Vector2 input)
    {
        if (cameraArm != null)
        {
            Vector3 camAngle = cameraArm.rotation.eulerAngles;
            float x = camAngle.x - input.y;

            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            else
            {
                x = Mathf.Clamp(x, 335f, 361f);
            }

            cameraArm.rotation = Quaternion.Euler(x, camAngle.y + input.x, camAngle.z);
        }
    }
}

