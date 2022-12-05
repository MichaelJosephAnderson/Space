using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroid01;
    [SerializeField]
    private GameObject asteroid02;
    [SerializeField]
    private GameObject asteroid03;
    [SerializeField]
    private float fireRate;
    public float difficulty;

    private GameManager gameManager;

    public List<GameObject> spawners;

    private GameObject toSpawn;
    private float numToSpawn;

    private float _timer;
    private float timerSpeed = 2f;

    private void Start()
    { 
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        difficulty = gameManager.difficultyLevel;
        difficulty = Mathf.Floor(difficulty);
        difficulty = Mathf.Clamp(difficulty, 0, 5);

        if (_timer >= fireRate)
        {
            Spawn();
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime * timerSpeed;
        }
    }

    public void Spawn()
    {
        GetNumberToSpawn();
        StartCoroutine("WaitForSpawn");

    }

    IEnumerator WaitForSpawn()
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            float randTime = Random.Range(0, 2.5f);

            GameObject ast = Instantiate(GetAsteroidToSpawn());
            int spawnpoint = Random.Range(0, spawners.Count);
            Vector3 adjustedPosition;
            AdjustSpawn(spawners[spawnpoint].transform, out adjustedPosition);
            ast.transform.position = adjustedPosition;
            ast.transform.rotation = spawners[spawnpoint].transform.rotation;
            ast.tag = spawners[spawnpoint].tag;


            yield return new WaitForSeconds(randTime);
        }

    }

    void AdjustSpawn(Transform currentSpawn, out Vector3 newPosition)
    {
        Vector3 targetDirection = currentSpawn.position - new Vector3(0, 0, 0);
        int randDir = Random.Range(-1, 2);
        Vector3 moveDir = Vector3.Cross(targetDirection, Vector3.up).normalized;
        if (randDir < 0)
        {
            newPosition = currentSpawn.position - moveDir * Random.Range(1, 2.5f);
        }
        else if (randDir > 0)
        {
            newPosition = currentSpawn.position + moveDir * Random.Range(1, 2.5f);
        }
        else
        {
            newPosition = currentSpawn.position;
        }
    }

    void GetNumberToSpawn()
    {
        if (difficulty < 1)
        {
            numToSpawn = 1;
            fireRate = 12;
        }
        else if (difficulty == 1)
        {
            int rand = Random.Range(1, 10);
            if (rand >= 8)
            {
                numToSpawn = 2;
            }
            else
            {
                numToSpawn = 1;
            }
            fireRate = 10;
        }
        else if (difficulty == 2)
        {
            int rand = Random.Range(1, 10);
            if (rand >= 6)
            {
                numToSpawn = 2;
            }
            else
            {
                numToSpawn = 1;
            }
        }
        else if (difficulty == 3)
        {
            int rand = Random.Range(1, 10);
            if (rand >= 6)
            {
                numToSpawn = 2;
            }
            else
            {
                numToSpawn = 1;
            }
            fireRate = 7;
        }
        else if (difficulty == 4)
        {
            int rand = Random.Range(1, 10);
            if (rand >= 9)
            {
                numToSpawn = 3;
            }
            else if (rand >= 7)
            {
                numToSpawn = 2;
            }
            else
            {
                numToSpawn = 1;
            }
            fireRate = 8;
        }
        else if (difficulty == 5)
        {
            int rand = Random.Range(1, 10);
            if (rand == 10)
            {
                numToSpawn = 4;
            }
            else if (rand >= 7)
            {
                numToSpawn = 3;
            }
            else if (rand >= 6)
            {
                numToSpawn = 2;
            }
            else
            {
                numToSpawn = 1;
            }

            fireRate = 9;
        }

    }

    private GameObject GetAsteroidToSpawn()
    {
        if (difficulty < 1)
        {
            return asteroid01;
        }
        else if (difficulty == 1)
        {
            return asteroid01;
        }
        else if (difficulty == 2)
        {
            int rand = Random.Range(1, 10);
            if (rand >= 7)
            {
                return asteroid02;
            }
            else
            {
                return asteroid01;
            }
        }
        else if (difficulty == 3)
        {
            int rand = Random.Range(1, 10);
            if (rand >= 5)
            {
                return asteroid02;
            }
            else
            {
                return asteroid01;
            }
        }
        else if (difficulty == 4)
        {
            int rand = Random.Range(1, 10);
            if (rand >= 8)
            {
                return asteroid03;
            }
            else if (rand >= 6)
            {
                return asteroid02;
            }
            else
            {
                return asteroid01;
            }
        }
        else if (difficulty == 5)
        {
            int rand = Random.Range(1, 10);
            if (rand >= 7)
            {
                return asteroid03;
            }
            else if (rand >= 4)
            {
                return asteroid02;
            }
            else
            {
                return asteroid01;
            }
        }
        else
        {
            return null;
        }

    }

}
