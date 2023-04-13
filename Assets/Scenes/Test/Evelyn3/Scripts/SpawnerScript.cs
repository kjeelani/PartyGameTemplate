 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] boxes;

    public GameObject bomb;

    public float xBound, yBound;

    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());


    }

    IEnumerator SpawnRandomGameObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));

        int randomBox = Random.Range(0, boxes.Length);

        if (Random.value <= .6f)
            Instantiate(boxes[randomBox], new Vector2(Random.Range(-xBound, xBound), yBound), Quaternion.identity);
        else
            Instantiate(bomb, new Vector2(Random.Range(-xBound, xBound), yBound), Quaternion.identity);

        StartCoroutine(SpawnRandomGameObject());
    }
}
