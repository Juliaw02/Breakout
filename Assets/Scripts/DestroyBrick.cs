using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBrick : MonoBehaviour
{
    public int numberOfHits = 0;
    public int maxHits;
    public Sprite noHit;
    public Sprite oneHit;
    public SpriteRenderer brickSprite;
    public int brickValue;
    public Transform powerup;
    public GameMaster gameMaster;

    //public Vector3[] maskLocations;

    // Start is called before the first frame update
    void Start()
    {
        brickSprite = GetComponent<SpriteRenderer>();
        gameMaster.GetComponent<GameMaster>();

       // maskLocations = new Vector3[8];
       // maskLocations[0] = new Vector3(-7.5f, 1.75f, 0f);
       // maskLocations[1] = new Vector3(-4.5f, 0.75f, 0f);
       // maskLocations[2] = new Vector3(-1.5f, -0.25f, 0f);
       // maskLocations[3] = new Vector3(1.5f, -0.25f, 0f);
       // maskLocations[4] = new Vector3(7.5f, -0.25f, 0f);
       // maskLocations[5] = new Vector3(-4.5f, -0.25f, 0f);
       // maskLocations[6] = new Vector3(-7.5f, 0.75f, 0f);
       // maskLocations[7] = new Vector3(-7.5f, 3.75f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the object is a mask and has been hit once
       //if (gameObject.tag == "Mask" && numberOfHits >= 1 && numberOfHits < maxHits)
       // {
            // teleport?
           // int i = Random.Range(0, maskLocations.Length);
           // transform.position = maskLocations[i].position;
      //  }
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

            if (randomChance < 40)
            {
                Instantiate(powerup, this.transform.position, other.transform.rotation);
            }
        }
        
        // If brick is hit max # of times, update score and destroy brick
        if (numberOfHits >= maxHits)
        {
            gameMaster.UpdateScore(+brickValue);
            Destroy(this.gameObject);
        }

        // Exploding fish bricks
        GameObject[] fish3Box = GameObject.FindGameObjectsWithTag("Fish3Box");
        GameObject[] fish2Box = GameObject.FindGameObjectsWithTag("Fish2Box");
        GameObject[] fish1Box = GameObject.FindGameObjectsWithTag("Fish1Box");
        for (int i = 0; i < fish3Box.Length; i++)
        {
            if (gameObject.tag == "Fish3")
            {
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
