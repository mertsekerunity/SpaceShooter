using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    List<Transform> waypoints;
    //int waypointIndex = 0;
    int startingWaypointIndex = 0; // alternative method
    int waypointIndex = 1; // alternative method
    float perlinNoiseScale = 1.0f; //alternative method

    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWayPoints();
        //transform.position = waypoints[waypointIndex].position;
        transform.position = waypoints[startingWaypointIndex].position; // alternative method
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
        //FollowPathAlternative();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

            if (transform.position == targetPosition) //not good approach, movetowards snaps transform.position to targetposition
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FollowPathAlternative() //alternative method
    {
        if (waypointIndex < waypoints.Count)
        {
            float moveSpeed = waveConfig.GetMoveSpeed();
            float noise = Mathf.PerlinNoise(Time.time, 0); //noise can also defined for y-axis for more variety
            float delta = (moveSpeed + noise * perlinNoiseScale) * Time.deltaTime;


            Vector3 targetPosition = waypoints[waypointIndex].position;
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * delta;

            if (Vector3.Distance(transform.position, targetPosition) <= delta / 2)  //Epsilon value leads to overshoot and oscillation, fine-tuned threshold needed
            {                                                                     // Vector2.MoveTowards inherently ensures the object will land exactly on the target position if the step size aligns perfectly.  
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
