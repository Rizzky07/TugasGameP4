using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
     public GameObject enemyPrefab; // isi dari Inspector
    public float spawnTime = 2f;   // jeda spawn
    public float range = 2f;       // area spawn

    public int maxSpawn = 5;       // batas maksimal spawn
    private int currentSpawn = 0;  // jumlah yang sudah spawn

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemyMethod), 1f, spawnTime);
    }

    void SpawnEnemyMethod()
    {
        // kalau sudah mencapai batas → stop spawn
        if (currentSpawn >= maxSpawn)
        {
            CancelInvoke(nameof(SpawnEnemyMethod));
            return;
        }

        Vector3 spawnPos = transform.position + new Vector3(
            Random.Range(-range, range),
            0f,
            Random.Range(-range, range)
        );

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        currentSpawn++; // tambah jumlah spawn
    }
}
