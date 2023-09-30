using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class Target : MonoBehaviour, IDamageable {
        [SerializeField] public int Points = 0;
        [SerializeField] AudioClip killNoise;

        public System.Action<int> onDestruction;

        public float health = 100f;

        public void Damage(float damageAmount) {
            health -= damageAmount;
            if (health <= 0) {
                onDestruction?.Invoke(Points);
                AudioSource.PlayClipAtPoint(killNoise, transform.position, 0.5f);
                Destroy(gameObject);
            }
        }
    }
}