using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f;

    private bool backUp = false;
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
        Movement();
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void Movement()
    {
        backUp = false;
        if(backUp == false)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            backUp = true;
            Debug.Log("Backing up");
            Vector3 BackUp = (player.transform.position + transform.position).normalized;
            enemyRb.AddForce(BackUp * speed);
            Movement();
        }
    }
    
}
