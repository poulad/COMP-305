using UnityEngine;

namespace Bomberman.Movement
{
    public class KeyboardController : IMovementController
    {
        public float MoveSpeed { get; set; }

        private readonly KeyCode _up;

        private readonly KeyCode _down;

        private readonly KeyCode _left;

        private readonly KeyCode _right;

        private readonly KeyCode _bomb;

        public KeyboardController(KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode bomb)
        {
            _up = up;
            _down = down;
            _left = left;
            _right = right;
            _bomb = bomb;
            MoveSpeed = .4f;
        }

        public float GetMovementX()
        {
            float moveX = 0;
            if (Input.GetKey(_right)) moveX = MoveSpeed;
            if (Input.GetKey(_left)) moveX = -MoveSpeed;
            return moveX;
        }

        public float GetMovementY()
        {
            float moveY = 0;
            if (Input.GetKey(_up)) moveY = MoveSpeed;
            if (Input.GetKey(_down)) moveY = -MoveSpeed;
            return moveY;
        }

        public bool BombPressed()
        {
            return Input.GetKey(_bomb);
        }
    }
}
