using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    //actions events
    public static Action PlayerDestroyed;
    public static Action<int> HealthUpdated;


    //configuration parameters
    [Header("Player Data")]
    [SerializeField] private int health = 10;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float xPadding = 0.5f;
    [SerializeField] private float yPadding = 0.5f;
    [SerializeField] [Range(0,1)] float weaponVolumeLevel = 0.1f;
    [SerializeField] [Range(0, 1)] float destructionVolumeLevel = 0.5f;
    [SerializeField] private AudioClip destructionClip;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDuration = 1f;

    [Header("Weapon Data")]
    [SerializeField] private GameObject weaponPrefab_1;
    private Coroutine firingContinousCoroutine;

    //used for movement
    private float moveXmin, moveXmax;
    private float moveYmin, moveYmax;
    //used for axis input
    private float deltaX = 0f;
    private float deltaY = 0f;
    private Vector2 newPosition = new Vector2(0, 0);

    // cached references
    private DamageDealerWeapon weapon_1;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        SetUpWeapon(ref weapon_1, weaponPrefab_1);
        HealthUpdated?.Invoke(health);
    }


    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerFire();
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        //set min/max x/y values for player movement by converting the corners of the viewport (plus padding) to world coordinates
        moveXmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        moveXmax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        moveYmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        moveYmax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    private void SetUpWeapon(ref DamageDealerWeapon weaponToSet, GameObject weaponPrefab)
    {
        weaponToSet = weaponPrefab.GetComponent<DamageDealerWeapon>();
        // TODO figure out a way to use relative position
        //weaponToSet.Position = transform.position;
    }

    //TODO update this to new input system
    private void PlayerMove()
    {
        deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        // TODO see if it's more efficient to replace this with colliders
        newPosition.x = Mathf.Clamp(transform.position.x + deltaX, moveXmin, moveXmax);
        newPosition.y = Mathf.Clamp(transform.position.y + deltaY, moveYmin, moveYmax);
        transform.position = newPosition;
    }


        public void PlayerFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingContinousCoroutine = StartCoroutine(FireContinuously(weapon_1));
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingContinousCoroutine);
        }
    }

    //*************Coroutines****************
    IEnumerator FireContinuously(DamageDealerWeapon weapon)  //TODO see if we can move continuous fire to the weapon
    {
        while (true)
        {
            weapon.Fire(transform.position, ProjectileDirection.Up, weaponVolumeLevel);
            yield return new WaitForSeconds(weapon.GetFireDelay());
            ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer)
            ProcessDamage(damageDealer);
    }

    // TODO possibly change this to a "damagtaker" class
    private void ProcessDamage(DamageDealer damageDealer)
    {
        health -= damageDealer.Damage;
        HealthUpdated?.Invoke(health);
        damageDealer.Hit();
        if (health <= 0)
        {
            ProcessDestruction();
        }
    }

    private void ProcessDestruction()
    {
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(destructionClip, Camera.main.transform.position, destructionVolumeLevel);
        Destroy(explosion, explosionDuration);
        PlayerDestroyed?.Invoke();
        Destroy(gameObject);
    }

}//end of class player
