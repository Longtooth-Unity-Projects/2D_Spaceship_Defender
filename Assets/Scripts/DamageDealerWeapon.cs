﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerWeapon : DamageDealer
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float fireDelay = 0.15f; // 15 rounds per second
    [SerializeField] AudioClip fireClip;

    public Vector3 Position { get; set; }

    public float GetSpeed() { return speed; }
    public float GetFireDelay() { return fireDelay; }

    public AudioClip GetFireClip() { return fireClip; }



    public void Fire(Vector3 weaponLocation, ProjectileDirection direction, float volumeLevel)
    {
        GameObject projectile = Instantiate(gameObject, weaponLocation, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed * (int)direction);
        AudioSource.PlayClipAtPoint(fireClip, Camera.main.transform.position, volumeLevel);
    }
}
