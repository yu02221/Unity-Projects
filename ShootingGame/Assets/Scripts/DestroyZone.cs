using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // onTriggerExit �� Ȱ���ϸ� zone �ϳ��ε� ���� ����
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj == null)
            {
                Destroy(other.gameObject);
                return;
            }
            PlayerFire playerFire =  playerObj.GetComponent<PlayerFire>();
            // ����� ���� ������Ʈ�� Ǯ�� ��������
            if (other.gameObject.name.Contains("Big"))
            {
                playerFire.bigBulletObjPool.Add(other.gameObject);
            }
            else
            {
                playerFire.bulletObjectPool.Add(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            EnemyManager enemyManager = GameObject.Find("EnemyManager")
                .GetComponent<EnemyManager>();
            enemyManager.enemyObjectPool.Add(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

}
