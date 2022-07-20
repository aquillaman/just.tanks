using System.Collections;
using System.Collections.Generic;
using Pooling;
using UnityEngine;

namespace Units
{
    public class EnemySpawner : MonoBehaviour
    {
        public int MaxEnemies = 1;
        public EnemyTank Template;
        public Transform[] SpawnPoints;
        public Transform[] PatrolPoints;
        public List<Tank> Enemies = new List<Tank>();
    
        private Vector3[] _patrolPoints;
        private void Awake()
        {
            _patrolPoints = new Vector3[PatrolPoints.Length];
            for (var i = 0; i < PatrolPoints.Length; i++)
            {
                _patrolPoints[i] = PatrolPoints[i].position;
            }
        }

        public void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);

                if (Enemies.Count < MaxEnemies)
                {
                    SpawnOneEnemy();
                }
            }
        }

        private void SpawnOneEnemy()
        {
            if (!Template)
            {
                Debug.LogError($"EnemySpawner. Enemy template not set.");
                return;
            }
        
            var index = Enemies.Count % SpawnPoints.Length;
            var enemy = Pools.EnemyTank.Take();
            enemy.transform.position = SpawnPoints[index].position;
            enemy.Destroyed += EnemyOnDestroy;
            Enemies.Add(enemy);

            enemy.SetPatrolPath(_patrolPoints);
            enemy.Initialize();
        }

        private void EnemyOnDestroy(Tank tank)
        {
            tank.Destroyed -= EnemyOnDestroy;
            Enemies.Remove(tank);
        }
    }
}
