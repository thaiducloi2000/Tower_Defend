using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wavespawner : MonoBehaviour
{
    public Transform enemyPrefabs;
    public Transform spawnPoint;
    public float timeBetweenWave = 5f;
    private float countdown = 1f;
    private int waveIndex = 0;
    public Text waveCountdown;
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(Spawnwave());
            countdown = timeBetweenWave;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdown.text = string.Format("{0:00.00}",countdown);
    }

    IEnumerator Spawnwave()
    {
        waveIndex++;
        PlayerStats.Rounds++;
        for (int i = 0; i < waveIndex; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        //waveNumber++;
    }

    void spawnEnemy()
    {
        Instantiate(enemyPrefabs,spawnPoint.position,spawnPoint.rotation);
    }
}
