using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject powerupIndicator;
    [SerializeField] private bool PushPowerup;
    [SerializeField] private float powerUpStrength = 15.0f;
    [SerializeField] private float speedPowerUp = 2;
    [SerializeField] private GameObject playerTrial;

    private Rigidbody playerRb;
    private float VerticalInput;
    private float HorizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float VerticalInput = Input.GetAxis("Vertical");
        float HorizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(HorizontalInput, 0, VerticalInput);

        //playerRb.transform.Translate(HorizontalInput, 0, VerticalInput, Space.World);
        playerRb.AddForce(movement.normalized * speed, ForceMode.Impulse);


        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup1"))
        {
            PushPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }

        if (other.CompareTag("Powerup2"))
        {
            speed = speed * speedPowerUp;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            playerTrial.gameObject.SetActive(true);
        }

    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        PushPowerup = false;
        speed = speed / speedPowerUp;
        powerupIndicator.gameObject.SetActive(false);
        playerTrial.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && PushPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }

    }
}
