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
    public Transform wallParticles;
    public Transform defeatParticles;

    public AudioSource clipSource;
    public AudioClip[] bottleArray;
    public AudioClip[] wrapperArray;
    public AudioClip[] canArray;
    public AudioClip[] maskArray;
    public AudioClip[] deathArray;
    public AudioClip[] bounceArray;
    public AudioClip[] turtleArray;

    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        gameMaster.GetComponent<GameMaster>();
        clipSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Launch the ball if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !ballLaunched)
        {
            randomNumber = Random.Range(0, startDirections.Length);
            ballRigidbody.AddForce(startDirections[randomNumber] * ballForce, ForceMode2D.Impulse);
            ballLaunched = true;
        }

        // Reset the ball's position if R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            int bounceIndexA;
            bounceIndexA = Random.Range(0, bounceArray.Length);
            clipSource.PlayOneShot(bounceArray[bounceIndexA]);

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
            clipSource.PlayOneShot(deathArray[0]);

            // The ball's velocity will go to 0
            ballRigidbody.velocity = Vector3.zero;
            // Tracking lost lives
            gameMaster.UpdateLives(-1);
            // The ball's position will reset back to the starting position
            transform.position = startPosition;
            ballLaunched = false;
        }

        // Defeat particles
        if (other.gameObject.name == "Defeat")
        {
            Transform newDefeatParticles = Instantiate(defeatParticles, other.transform.position, this.transform.rotation);
            Destroy(newDefeatParticles.gameObject, 2.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        DestroyBrick destroyBrick = other.gameObject.GetComponent<DestroyBrick>();

        // Bottle sound effects
        if (other.gameObject.name == "1HitBottle")
        {
            int bottleIndex;
            bottleIndex = Random.Range(0, bottleArray.Length);
            clipSource.PlayOneShot(bottleArray[bottleIndex]);
        }

        // Wrapper sound effects
        if (other.gameObject.name == "2HitWrapper")
        {
            clipSource.PlayOneShot(wrapperArray[0]);
        }

        // Can sound effects
        if (other.gameObject.name == "UnbreakableCan")
        {
            int canIndex;
            canIndex = Random.Range(0, canArray.Length);
            clipSource.PlayOneShot(canArray[canIndex]);
        }

        // Mask sound effects
        if (other.gameObject.name == "5FaceMask")
        {
            clipSource.PlayOneShot(maskArray[0]);
        }

        // Fish explosion particles
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

        // Wall particles
        if (other.gameObject.name == "Wall")
        {
            Transform newWallParticles = Instantiate(wallParticles, this.transform.position, other.transform.rotation);
            Destroy(newWallParticles.gameObject, 1.5f);
        }

        // Wall bounce sound
        if (other.gameObject.name == "Wall" || other.gameObject.name == "Paddle")
        {
            int bounceIndexB;
            bounceIndexB = Random.Range(0, bounceArray.Length);
            clipSource.PlayOneShot(bounceArray[bounceIndexB]);
        }

        // Turtle shell sounds
        if (other.gameObject.name == "BigTurtle")
        {
            clipSource.PlayOneShot(turtleArray[0]);
        }
        if (other.gameObject.name == "MedTurtle")
        {
            clipSource.PlayOneShot(turtleArray[1]);
        }
        if (other.gameObject.name == "SmallTurtle")
        {
            clipSource.PlayOneShot(turtleArray[2]);
        }
    }
}
