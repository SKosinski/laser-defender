using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] WaveConfig waveConfig;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;

    [SerializeField] int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //AddPath();
        waypoints = waveConfig.getWaypoints();
        transform.position = waypoints[0].transform.position;

    }
    /*
    private void AddPath()
    {
        for (int i=0; i<path.transform.childCount; i++)
        {
            waypoints.Add(path.transform.GetChild(i));
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
