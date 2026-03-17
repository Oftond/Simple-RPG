using Player.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class EnemySpawnerSystem : MonoBehaviour
{
    [Inject] private PlayerMovementSystem _pms;

    [SerializeField] private List<EnemyPool> _enemies;
    [SerializeField] private int _maxCountEnemy = 0;
    [SerializeField, Range(0, 60)] private float _radiusSpawn;
    [SerializeField, Range(0, 60)] private float _spawnMinTime;
    [SerializeField, Range(0, 60)] private float _spawnMaxTime;

    private readonly HashSet<EnemyAI> _spawnedEnemies = new();
    private bool _isSpawning;

    public int MaxCountEnemy => _maxCountEnemy;

    private void Update()
    {
        if (_spawnedEnemies.Count < _maxCountEnemy && !_isSpawning)
            StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        _isSpawning = true;
        var randomOffset = Random.insideUnitCircle * _radiusSpawn;
        Vector2 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y);

        if (_pms != null)
        {
            float distanceToPlayer = Vector2.Distance(spawnPosition, _pms.transform.position);
            if (distanceToPlayer < 2)
                spawnPosition = _pms.transform.position + (Vector3)Random.insideUnitCircle * 3;
        }

        _spawnedEnemies.Add(_enemies[Random.Range(0, _enemies.Count)].GetObject(spawnPosition));
        var test = Random.Range(_spawnMinTime, _spawnMaxTime);
        yield return new WaitForSeconds(test);
        _isSpawning = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.2f, 0.2f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, _radiusSpawn);
    }
}