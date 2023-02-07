using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

enum PlayerButtons
{
    Jump = 0,
    Fire0 = 1,
    Fire1 = 2,
}

public struct NetworkInputData : INetworkInput
{
    public Vector3 dir;
    public NetworkButtons Buttons;
    public float mx;
}
