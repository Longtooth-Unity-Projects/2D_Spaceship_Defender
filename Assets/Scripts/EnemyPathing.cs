using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //configuration variables
    public WaveConfiguration WaveConfig { get; set; }
    private List<Transform> waypointTransforms;
    int waypointIndex = 0;
    int lastWaypointIndex = 0;
    Transform targetTransform;

    


    //cached references


    // Start is called before the first frame update
    void Start()
    {
        waypointTransforms = WaveConfig.GetWaypoints();
        lastWaypointIndex = waypointTransforms.Count - 1;

        if (waypointTransforms[0] != null)
            transform.position = waypointTransforms[waypointIndex++].transform.position;


        if (waypointIndex <= lastWaypointIndex)
            targetTransform = waypointTransforms[waypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }//end of method update

    private void Move()
    {
        if (waypointIndex <= lastWaypointIndex)
        {
            //keep moving
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, WaveConfig.MoveSpeed * Time.deltaTime);

            if (transform.position == targetTransform.position)
                if (++waypointIndex <= lastWaypointIndex)
                    targetTransform = waypointTransforms[waypointIndex];
        }
        else
        {
            Destroy(gameObject);
        }
    }// end of method move
}
