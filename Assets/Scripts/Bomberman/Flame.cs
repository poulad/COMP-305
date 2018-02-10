using UnityEngine;

namespace Bomberman
{
    public class Flame : MonoBehaviour
    {
        public void Start()
        {
            var rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = Vector2.right;
        }

        public void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject, 1);
        }
    }
}
