using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // player variables
    public float moveSpeed = 3;
    public bool isProtected;
    private Vector3 bulletEulerAngles;
    private float attackTimer = 0.4f;
    private float protectTimer = 3f;
    

    // player tank artwork reference
    private SpriteRenderer sr;
    public Sprite[] tankSprite; // up, right, down, left

    // player tank effect reference
    public GameObject explosionEffect;
    public GameObject shieldEffect;

    // player bullet reference
    public GameObject bulletPrefab;

    // player audio reference
    public AudioClip tankMoveSound;
    public AudioClip tankIdleSound;
    public AudioSource moveAudio;
    public AudioClip playerDie;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        moveAudio.clip = tankIdleSound;
    }


    void Update()
    {
        if (GameManager.Instance.IsKingDead)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        // Control player attack interval
        if (attackTimer >= 0.4f)
        {
            Attack();
        } else
        {
            attackTimer += Time.deltaTime;
        }

        if (isProtected) 
        {
            shieldEffect.SetActive(true);
            if (protectTimer > 0)
            {
                protectTimer -= Time.deltaTime;
            }
            else
            {
                isProtected = false;
                shieldEffect.SetActive(false);
                protectTimer = 3f;
            }
        }


    }

    // Use fixed update to avoid player shaking problem
    private void FixedUpdate()
    {
        Move();
    }

    // player move
    private void Move()
    {
        // play horizontal move
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        // art work: change sprite to show player face which direction when play turn direction horizontally
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
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

        if ( h==0 && v==0)
        {
            moveAudio.clip = tankIdleSound;
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankMoveSound;
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

    }

    // Player attack
    private void Attack()
    {
        // generate bullet and rotate its direction according to player direction if player presses SPACE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // there is a bug to attach bullet to player as mother object, if the player move, bullet will change angles
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+bulletEulerAngles), transform);
            attackTimer = 0;
        }
    }

    // Player die
    private void Die()
    {
        if (!isProtected)
        {
            moveAudio.Stop();
            AudioSource.PlayClipAtPoint(playerDie, transform.position);
            Destroy(gameObject);
            GameManager.Instance.Recover();
        }
    }

}
