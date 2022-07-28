using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Pun;

public class OrbBase : MonoBehaviourPun
{
    [SerializeField]
    protected float onShootSpeed;
    protected float orbSpeed;
    protected Transform parent;

    protected void Update()
    {
        OrbMoving();
    }
    protected void OnEnable() 
    {
        Init();
        if (parent) transform.parent = parent;
    }

    protected void OnDisable()
    {
        parent = null;
    }

    protected virtual void Init()
    {
        orbSpeed = 0;
    }
    protected void OrbMoving()
    {
        Vector3 dir = this.transform.forward;
        transform.position += dir * orbSpeed * Time.deltaTime;
    }
    public void SetParent(Transform tr)
    {
        if (tr.TryGetComponent<PhotonView>(out var pv) && pv.ViewID > 0)
            photonView.CustomRPC(this, "SetPRPC", RpcTarget.All, pv.ViewID);
        else
        {
            print("SetParentNotRpc");
            parent = tr;
            try{
                transform.parent = parent;
            }
            finally{}

        }
    }
    [PunRPC]
    protected void SetPRPC(int viewID)
    {
        parent = PhotonNetwork.GetPhotonView(viewID).transform;
        try
        {
            transform.parent = parent;
        }
        finally { }
    }
    Audio audio;
    [SerializeField] AudioClip clip;
    protected IEnumerator MoveToParent(Transform tr)
    {
        yield return new WaitForEndOfFrame();
        transform.parent = tr;
        transform.localPosition = transform.localEulerAngles = Vector3.zero;
        audio = AudioPool.instance.Play(clip.name, 1, tr.position, tr);
    }
    
    public void OrbFire()
    {
        photonView.CustomRPC(this, "CallRpc", RpcTarget.AllViaServer, this.transform.position, this.transform.forward);
    }

    [PunRPC]
    protected void CallRpc(Vector3 shootPosition, Vector3 forward)
    {
        RPCFire(shootPosition, forward);
    }

    protected virtual void RPCFire(Vector3 shootPosition, Vector3 forward)
    {
        this.transform.position = shootPosition;
        this.transform.forward = forward;

        this.transform.SetParent(null);
        orbSpeed = onShootSpeed;
        audio?.FadeOut();
    }
}
