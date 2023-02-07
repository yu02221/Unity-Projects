using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Vector3 target;
    void Start()
    {
        target = GameObject.Find("Ball").transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 0.03f);
        transform.Rotate(new Vector3(0, 0, 2));
        if(transform.position == target)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Ball")
        {
            GameManager gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();
            gm.RestartGame();
        }
    }
}
