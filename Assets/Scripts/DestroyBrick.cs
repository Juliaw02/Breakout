using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBrick : MonoBehaviour
{
    public int numberOfHits = 0;
    public int maxHits;
    public int brickValue;

    public Sprite noHit;
    public Sprite oneHit;
    public SpriteRenderer brickSprite;

    public Transform powerup;
    public GameMaster gameMaster;

    public Transform[] maskLocations;

    public AudioSource clipSource;
    public AudioClip[] wrapperArray;
    public AudioClip[] maskArray;

    // Start is called before the first frame update
    void Start()
    {
        brickSprite = GetComponent<SpriteRenderer>();
        gameMaster.GetComponent<GameMaster>();
        clipSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Collision rather than Trigger so the ball actually collides
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Number of hits goes up every time the ball collides with the bricks
        numberOfHits++;
        // Change sprite after first collision
        transform.GetComponent<SpriteRenderer>().sprite = oneHit;

        // Powerups
        if (this.transform.CompareTag("Bricks"))
        {
            int randomChance = Random.Range(1, 101);
            // 25% chance
            if (randomChance < 25)
            {
                Instantiate(powerup, this.transform.position, other.transform.rotation);
            }
        }

        // Update score and destroy brick if it is hit max # of times
        if (numberOfHits >= maxHits)
        {
            gameMaster.UpdateScore(+brickValue);
            Destroy(this.gameObject);
        }

        // Wrapper crumple sound (1st hit)
        if (gameObject.name == "2HitWrapper" && numberOfHits < maxHits)
        {
            clipSource.PlayOneShot(wrapperArray[0]);
        }

        // Teleporting mask bricks
        if (gameObject.tag == "Mask" && numberOfHits == 1)
        {
            // Teleport to random location from spawn points
            int u = Random.Range(0, maskLocations.Length);
            transform.position = maskLocations[u].position;
            clipSource.PlayOneShot(maskArray[0]);
        }

        // Exploding fish bricks
        GameObject[] fish3Box = GameObject.FindGameObjectsWithTag("Fish3Box");
        GameObject[] fish2Box = GameObject.FindGameObjectsWithTag("Fish2Box");
        GameObject[] fish1Box = GameObject.FindGameObjectsWithTag("Fish1Box");
        for (int i = 0; i < fish3Box.Length; i++)
        {
            // If the 3rd fish was hit
            if (gameObject.tag == "Fish3")
            {
                // Destroy the bricks that are in the fish's group (surrounding the fish)
                Destroy(fish3Box[i]);
                gameMaster.UpdateScore(+brickValue);
            }
        }
        for (int q = 0; q < fish2Box.Length; q++)
        {
            if (gameObject.tag == "Fish2")
            {
                Destroy(fish2Box[q]);
                gameMaster.UpdateScore(+brickValue);
            }
        }
        for (int p = 0; p < fish1Box.Length; p++)
        {
            if (gameObject.tag == "Fish1")
            {
                Destroy(fish1Box[p]);
                gameMaster.UpdateScore(+brickValue);
            }
        }
    }
}
