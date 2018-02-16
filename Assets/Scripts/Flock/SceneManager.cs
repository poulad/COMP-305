using System.Linq;
using UnityEngine;

namespace Flock
{
    public class SceneManager : MonoBehaviour
    {
        public GameObject Boid;

        public void Start()
        {
            foreach (var _ in Enumerable.Range(0, 30))
            {
                Instantiate(Boid);
            }


        }
    }
}