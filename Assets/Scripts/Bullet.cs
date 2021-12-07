using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;

    public bool isPlayerBullet;

    // bullet Audio reference
    public AudioClip wallDestroy;

    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "StoneWall":
                if (isPlayerBullet)
                {
                    AudioSource.PlayClipAtPoint(wallDestroy, transform.position);
                }
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "SteelWall":
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    collision.SendMessage("BeAttacked");
                    Destroy(gameObject);
                }
                break;
            case "Player":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "King":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "AirWall":
                Destroy(gameObject);
                break;
            default:
                break;
        }


    }
}
