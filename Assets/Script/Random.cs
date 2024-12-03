using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRock : MonoBehaviour
{
    public GameObject[] rockPrefabs;
    private float minRandomX = -2.54f;
    private float maxRandomX = 2.71f;
    private float randomY = 5.37f;

    private float minGravity = 0.3f;
    private float maxGravity = 1.3f;

    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = StartCoroutine(swapmManager());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator swapmManager()
    {
        while (true)
        {
            int numberOfRocks = Random.Range(1, 4);
            for (int i = 0; i < numberOfRocks; i++)
            {
                spawnRockRandomPosition();
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(2f);
        }
    }
    private void spawnRockRandomPosition()
    {
        float spawnX = Random.Range(minRandomX, maxRandomX);
        Vector2 spawnPosition = new Vector2(spawnX, randomY);

        int randomIndex = Random.Range(0, rockPrefabs.Length);
        GameObject selectedRock = rockPrefabs[randomIndex];

        GameObject spawnedRock = Instantiate(selectedRock, spawnPosition, Quaternion.identity);
        Rigidbody2D rockRb = spawnedRock.GetComponent<Rigidbody2D>();

        rockRb.gravityScale = Random.Range(minGravity, maxGravity);
    }

}
