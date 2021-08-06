using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    private Enemy targetEnermy;

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefabs;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]

    public bool userLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform PartToRotate;
    public float turnSpeed = 10f;


    public Transform firePoint;



    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearstEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearstEnemy = enemy;
            }
        }
        if(nearstEnemy!=null&& shortestDistance <= range)
        {
            target = nearstEnemy.transform;
            targetEnermy = nearstEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (userLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        //target lock on
        LockOnTarget();

        if (userLaser)
        {
            Laser();
        }
        else
        {

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnermy.takeDamge(damageOverTime*Time.deltaTime);
        targetEnermy.Slow(slowAmount);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1,target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * .5f;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);       
    }

    void Shoot()
    {
        GameObject bulletGO=(GameObject) Instantiate(bulletPrefabs, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
