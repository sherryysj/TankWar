using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // enemy variable
    public float moveSpeed = 3;
    private float attackTimer = 0;
    public int health;
    private string moveDirection = "h";
    private float directionTimer = 4f;
    private string[] moveDirections;

    // enemy tank effect reference
    public GameObject explosionEffect;

    // enemy bullet reference
    public GameObject bulletPrefab;

    void Start()
    {
        Debug.Log("Enemy Health: " + health);
        moveDirections = new string[4] { "up", "down", "left", "right" };
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
            int num = Random.Range(0, 4);
            moveDirection = moveDirections[num];
            directionTimer = 0;
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
    public void BeAttacked()
    {
        health -= 1;
        if (health == 0)
        {
            Die();
        }
    }

    // enemy die
    public void Die()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
