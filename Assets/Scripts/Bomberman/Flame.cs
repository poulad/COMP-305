using UnityEngine;

// ReSharper disable UnusedMember.Local
namespace Bomberman
{
    public class Flame : MonoBehaviour
    {
        public float Speed = 4;

        [HideInInspector]
        public string Direction;

        [HideInInspector]
        public int Range;

        private Vector3 _startPosition;

        public void Start()
        {
            _startPosition = transform.position;
        }

        public void FixedUpdate()
        {
            if (string.IsNullOrEmpty(Direction))
                return;

            Invoke("Move" + Direction, 0);
        }

        private void MoveUp()
        {
            if (Range <= transform.position.y - _startPosition.y)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.Translate(0, Speed / 64, 0);
            }
        }

        private void MoveDown()
        {
            if (Range <= _startPosition.y - transform.position.y)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.Translate(0, -Speed / 64, 0);
            }
        }

        private void MoveRight()
        {
            if (Range <= transform.position.x - _startPosition.x)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.Translate(Speed / 64, 0, 0);
            }
        }

        private void MoveLeft()
        {
            if (Range <= _startPosition.x - transform.position.x)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.Translate(-Speed / 64, 0, 0);
            }
        }
    }
}
