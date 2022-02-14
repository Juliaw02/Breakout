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

    //public Vector3[] maskLocations4 =
    //{
        //new Vector3 {x = -7.5, y = 1.75, z = 0},
        //new Vector3 {x = -4.5, y = 0.75, z = 0},
        //new Vector3 {x = -1.5, y = -0.25, z = 0},
        //new Vector3 {x = 1.5, y = -0.25, z = 0},
        //new Vector3 {x = 7.5, y = -0.25, z = 0},
        //new Vector3 {x = -4.5, y = -0.25, z = 0},
        //new Vector3 {x = -7.5, y = 0.25, z = 0},
        //new Vector3 {x = -7.5, y = 3.75, z = 0}
    //};

    // Start is called before the first frame update
    void Start()
    {
        brickSprite = GetComponent<SpriteRenderer>();
        gameMaster.GetComponent<GameMaster>();

    }

    // Update is called once per frame
    void Update()
    {
        // if the object is a mask and has been hit once
        //if (gameObject.tag == "Mask" && numberOfHits == 1)
        //{
            // teleport?
            //int w = Random.Range(0, maskLocations4.Length);
            //transform.position = maskLocations4[w].position;
        //}
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
