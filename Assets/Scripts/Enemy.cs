using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // enemy variable
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float attackTimer = 0;
    public int health;

    // enemy tank effect reference
    public GameObject explosionEffect;

    // enemy bullet reference
    public GameObject bulletPrefab;

    void Start()
    {
        Debug.Log("Enemy Health: " + health);
    }

    void Update()
    {
        if (attackTimer >= 1f)
        {
            // update to auto attack
            Attack();
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }

    // Use fixed update to avoid enemy shaking problem
    private void FixedUpdate()
    {
        // update to auto move
        Move();
    }

    // enemy move
    private void Move()
    {
        // play horizontal move
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        // art work: change sprite to show player face which direction when play turn direction horizontally
        if (h < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90); // need to test the value
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
            bulletEulerAngles = new Vector3(0, 0, -90);
        }

        // restrict player use both horizontal and vertical key at the same time to control the player
        if (h != 0)
        {
            return;
        }

        // play vertical move
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        // art work: change sprite to show player face which direction when play turn direction vertically
        if (v < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -180);
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
    }

    // enemy attack
    private void Attack()
    {
        // generate bullet and rotate its direction
        Debug.Log("Call Attack");
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
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
