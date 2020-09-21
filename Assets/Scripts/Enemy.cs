using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //configuration parameters
    [SerializeField] int health = 1;
    [SerializeField] GameObject weaponPrefab_1;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] AudioClip destructionClip;
    [SerializeField] [Range(0, 1)] float weaponVolumeLevel = 0.1f;
    [SerializeField] [Range(0, 1)] float destructionVolumeLevel = 0.5f;

    //TODO put these in the weapon
    [SerializeField] float shotCounter = 0;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetwenShots = 3f;

    int projectileDirection = -1;

    //cached references
    private DamageDealer weapon_1;  //TODO make this an array of weapons and fire them all


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetwenShots);

        weapon_1 = weaponPrefab_1.GetComponent<DamageDealer>();;
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
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
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
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetwenShots);
        }
    }
}
