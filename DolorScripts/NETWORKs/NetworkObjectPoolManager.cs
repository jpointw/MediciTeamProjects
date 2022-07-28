using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class NetworkObjectPoolManager : MonoBehaviourPunCallbacks
{
    public GameObject networkObjectPool;
    private GameObject spawnedPool;

    IEnumerator Start() {
        yield return new WaitForSeconds(3);
        // if (PhotonNetwork.SingleMode == true)
            Instantiate(networkObjectPool);
    }

    public override void OnLeftRoom()
    {
        spawnedPool.GetComponent<NetworkObjectPool>().DestroyPool();
        GC.Collect();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GC.Collect();
    }
}
