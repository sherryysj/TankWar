using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorn : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    void Start()
    {
        Invoke("TankBorn", 1f);
    }

    private void TankBorn()
    {
        int tankRate = Random.Range(1, 100);
        if (tankRate <= 60)
        {
            Instantiate(enemyPrefab[0], transform.position, Quaternion.identity, GameManager.Instance.CurrentRound.transform);
        } else if (tankRate <= 90)
        {
            Instantiate(enemyPrefab[1], transform.position, Quaternion.identity, GameManager.Instance.CurrentRound.transform);
        } else
        {
            Instantiate(enemyPrefab[2], transform.position, Quaternion.identity, GameManager.Instance.CurrentRound.transform);
        }
        Destroy(gameObject);
    }
}
