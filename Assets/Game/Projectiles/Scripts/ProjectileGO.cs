using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Game {
    public class ProjectileGO : MonoBehaviour {
        [SerializeField] protected float speed = 20f;      
        [SerializeField, Range(0f, 5f)] protected float destroyTime = 5f;
        [Space]
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected SpriteRenderer projectileSprite;
        [SerializeField] protected ParticleSystem particles;

        protected int damage;

        public void SetDamage(int value) {
            damage = value;
        }
        public virtual void Destroy() {
            rb.velocity = Vector2.zero;
            if (particles != null) {
                projectileSprite.gameObject.SetActive(false);
                particles.gameObject.SetActive(true);
                Destroy(gameObject, particles.main.duration);
            } else {
                Destroy(gameObject);
            }
        }
    }
}