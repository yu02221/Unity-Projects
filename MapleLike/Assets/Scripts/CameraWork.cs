using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    private GameObject player;

    private Vector3 offset;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();

        player = GameObject.FindGameObjectWithTag("Player");

        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, gm.beackGroundLeft, gm.beackGroundRight),
            Mathf.Clamp(transform.position.y, gm.beackGroundBottom, gm.beackGroundTop),
            transform.position.z);
    }
}
