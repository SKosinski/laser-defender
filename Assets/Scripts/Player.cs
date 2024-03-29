﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.1f;
    [SerializeField] public int health = 200;
    [SerializeField] AudioClip deathClip;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] GameObject explosionVFXPrefab;

    [Header ("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.3f;
    [SerializeField] AudioClip laserClip;
    [SerializeField] [Range(0, 1)] float laserSoundVolume = 0.7f;


    Coroutine firingCoroutine;



    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(laserClip, Camera.main.transform.position, laserSoundVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            health = 0;
            FindObjectOfType<GameSession>().UpdateHealth();
            PlayerDeath(damageDealer);
            FindObjectOfType<SceneLoader>().LoadGameOver();
        }
        FindObjectOfType<GameSession>().UpdateHealth();
    }

    private void PlayerDeath(DamageDealer damageDealer)
    {
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, deathSoundVolume);
        damageDealer.Hit();
        Destroy(gameObject);
        GameObject explosionVFX = Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosionVFX, 1f);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0 + padding, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1 - padding, 0, 0)).x;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0 + padding, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1 - padding, 0)).y;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;        
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

}
