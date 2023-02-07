using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Transform target;
    public PlayerController playerCtrl;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            target.position = new Vector3(hit.point.x, 0.3f, hit.point.z);

            if (Input.GetMouseButtonDown(0) && playerCtrl.playerState != PlayerState.Dead)
            {
                target.position = new Vector3(hit.point.x, 0.0f, hit.point.z);
                playerCtrl.lookDirection = target.position - transform.position;
                playerCtrl.StartCoroutine("Shot");
            }
        }
    }
}
