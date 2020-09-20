using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float fireDelay = 0.15f; //10 rounds per second

    public Vector3 Position { get; set; }

    public int GetDamage() { return damage; }
    public float GetSpeed() { return speed; }
    public float GetFireDelay() { return fireDelay; }

    
    public void Fire(Vector3 weaponLocation, ProjectileDirection direction)
    {
        GameObject projectile = Instantiate(gameObject, weaponLocation, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed * (int) direction);
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
