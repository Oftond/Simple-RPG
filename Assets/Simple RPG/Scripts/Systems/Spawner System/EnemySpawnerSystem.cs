using Player.Movement;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawnerSystem : MonoBehaviour
{
    [Inject] private PlayerMovementSystem _pms;

    [SerializeField] private List<EnemyPool> _enemies;
    [SerializeField] private int _maxCountEnemy = 0;
    [SerializeField, Range(0, 60)] private float _radiusSpawn;
    [SerializeField, Range(0, 60)] private float _spawnMinTime;
    [SerializeField, Range(0, 60)] private float _spawnMaxTime;

    private readonly HashSet<EnemyAI> _spawnedEnemies = new();

    public int MaxCountEnemy => _maxCountEnemy;

    private void Update()
    {
        if (_spawnedEnemies.Count < _maxCountEnemy)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator<WaitForSeconds> SpawnEnemy()
    {
        var randomOffset = Random.insideUnitCircle * _radiusSpawn;
        Vector2 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y);

        _spawnedEnemies.Add(_enemies[Random.Range(0, _enemies.Count)].GetObject(spawnPosition));
        var test = Random.Range(_spawnMinTime, _spawnMaxTime);
        yield return new WaitForSeconds(test);
        print(test);
    }
}