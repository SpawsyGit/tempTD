using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class ProjectileArrow : ProjectileGO {
        private bool isHit = false;
        void Start() {
            rb.velocity = speed * transform.right;
            Destroy(gameObject, destroyTime);
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if (isHit) return;
            var enemy = other.GetComponent<GameObject>(); //switch to enemy

            Destroy();
            isHit = true;
        }

    }
}