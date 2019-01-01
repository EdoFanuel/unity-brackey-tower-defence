using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 10f;
    public List<Transform> Points;

    private Transform nextPoint;
    private int pointIdx = 0;

    private void Start()
    {
        FindPath();
        nextPoint = Points[0];
    }

    private void Update()
    {
        Vector3 direction = nextPoint.position - transform.position;
        transform.Translate(direction.normalized * Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(nextPoint.position, transform.position) < 0.4)
        {
            if (pointIdx >= Points.Count - 1)
            {
                Destroy(gameObject);
                return;
            }
            nextPoint = Points[++pointIdx];
        }
    }

    private void FindPath()
    {
        if (Points.Count == 0)
        {
            GameObject waypoints = GameObject.Find("/Waypoints");
            for (int i = 0; i < waypoints.transform.childCount; i++)
            {
                Points.Add(waypoints.transform.GetChild(i));
            }
        }
    }
}
