using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    GameManager gm;
    public GameObject hitEffect;
    public float speed;

    private void Start()
    {
        gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();
    }

    private void Update()
    {
        Vector3 dir = Vector3.down;
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            GameObject hit = Instantiate(hitEffect);
            hit.transform.position = transform.position;

            gm.lifeCount--;
            gm.SetLifeCount();
            if (gm.lifeCount <= 0)
            {
                gm.GameOver();
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
