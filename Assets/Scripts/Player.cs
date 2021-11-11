using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3;

    private SpriteRenderer sr;
    public Sprite[] tankSprite; // up, right, down, left

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    void Update()
    {

        // play horizontal move
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime, Space.World);

        // art work: change sprite to show player face which direction when play turn direction horizontally
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
        } else if (h > 0) 
        {
            sr.sprite = tankSprite[1];
        }

        // play vertical move
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.deltaTime, Space.World);

        // art work: change sprite to show player face which direction when play turn direction vertically
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
        }

    }
}
