using UnityEngine;

namespace Bomberman
{
    public class Block : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Flame"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
