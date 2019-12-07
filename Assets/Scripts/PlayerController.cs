using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject powerupIndicator;
    [SerializeField] private bool PushPowerup;
    [SerializeField] private bool DestroyPowerUp;
    [SerializeField] private float powerUpStrength = 15.0f;
    [SerializeField] private float speedPowerUp = 2;

    private Rigidbody playerRb;
    private TrailRenderer playerTrial;

    private float VerticalInput;
    private float HorizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerTrial = GetComponent<TrailRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void PlayerMovement()
    {
        float VerticalInput = Input.GetAxis("Vertical");
        float HorizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(HorizontalInput, 0, VerticalInput);

        playerRb.AddForce(movement.normalized * speed, ForceMode.Impulse);


        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup1"))
        {
            PushPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(Powerup1CountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }

        if (other.CompareTag("Powerup2"))
        {
            speed = speed * speedPowerUp;
            Destroy(other.gameObject);
            StartCoroutine(Powerup2CountdownRoutine());
            playerTrial.enabled = true;
        }

        if (other.CompareTag("Powerup3"))
        {
            Destroy(other.gameObject);
            DestroyPowerUp = true;
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(Powerup3CountdownRoutine());
        }

    }

    IEnumerator Powerup1CountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        PushPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator Powerup2CountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        speed = speed / speedPowerUp;
        playerTrial.enabled = false;
    }

    IEnumerator Powerup3CountdownRoutine()
    {
        yield return new WaitForSeconds(3);
        DestroyPowerUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && PushPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("Enemy") && DestroyPowerUp)
        {
            Destroy(collision.gameObject);
        }

    }
}
