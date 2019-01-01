using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackEnemy : MonoBehaviour
{
    public float Range;
    public string EnemyTag;

    public Transform TurretJoint;//Where should the turret rotation based on
    public float RotationSpeed;//How fast should the turret rotate

    private Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindEnemy", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            return;
        }

        //This one works, but jerking movement when switching target
        //TurretJoint.LookAt(currentTarget);

        //Move 
        Vector3 direction = currentTarget.position - this.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        //Reduce jerking when switching target by smooting out the rotation over time
        Vector3 rotation = Quaternion.Lerp(TurretJoint.rotation, lookRotation, Time.deltaTime * RotationSpeed).eulerAngles;

        //Rotate the turret to face the target
        TurretJoint.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    // Additional Scene View overlay when gameObject is selected with gizmos enabled
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    private void FindEnemy()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag(EnemyTag));
        currentTarget = enemies
            .Where(enemy => Vector3.Distance(enemy.transform.position, this.transform.position) <= Range)
            .OrderBy(enemy => Vector3.Distance(enemy.transform.position, this.transform.position))
            .FirstOrDefault()
            ?.transform;
    }
}
