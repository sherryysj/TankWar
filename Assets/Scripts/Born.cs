using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        Invoke("TankBorn", 1f);
    }

    private void TankBorn()
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
