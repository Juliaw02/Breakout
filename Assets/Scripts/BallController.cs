using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool ballLaunched = false;
    public Rigidbody2D ballRigidbody;
    public Vector2[] startDirections;
    public int randomNumber;
    public float ballForce;
    public Vector3 startPosition;

    public GameMaster gameMaster;

    public Transform explosion;

    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        gameMaster.GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the spacebar has been pressed
        if (Input.GetKeyDown(KeyCode.Space) && !ballLaunched)
        {
            randomNumber = Random.Range(0, startDirections.Length);
            ballRigidbody.AddForce(startDirections[randomNumber] * ballForce, ForceMode2D.Impulse);
            ballLaunched = true;
        }

        // If R is pressed reset the ball position
        if (Input.GetKeyDown(KeyCode.R))
        {
            ballRigidbody.velocity = Vector3.zero;
            transform.position = startPosition;
            ballLaunched = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the ball runs into an object with this tag
        if (other.CompareTag ("DefeatZone"))
        {
            // the ball's velocity will go to 0
            ballRigidbody.velocity = Vector3.zero;
            // tracking lost lives
            gameMaster.UpdateLives(-1);
            // the ball's position will reset back to the starting position
            transform.position = startPosition;
            ballLaunched = false;
        }
    }

    // Fish explosion particles
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Fish1"))
        {
            Transform newExplosion1 = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(newExplosion1.gameObject, 2.5f);
        }

        if (other.transform.CompareTag("Fish2"))
        {
            Transform newExplosion2 = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(newExplosion2.gameObject, 2.5f);
        }

        if (other.transform.CompareTag("Fish3"))
        {
            Transform newExplosion3 = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(newExplosion3.gameObject, 2.5f);
        }
    }
}
