using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerRotate : NetworkBehaviour
{
    public float rotSpeed = 200f;

    public override void FixedUpdateNetwork()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
            return;

        if (GetInput(out NetworkInputData data))
        {
            transform.eulerAngles = new Vector3(
                0, data.mx * rotSpeed * Runner.DeltaTime, 0);
        }
    }
}
