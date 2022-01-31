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

    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        gameMaster.GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ballLaunched)
        {
            randomNumber = Random.Range(0, startDirections.Length);
            ballRigidbody.AddForce(startDirections[randomNumber] * ballForce, ForceMode2D.Impulse);
            ballLaunched = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the ball runs into an object with this tag
        if (other.gameObject.tag == "DefeatZone")
        {
            // the ball's velocity will go to 0
            ballRigidbody.velocity = Vector3.zero;
            // tracking lost lives
            gameMaster.playerLives = gameMaster.playerLives - 1;
            // the ball's position will reset back to the starting position
            transform.position = startPosition;
            ballLaunched = false;
        }
    }
}
