using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BombAction : NetworkBehaviour
{
    public GameObject bombEffect;

    public int attackPower = 10;
    public float explosionRadious = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] cols = Physics.OverlapSphere(
            transform.position, explosionRadious, 1 << LayerMask.NameToLayer("Enemy"));

        for (int i = 0; i < cols.Length; i++)
            cols[i].GetComponent<EnemyFSM>().HitEnemy(attackPower);

        Runner.Spawn(bombEffect, transform.position, Quaternion.identity);

        Runner.Despawn(Object);
    }
}
