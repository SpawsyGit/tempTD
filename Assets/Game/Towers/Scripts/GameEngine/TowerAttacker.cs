using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class TowerAttacker : MonoBehaviour {
        [SerializeField] Transform shootPoint;
        [SerializeField] TowerArea area;
        [SerializeField] ProjectileGO projectile;

        private float attackRate;
        private int damage;

        private bool canShoot = true;
        private float cooldownTimer = 0f;

        private void Start() {
            cooldownTimer = attackRate;
        }
        private void Update() {
            cooldownTimer -= Time.deltaTime;
            if (!canShoot) return;
            if(cooldownTimer <= 0f) {
                if (area.HasTarget)
                    Shoot();
                /*else {
                    canShoot = false;
                    area.OnEnemyAppearedInRadius += WaitForTarget;
                }*/
                cooldownTimer = attackRate;
            }
        }

        private void WaitForTarget() {
            canShoot = true;
            area.OnEnemyAppearedInRadius -= WaitForTarget;
        }

        private void Shoot() {

        }


    }
}