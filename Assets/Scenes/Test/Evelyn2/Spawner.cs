using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float delay = 0.5f;


    public GameObject cube;

    private bool collide = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", delay, delay);
    }

    void Spawn()
    {
        Instantiate(cube, new Vector3(Random.Range(-7, 7), 10, 0), Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        collide = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (collide)
            CancelInvoke();
    }

    
}
