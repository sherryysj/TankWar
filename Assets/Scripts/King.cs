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

    // King Audio reference
    public AudioClip kingDie;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Die()
    {
        if (!isDead)
        {
            AudioSource.PlayClipAtPoint(kingDie, transform.position);
            sr.sprite = deadSprite;
            isDead = true;
            GameManager.Instance.IsKingDead = true;
        }

        //Game Over
    }
}
