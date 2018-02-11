using UnityEngine;

namespace Bomberman
{
    public class Bomb : MonoBehaviour
    {
        public GameObject Flame;

        [HideInInspector]
        public int FlameRange;

        public void Start()
        {
            Invoke("Explode", 2.5f);
        }

        private void Explode()
        {
            var flame = Instantiate(Flame, this.transform.position, Quaternion.identity);
            var flameBehavior = flame.GetComponent<Flame>();
            flameBehavior.Direction = "Right";
            flameBehavior.Range = FlameRange;

            flame = Instantiate(Flame, this.transform.position, Quaternion.identity);
            flameBehavior = flame.GetComponent<Flame>();
            flameBehavior.Direction = "Up";
            flameBehavior.Range = FlameRange;

            flame = Instantiate(Flame, this.transform.position, Quaternion.identity);
            flameBehavior = flame.GetComponent<Flame>();
            flameBehavior.Direction = "Left";
            flameBehavior.Range = FlameRange;

            flame = Instantiate(Flame, this.transform.position, Quaternion.identity);
            flameBehavior = flame.GetComponent<Flame>();
            flameBehavior.Direction = "Down";
            flameBehavior.Range = FlameRange;

            Destroy(gameObject, 20);
        }
    }
}
