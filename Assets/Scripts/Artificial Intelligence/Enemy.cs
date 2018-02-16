using UnityEngine;

namespace ArtificialIntelligence
{
    public class Enemy : MonoBehaviour
    {
        public int Direction = 1;

        public int NextState;

        private int _state;

        private float _speed = 10;

        private bool _done;

        private Rigidbody _rigidbody;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Invoke("ChangeState", 2);
        }

        public void FixedUpdate()
        {
            switch (_state)
            {
                case Constants.PatrolState:
                    Patrol();
                    break;
                case Constants.AttackState:
                    Attack();
                    break;
                case Constants.SeekState:
                    Seek();
                    break;
                case Constants.EvadeState:
                    Evade();
                    break;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            Direction *= -1;
        }

        private void ChangeState()
        {
            _state = NextState;
        }

        private void Patrol()
        {
            _rigidbody.velocity = new Vector3(_speed * Direction, 0);
        }

        private void Attack()
        {
            if (_done) return;
            _done = true;

            GetComponentInChildren<ParticleSystem>().Play();
        }

        private void Seek()
        {
            if (_done) return;
            _done = true;

            _speed /= 3;
            _state = Constants.PatrolState;
        }

        private void Evade()
        {
            if (transform.localScale.x > 1.4)
                return;

            const float factor = 0.004f;
            transform.localScale *= (1 + factor);
        }

        private static class Constants
        {
            public const int PatrolState = 0;
            public const int AttackState = 2;
            public const int SeekState = 3;
            public const int EvadeState = 4;
        }
    }
}