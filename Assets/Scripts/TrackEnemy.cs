using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackEnemy : MonoBehaviour
{
    [Header("Tower Attributes")]
    public float Range;

    [Header("Shooting Behaviour")]
    public string EnemyTag;//Game objects to be targetted
    public Transform CurrentTarget { get; set; }

    [Header("Animation")]
    public Transform TurretJoint;//Where should the turret rotation based on
    public float RotationSpeed;//How fast should the turret rotate

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindEnemy", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTarget == null)
        {
            return;
        }

        //This one works, but jerking movement when switching target
        //TurretJoint.LookAt(currentTarget);

        //Move 
        Vector3 direction = CurrentTarget.position - this.transform.position;
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
        CurrentTarget = Physics.OverlapSphere(transform.position, Range)
            .Where(collider => collider.gameObject.tag == "Enemy")
            .OrderBy(enemy => Vector3.Distance(enemy.transform.position, this.transform.position))
            .FirstOrDefault()
            ?.transform;
    }
}
