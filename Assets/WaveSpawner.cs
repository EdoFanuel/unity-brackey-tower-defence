using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform Enemy;
    public Transform SpawnPoint;
    public Text CountdownDisplay;
    public float WaveDelay;

    private float countdown = 2f;
    private int waveIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = WaveDelay;
        }
        countdown -= Time.deltaTime;
        CountdownDisplay.text = Mathf.Round(countdown).ToString();
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;
        Debug.Log("Incoming Wave #" + waveIndex);
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    private void SpawnEnemy()
    {
        Instantiate(Enemy, SpawnPoint.position, SpawnPoint.rotation);
    }
}
