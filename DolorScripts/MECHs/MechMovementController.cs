using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Photon.Pun;

public class MechMovementController : MonoBehaviourPun, IInitialize
{
    public void Reset() {
    #if UNITY_EDITOR
        centerEye = GetComponentInChildren<Camera>(true)?.transform; leftHandJoystick = Utility.FindInputReference(ActionMap.XRI_LeftHand_Locomotion, "Move");
    #endif
    }
    enum WalkState{Idle, Forward, Back, Left, Right, RotateLeft, RotateRight}
    
    [SerializeField]        Transform centerEye;

    [Header("Audio")]
    [SerializeField]        AudioClip footstep;

    [Header("Move")]
    [SerializeField]        InputActionReference leftHandJoystick;
    [SerializeField]        float moveSpeed = 1f;

    [Header("Rotation")]
    // [SerializeField]        private float timeToReachMaxRotSpeed = 1;
    [SerializeField]        float rotateStartThreshold = 30;
    [SerializeField]        float rotateFinishThreshold = 20;
    [SerializeField]        float maxRotationSpeed = 15;
    [Range(0, 10)]
    [SerializeField]        float rotatingLerpSpeed = 1;

    [Tooltip("If angle between hmd and robot is this value, the rotation speed becomes max")]
    [SerializeField]        float angleToReachMaxRotationSpeed = 90;
                            bool rotating;
                            bool rotateLeft;

    Transform tr;
    Animator anim;
    // Rigidbody rb;
    CharacterController cc;
    Vector3 moveDir = Vector3.zero;
    WalkState walkState;
    int[] walkStateHash;
    float deltaTime;
    float angle, absAngle;
    float rotSpeed;
    bool cachedMine;

    WalkState walkStateProperty
    {
        get { return walkState; }
        set 
        {
            if (walkState == value) return;

            walkState = value;
            photonView.CustomRPC(this, "CrossFade", RpcTarget.All, (int)walkState, rotating);
        }
    }

    [PunRPC]
    private void CrossFade(int state, bool rotating)
    {
        anim.CrossFade(walkStateHash[state], 0.2f);
    }

    void Awake()
    {
        cachedMine = photonView.Mine;

        string[] walkStateNames = System.Enum.GetNames(typeof(WalkState));
        walkStateHash = new int[walkStateNames.Length];
        for (int i = 0; i < walkStateNames.Length; i++)
            walkStateHash[i] = Animator.StringToHash(walkStateNames[i]);

        anim = GetComponent<Animator>();
        if (cachedMine)
        {
            tr = GetComponent<Transform>();
            // rb = GetComponent<Rigidbody>();
            cc = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cachedMine == false) return;

        deltaTime = Time.deltaTime;
        UpdateMove();
        UpdateRotate();
        cc.Move(moveDir * deltaTime * moveSpeed);
    }

    private void UpdateMove()
    {
        Vector2 inputDir = leftHandJoystick.action.ReadValue<Vector2>();

        float absX = Mathf.Abs(inputDir.x);
        float absY = Mathf.Abs(inputDir.y);

        // walk
        if (absX > 0.5f || absY > 0.5f)
        {
            // move left or right
            if (absX > absY)
            {
                int round = Mathf.RoundToInt(inputDir.x);
                moveDir = tr.right * round;

                walkStateProperty = round == 1 ? WalkState.Right : WalkState.Left;
            }
            //move forward or backward
            else
            {
                int round = Mathf.RoundToInt(inputDir.y);
                moveDir = tr.forward * round;

                walkStateProperty = round == 1 ? WalkState.Forward : WalkState.Back;
            }
        }
        // not walk
        else
        {
            moveDir = Vector3.zero;
            walkStateProperty = rotating ? (rotateLeft ? WalkState.RotateLeft : WalkState.RotateRight) : WalkState.Idle;
        }
    }

    private void UpdateRotate()
    {
        CalculateAngle();

        // Start rotate when angle between robot's forward direction and centereye's forward direction larger than threshold
        if (rotating == false)
        {
            if (absAngle > rotateStartThreshold)
            {
                rotating = true;
                rotateLeft = angle > 0;
                if (walkState == WalkState.Idle)
                    walkStateProperty = rotateLeft ? WalkState.RotateLeft : WalkState.RotateRight;
                rotSpeed = 0;
            }
            else if (rotSpeed > 0.1f)
            {
                rotSpeed = Mathf.Lerp(rotSpeed, 0, deltaTime * rotatingLerpSpeed);
                Rotate();
            }
            else
            {
                rotSpeed = 0;
            }
        }
        // Rotating...
        else
        {
            if (absAngle > rotateFinishThreshold)
            {
                float targetSpeed = maxRotationSpeed * Mathf.Clamp01(absAngle/angleToReachMaxRotationSpeed);
                rotSpeed = Mathf.Lerp(rotSpeed, targetSpeed, deltaTime * rotatingLerpSpeed);
                Rotate();
            }
            else
            {
                rotating = false;
                if ((int)walkStateProperty >= (int)WalkState.RotateLeft)
                    walkStateProperty = WalkState.Idle;
            }
        }
    }

    void CalculateAngle()
    {
        angle = Vector3.SignedAngle(Vector3.ProjectOnPlane(centerEye.forward, tr.up), tr.forward, tr.up);
        absAngle = Mathf.Abs(angle);
    }
    void Rotate()
    {
        if ((int)walkState < (int)WalkState.RotateLeft)
            tr.rotation = Quaternion.RotateTowards(tr.rotation, Quaternion.Euler(tr.eulerAngles - tr.up * angle), deltaTime * rotSpeed);
        else
            anim.SetFloat("TurnSpeed", rotSpeed/22.5f);
    }

    public void PlayFootStepSound()
    {
        if (Physics.Raycast(tr.position+tr.up*0.1f, -tr.up, 0.2f, LayerMask.GetMask("Ground")))
            // audio.PlayOneShot(footstep);
            AudioPool.instance.Play(footstep.name, 2, tr.position);
    }

#region 
    // IEnumerator IEStartRotate()
    // {
    //     float angle = 0;

    //     while(true)
    //     {
    //         Vector3 projectedCenterEyeFwdDir = ;
    //         angle = Vector3.SignedAngle(Vector3.ProjectOnPlane(centerEye.forward, tr.up), tr.forward, tr.up);
    //         if (Mathf.Abs(angle) > rotateStartThreshold)
    //         {
    //             // Increase Rotation Speed
    //             for (float t = 0; t < 1; t += deltaTime / timeToReachMaxRotSpeed)
    //             {
    //                 projectedCenterEyeFwdDir = Vector3.ProjectOnPlane(centerEye.forward, tr.up);
    //                 angle = Vector3.SignedAngle(projectedCenterEyeFwdDir, tr.forward, tr.up);
    //                 targetRot = Quaternion.Euler(tr.eulerAngles - tr.up * angle);

    //                 tr.rotation = Quaternion.RotateTowards(tr.rotation, targetRot, deltaTime * maxRotationSpeed * t);
    //                 yield return null;
    //             }

    //             // Rotate with fixed speed
    //             while (Mathf.Abs(angle) > rotateFinishThreshold)
    //             {
    //                 projectedCenterEyeFwdDir = Vector3.ProjectOnPlane(centerEye.forward, tr.up);
    //                 angle = Vector3.SignedAngle(projectedCenterEyeFwdDir, tr.forward, tr.up);

    //                 targetRot = Quaternion.Euler(tr.eulerAngles - tr.up * angle);
    //                 tr.rotation = Quaternion.RotateTowards(tr.rotation, targetRot, deltaTime * maxRotationSpeed);
    //                 yield return null;
    //             }

    //             // Decrease Rotation Speed
    //             for (float t = 1; t > 0; t -= deltaTime / timeToReachMaxRotSpeed)
    //             {
    //                 projectedCenterEyeFwdDir = Vector3.ProjectOnPlane(centerEye.forward, tr.up);
    //                 angle = Vector3.SignedAngle(projectedCenterEyeFwdDir, tr.forward, tr.up);

    //                 targetRot = Quaternion.Euler(tr.eulerAngles - tr.up * angle);

    //                 tr.rotation = Quaternion.RotateTowards(tr.rotation, targetRot, deltaTime * maxRotationSpeed * t);
    //                 yield return null;
    //             }
    //         }

    //         yield return null;
    //     }
    // }
#endregion
}
