using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // �Ѿ� ���� ����
    public GameObject bulletFactory;
    public GameObject bigBulletFactory;
    // ������Ʈ Ǯ�� ���� �Ѿ� ����
    public int poolSize = 10;
    // ������ƮǮ �迭
    public List<GameObject> bulletObjectPool;
    public List<GameObject> bigBulletObjPool;
    // �ѱ�
    public GameObject firePosition;

    private void Start()
    {
        // Ǯ ����
        bulletObjectPool = new List<GameObject>();
        bigBulletObjPool = new List<GameObject>();

        // Ǯ�� ������Ʈ ����
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            GameObject bigBullet = Instantiate(bigBulletFactory);
            bulletObjectPool.Add(bullet);
            bigBulletObjPool.Add(bigBullet);
            bigBullet.SetActive(false);
            bullet.SetActive(false);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            BigFire();
        }
#endif
    }

    public void Fire()
    {
        // ����Ʈ �ȿ� ������Ʈ�� �ִٸ�
        if (bulletObjectPool.Count > 0)
        {
            // ��Ȱ��ȭ �� ������Ʈ�� �ϳ� ������
            GameObject bullet = bulletObjectPool[0];
            // ������Ʈ ��ġ ����
            bullet.transform.position = transform.position;
            // ������Ʈ Ȱ��ȭ
            bullet.SetActive(true);
            // Ȱ��ȭ�� ������Ʈ�� ����
            bulletObjectPool.Remove(bullet);
        }
        else
        {
            GameObject bullet = Instantiate(bulletFactory);
            // ������Ʈ ��ġ ����
            bullet.transform.position = transform.position;
        }
    }

    public void BigFire()
    {
        if (bigBulletObjPool.Count > 0)
        {
            GameObject bigBullet = bigBulletObjPool[0];
            bigBullet.transform.position = transform.position;
            bigBullet.SetActive(true);
            bigBulletObjPool.Remove(bigBullet);
        }
        else
        {
            GameObject bigBullet = Instantiate(bigBulletFactory);
            bigBullet.transform.position = transform.position;
        }
    }
}
