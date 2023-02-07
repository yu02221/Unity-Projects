using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public float fireCoolTime;
    public float startWait;

    private void OnEnable()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            GameObject eBullet = Instantiate(bulletFactory);
            eBullet.transform.position = transform.position;
            yield return new WaitForSeconds(fireCoolTime);
        }
    }
}
