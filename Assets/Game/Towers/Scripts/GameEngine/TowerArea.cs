using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class TowerArea : MonoBehaviour {
        public event Action OnEnemyAppearedInRadius;
        [SerializeField] CircleCollider2D circleCollider;
        [SerializeField] LayerMask targetMask;

        private List<GameObject/*change to Enemy*/> enemiesInAttackRadius;

        public GameObject[] EnemiesInAttackRadius => enemiesInAttackRadius.ToArray();
        public bool HasTarget => enemiesInAttackRadius.Count > 0;
        public GameObject NearestEnemy => GetNearestEnemy();

        private void Awake() {
            enemiesInAttackRadius = new List<GameObject>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if(!collision.TryGetComponent<GameObject>(out var enemy)) return;
            if (enemiesInAttackRadius.Contains(enemy)) return;
            enemiesInAttackRadius.Add(enemy);
            if (enemiesInAttackRadius.Count == 1) OnEnemyAppearedInRadius?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.TryGetComponent<GameObject>(out var enemy)) return;
            if (!enemiesInAttackRadius.Contains(enemy)) return;
            enemiesInAttackRadius.Remove(enemy);
        }

        private GameObject GetNearestEnemy() {
            //if(enemiesInAttackRadius.Count == 0) 
            return  enemiesInAttackRadius
                    .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
                    .FirstOrDefault();
        }
    }
}