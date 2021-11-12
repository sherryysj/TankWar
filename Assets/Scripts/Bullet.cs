using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "StoneWall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "SteelWall":
                Destroy(gameObject);
                break;
            case "Enemy":
                Destroy(gameObject);
                break;
            case "Player":
                Destroy(gameObject);
                break;
            case "King":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
