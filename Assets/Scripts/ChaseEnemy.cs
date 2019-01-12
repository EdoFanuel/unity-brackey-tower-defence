using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    [Header("Attributes")]
    public float ProjectileSpeed;

    [Header("Animation")]
    public GameObject ImpactVFX;

    public Transform Target { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = Target.position - transform.position;
        if (direction.magnitude <= ProjectileSpeed * Time.deltaTime)
        {
            DamageTarget();
        }
        else
        {
            transform.Translate(direction.normalized * ProjectileSpeed * Time.deltaTime, Space.World);
        }
        
    }

    private void DamageTarget()
    {
        GameObject vfx = Instantiate(ImpactVFX, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(vfx, 2f);
        Destroy(Target.gameObject);
    }
}
