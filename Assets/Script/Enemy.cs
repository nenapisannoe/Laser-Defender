using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyLaserPrefab; 
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float health = 100f;
    [SerializeField] private int score = 20;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private float explosionDuration = 1f;
    [Range(0,1)][SerializeField]float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] private AudioClip deathSound;
    [Range(0,1)][SerializeField] float shootSoundVolume = 0.25f;
    
    private float shotCounter;
    

    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        /*GameObject laser1 = Instantiate(
            enemyLaserPrefab,
            transform.position - new Vector3(0.15f,0f,0f), 
            transform.rotation * Quaternion.Euler (0f, 0f, -40f)) as GameObject;
        laser1.GetComponent<Rigidbody2D>().velocity = new Vector2(-8f, -projectileSpeed); */
        GameObject laser2 = Instantiate(
            enemyLaserPrefab,
            transform.position, 
            transform.rotation * Quaternion.identity) as GameObject;
        laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        /*GameObject laser3 = Instantiate(
            enemyLaserPrefab,
            transform.position + new Vector3(0.15f,0f,0f), 
            transform.rotation * Quaternion.Euler (0f, 0f, 40f)) as GameObject;
        laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(8f, -projectileSpeed);*/
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionParticle, transform.position, transform.rotation);
        Destroy(explosion,explosionDuration); 
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<GameSession>().AddToScore(score);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }
    
    
}
