using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    NavMeshAgent[] navAgents;

    private void Start()
    {
        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) // NavMesh컴포넌트가 포함된 모든 오브젝트를 찾아줌
            as NavMeshAgent[];  // NavMeshAgent 형태로 변환해서 저장 -> null 체크를 해줘야 함
        if (navAgents == null)
            return;
        Debug.Log("Number of agent = " + navAgents.Length);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                UpdateTargets(targetPosition);
                transform.position = targetPosition;
            }
        }
        
    }

    void UpdateTargets(Vector3 targetPosition)
    {
        foreach (NavMeshAgent agent in navAgents)
        {
            agent.destination = targetPosition;
        }
    }
}
