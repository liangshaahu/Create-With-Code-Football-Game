using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject focusPoint;
    public bool isOnGround = true;
    public bool powerUp = false;
    public GameObject powerUpIndicator;

    private Rigidbody playerRb;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        initialPos = gameObject.transform.position;
        Debug.Log(initialPos);
        powerUpIndicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focusPoint.transform.forward * speed * forwardInput);
        if (transform.position.y < -5.0f)
        {
            transform.position = initialPos;
            playerRb.velocity = new Vector3(0, 0, 0);
        }
        powerUpIndicator.transform.position = transform.position - new Vector3 (0, 0.5f, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayfromPlayer = (collision.gameObject.transform.position - transform.position);

        isOnGround = true;
        
        

        if (collision.gameObject.CompareTag("Enemy")&&powerUp)
        {
            
            enemyRb.AddForce(awayfromPlayer * 10, ForceMode.Impulse);
            Debug.Log("PowerUp");
            enemyRb.AddForce(awayfromPlayer * 10, ForceMode.Impulse);
        }
     
    }

    private void OnCollisionExit(Collision collision)
    {
        
        isOnGround = false;
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            powerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }

}
