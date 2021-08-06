using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnermyMoment : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        
        target = Waypoint.points[0];
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoint.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoint.points[wavepointIndex];
    }

    void EndPath()
    {
        if (PlayerStats.lives < 0)
        {
            return;
        }
        PlayerStats.lives--;
        Destroy(gameObject);

    }
}
