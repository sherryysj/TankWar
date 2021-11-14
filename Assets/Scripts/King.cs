using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    // King variable
    public bool isDead;

    // King artwork reference
    private SpriteRenderer sr;
    public Sprite deadSprite; 

    // King effect reference
    public GameObject explosionEffect;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Die()
    {
        if (!isDead)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            sr.sprite = deadSprite;
            isDead = true;
        }

        //Game Over
    }
}
