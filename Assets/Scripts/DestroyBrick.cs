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

    // Start is called before the first frame update
    void Start()
    {
        brickSprite = GetComponent<SpriteRenderer>();
        gameMaster.GetComponent<GameMaster>();
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

        transform.GetComponent<SpriteRenderer>().sprite = oneHit;

        if (this.transform.CompareTag ("Bricks"))
        {
            int randomChance = Random.Range(1, 101);

            if (randomChance < 30)
            {
                Instantiate(powerup, this.transform.position, other.transform.rotation);
            }
        }
        

        if (numberOfHits >= maxHits)
        {
            gameMaster.UpdateScore(+brickValue);

            Destroy(this.gameObject);
        }
    }
}
