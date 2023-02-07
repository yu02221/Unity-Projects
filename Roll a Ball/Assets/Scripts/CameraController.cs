using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
        // Debug.Log(offset);
    }

    // LateUpdate : Update�� ������ �� �� ����
    // ���� ���� �����̰� ī�޶� ���󰡾� �ϹǷ� LateUpdate ���
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
