using System;
using UnityEngine;

namespace Bomberman
{
    public class Movement : MonoBehaviour
    {
        public Sprite UpSprite;

        public Sprite DownSprite;

        public Sprite LeftSprite;

        public Sprite RightSprite;

        public float MaxSpeed = 30;

        private float SpeedMultiplier = 10;

        private Rigidbody2D _rigidbody2D;

        private SpriteRenderer _spriteRenderer;

        public void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void FixedUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            if (Math.Abs(moveX) > 0.0001)
            {
                _spriteRenderer.sprite = moveX > 0 ? RightSprite : LeftSprite;
                float move = NormalizeMovement(moveX * SpeedMultiplier, MaxSpeed);
                _rigidbody2D.velocity = new Vector2(move, 0);
            }

            if (Math.Abs(moveY) > 0.0001)
            {
                _spriteRenderer.sprite = moveY > 0 ? UpSprite : DownSprite;
                float move = NormalizeMovement(moveY * SpeedMultiplier, MaxSpeed);
                _rigidbody2D.velocity = new Vector2(0, move);
            }
        }

        private static float NormalizeMovement(float movement, float max)
        {
            if (Mathf.Abs(movement) >= max)
                return movement > 0 ? max : -max;
            else
                return movement;
        }
    }
}
