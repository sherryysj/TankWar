using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // enemy variable
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float attackTimer = 0.4f;

    // enemy tank artwork reference
    private SpriteRenderer sr;
    public Sprite[] tankSprite; // up, right, down, left

    // enemy tank effect reference
    public GameObject explosionEffect;

    // enemy bullet reference
    public GameObject bulletPrefab;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (attackTimer >= 0.4f)
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
            sr.sprite = tankSprite[3]; //use Euler to update the sprite
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
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
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
    }

    // enemy attack
    private void Attack()
    {
        // generate bullet and rotate its direction
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        attackTimer = 0;

    }

    // enemy die
    public void Die()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
