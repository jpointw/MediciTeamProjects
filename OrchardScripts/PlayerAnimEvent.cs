using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    public PlayerMove pm;
    public void OnWalkDust()
    {
        pm.OnWalkDust();
    }

    public void OnDashDust()
    {
        pm.OnDashDust();
    }
}
