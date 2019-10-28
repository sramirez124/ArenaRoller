using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f;

    private GameObject player;
    private Rigidbody enemyRb;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        Vector3 runAway = new Vector3
        enemyRb.AddForce(lookDirection * speed);

        enemyRb.AddForce(transform.position - player.transform.position, 0, transform.position - player.transform.position);

        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

}
