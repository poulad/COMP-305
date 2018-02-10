using UnityEngine;

namespace Bomberman
{
    public class Bomb : MonoBehaviour
    {
        public void Start()
        {
            Invoke("Explode", 3);
        }

        private void Explode()
        {
            Destroy(gameObject);
        }
    }
}
