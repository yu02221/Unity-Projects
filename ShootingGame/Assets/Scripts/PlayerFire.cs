using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 총알 생산 공장
    public GameObject bulletFactory;
    public GameObject bigBulletFactory;
    // 오브젝트 풀에 넣을 총알 개수
    public int poolSize = 10;
    // 오브젝트풀 배열
    public List<GameObject> bulletObjectPool;
    public List<GameObject> bigBulletObjPool;
    // 총구
    public GameObject firePosition;

    private void Start()
    {
        // 풀 생성
        bulletObjectPool = new List<GameObject>();
        bigBulletObjPool = new List<GameObject>();

        // 풀에 오브젝트 저장
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
        // 리스트 안에 오브젝트가 있다면
        if (bulletObjectPool.Count > 0)
        {
            // 비활성화 된 오브젝트를 하나 가져옴
            GameObject bullet = bulletObjectPool[0];
            // 오브젝트 위치 지정
            bullet.transform.position = transform.position;
            // 오브젝트 활성화
            bullet.SetActive(true);
            // 활성화된 오브젝트를 제거
            bulletObjectPool.Remove(bullet);
        }
        else
        {
            GameObject bullet = Instantiate(bulletFactory);
            // 오브젝트 위치 지정
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
