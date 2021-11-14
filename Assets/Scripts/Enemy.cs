using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // enemy variable
    public float moveSpeed;
    private float attackTimer = 1f;
    public int health;
    private string moveDirection = "down";
    private float directionTimer = 1f;
    private string[] moveDirections;

    // enemy tank effect reference
    public GameObject explosionEffect;

    // enemy bullet and tank reference
    public GameObject bulletPrefab;
    public GameObject[] enemyPrefabs;

    void Start()
    {
        Debug.Log("Enemy Health: " + health);
        moveDirections = new string[8] { "up", "down", "left", "left", "right", "right", "down", "down" };
    }

    void Update()
    {
        // call enemy attack automatically every second
        if (attackTimer >= 1f)
        {
            Attack();
        }
        else
        {
            attackTimer += Time.deltaTime;
        }

        // change enemy direction automatically every three second
        if (directionTimer > 3f)
        {
            changeDirection();
        } else
        {
            directionTimer += Time.deltaTime;
        }
    }

    // Use fixed update to avoid enemy shaking problem
    private void FixedUpdate()
    {
        Move();
    }


    private void changeDirection()
    {
        // down/right/left directions has more possibility to make enemy more likely to go to the king
        int num = Random.Range(0, 8);
        Debug.Log(moveDirections[num]);
        moveDirection = moveDirections[num];
        directionTimer = 0;
    }

    // enemy auto move
    private void Move()
    {

        if (moveDirection == "up")
        {
            transform.Translate(Vector3.up * 1 * moveSpeed * Time.fixedDeltaTime, Space.World);
            // art work: change sprite to show enemy face which direction when turn directions
            transform.eulerAngles = new Vector3(0, 0, 0);

        } else if (moveDirection == "down")
        {
            transform.Translate(Vector3.up * -1 * moveSpeed * Time.fixedDeltaTime, Space.World);
            transform.eulerAngles = new Vector3(0, 0, -180);

        } else if (moveDirection == "left")
        {
            transform.Translate(Vector3.right * -1 * moveSpeed * Time.fixedDeltaTime, Space.World);
            transform.eulerAngles = new Vector3(0, 0, 90); 
        } else
        {
            transform.Translate(Vector3.right * 1 * moveSpeed * Time.fixedDeltaTime, Space.World);
            transform.eulerAngles = new Vector3(0, 0, -90);
        }

    }

    // enemy attack
    private void Attack()
    {
        // generate bullet and rotate its direction according to enemy direction
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
        attackTimer = 0;
    }

    // enemy is attacked
    private void BeAttacked()
    {
        health -= 1;

        // change enemy type according to enemy health and call die if health is 0
        if (health == 2)
        {
            Instantiate(enemyPrefabs[1], transform.position, transform.rotation);
            Destroy(gameObject);
        } else if (health == 1)
        {
            Instantiate(enemyPrefabs[0], transform.position, transform.rotation);
            Destroy(gameObject);
        } else if (health == 0)
        {
            Die();
        }
    }

    // enemy die
    private void Die()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // enemy will change direction if collide with the other enmey and airwall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "AirWall")
        {
            Debug.Log("i AM WORKING");
            changeDirection();
        }
    }
}
