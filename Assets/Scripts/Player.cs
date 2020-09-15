using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    //configuration parameters
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float xPadding = 0.5f;
    [SerializeField] private float yPadding = 0.5f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float laserBoltSpeed = 20f;
    private float moveXmin, moveXmax;
    private float moveYmin, moveYmax;

    private float deltaX = 0f;
    private float deltaY = 0f;
    private Vector2 newPosition = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
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


    private void PlayerMove()
    {
        deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        newPosition.x = Mathf.Clamp(transform.position.x + deltaX, moveXmin, moveXmax);
        newPosition.y = Mathf.Clamp(transform.position.y + deltaY, moveYmin, moveYmax);
        transform.position = newPosition;
    }


        public void PlayerFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject laserBolt = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laserBolt.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserBoltSpeed); //TODO move this functionality to the laser
        }
    }

}
