using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    [Header("Tower Attributes")]
    public float FireRate;
    private float timeToFire;

    [Header("Animation")]
    public GameObject Bullet;
    public Transform FirePoint;//Where bullet should be generated
    private Transform bulletTarget;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        bulletTarget = GetComponent<TrackEnemy>().CurrentTarget;
        if (bulletTarget == null)
        {
            return; //don't even bother shooting if there's no enemy
        }

        if (timeToFire < 0f)
        {
            GenerateBullet();
            timeToFire = 1 / FireRate;
        }
        timeToFire -= Time.deltaTime;
    }

    private void GenerateBullet()
    {
        Debug.Log("Firing " + Bullet);
        GameObject newBullet = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
        newBullet.GetComponent<ChaseEnemy>().Target = bulletTarget;
    }
}
