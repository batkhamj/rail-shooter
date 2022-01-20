using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathParticles;
    [SerializeField] GameObject hitParticles;
    [SerializeField] int scorePerHit = 20;
    [SerializeField] int healthPoints = 300;

    Scoreboard scoreBoard;
    GameObject parentGameObject;
    void Start()
    {
        scoreBoard = FindObjectOfType<Scoreboard>();
        parentGameObject = GameObject.FindWithTag("RuntimeSpawn");
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        
        DecreaseHealth();
        if (healthPoints < 1)
        {
            ScoreIncrease();
            DestroyEnemy();
        }
    }

    void DecreaseHealth()
    {
        GameObject hitVFX = Instantiate(hitParticles, transform.position, Quaternion.identity);
        hitVFX.transform.parent = parentGameObject.transform;
        healthPoints -= 20;
    }

    void ScoreIncrease()
    {
        scoreBoard.ScoreIncrease(scorePerHit);
    }

    void DestroyEnemy()
    {
        GameObject fx = Instantiate(deathParticles, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
