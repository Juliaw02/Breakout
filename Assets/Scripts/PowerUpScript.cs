using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float starSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0.0f, -1.0f) * Time.deltaTime * starSpeed);

        // If the powerup falls below the paddle and screen
        if (transform.position.y < -6.0f)
        {
            Destroy(gameObject);
        }
    }
}
