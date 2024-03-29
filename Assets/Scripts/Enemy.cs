﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<int> Destroyed;


    [Header("Configuration Parameters")]
    [SerializeField] int health = 1;
    [SerializeField] int scoreValue = 1;
    [SerializeField] float spawnDelay = 0.5f; //needs to vary based on size of ships

    [Header("Weapon Configuraton")]
    [SerializeField] GameObject weaponPrefab_1;
    //TODO put these in the weapon
    [SerializeField] float shotCounter = 0;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetwenShots = 3f;

    [Header("Audio Parameters")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] AudioClip destructionClip;
    [SerializeField] [Range(0, 1)] float weaponVolumeLevel = 0.1f;
    [SerializeField] [Range(0, 1)] float destructionVolumeLevel = 0.5f;


    //cached references
    private DamageDealerWeapon weapon_1;  //TODO make this an array of weapons and fire them all


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetwenShots);
        weapon_1 = weaponPrefab_1.GetComponent<DamageDealerWeapon>();;
        scoreValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO find a better way to do this
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
            ProcessDamage(damageDealer);
    }

    private void ProcessDamage(DamageDealer damageDealer)
    {
        health -= damageDealer.Damage;
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroyed?.Invoke(scoreValue);
            GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(destructionClip, Camera.main.transform.position, destructionVolumeLevel);
            Destroy(explosion, explosionDuration);
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            weapon_1.Fire(transform.position, ProjectileDirection.Down, weaponVolumeLevel);
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetwenShots);
        }
    }

    public float GetSpawnDelay() { return spawnDelay; }
}
