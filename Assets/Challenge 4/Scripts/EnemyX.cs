using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private SpawnManagerX spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed );

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            //Debug.Log("PLAYER GOAL");
            Destroy(gameObject);            
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            //Debug.Log("PLAYER GOAL");
            Destroy(gameObject);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            spawnManager.enemyGoal = true;            
        }

    }

}
