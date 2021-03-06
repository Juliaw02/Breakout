using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float playerInput;
    public float paddleSpeed;

    public float rScreenEdge;
    public float lScreenEdge;

    public GameMaster gameMaster;

    public AudioSource clipSource;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster.GetComponent<GameMaster>();
        clipSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Boundaries for paddle
        playerInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * paddleSpeed * playerInput);
        
        if (transform.position.x < lScreenEdge)
        {
            transform.position = new Vector2(lScreenEdge, transform.position.y);
        }

        if (transform.position.x > rScreenEdge)
        {
            transform.position = new Vector2(rScreenEdge, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the paddle hits the extra life powerup
        if (other.CompareTag("ExtraLife"))
        {
            clipSource.Play();
            gameMaster.UpdateLives(1);
            Destroy(other.gameObject);
        }
    }
}
