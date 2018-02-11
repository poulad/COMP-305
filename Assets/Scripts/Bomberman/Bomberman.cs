using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bomberman
{
    public class Bomberman : MonoBehaviour
    {
        public Sprite UpSprite;

        public Sprite DownSprite;

        public Sprite LeftSprite;

        public Sprite RightSprite;

        public float MaxSpeed = 30;

        public GameObject Bomb;

        public int MaxBombs = 10;

        public int MaxBombRange = 10;

        private float SpeedMultiplier = 10;

        private int _bombCount = 1;

        private int _bombRange = 1;

        private List<GameObject> _bombs = new List<GameObject>(10);

        private Rigidbody2D _rigidbody2D;

        private SpriteRenderer _spriteRenderer;

        public void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void FixedUpdate()
        {
            MoveBomberman();
            PlaceBomb();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Power Up"))
            {
                var powerupBehavior = other.GetComponent<PowerUp>();
                PowerUp(powerupBehavior.Kind);
                Destroy(other.gameObject);
            }
            else if (other.tag.Equals("Flame"))
                Die();
        }

        private void PlaceBomb()
        {
            if (!Input.GetKey(KeyCode.Space))
                return;

            _bombs = _bombs.Where(b => b != null).ToList();

            if (_bombs.Count == _bombCount)
                return;

            var bombPosition = new Vector3(
                (float)Math.Round(transform.position.x, MidpointRounding.ToEven),
                (float)Math.Round(transform.position.y, MidpointRounding.ToEven)
            );

            if (_bombs.Any(b => b != null && b.transform.position == bombPosition))
                // a bomb already exists in this cell
                return;

            var bomb = Instantiate(Bomb, bombPosition, Quaternion.identity);
            var bombBehavior = bomb.GetComponent<Bomb>();
            bombBehavior.FlameRange = _bombRange;
            _bombs.Add(bomb);
        }

        public void PowerUp(int kind)
        {
            switch (kind)
            {
                case Constants.CellKinds.WallExtraBomb:
                    if (_bombCount == MaxBombs)
                        return;
                    _bombCount++;
                    break;
                case Constants.CellKinds.WallExpander:
                    if (_bombRange == MaxBombRange)
                        return;
                    _bombRange++;
                    break;
                case Constants.CellKinds.WallSkull:
                    GetSick();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveBomberman()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            if (Math.Abs(moveX) > 0.0001)
            {
                _spriteRenderer.sprite = moveX > 0 ? RightSprite : LeftSprite;
                float move = NormalizeMoveSpeed(moveX * SpeedMultiplier, MaxSpeed);
                _rigidbody2D.velocity = new Vector2(move, 0);
            }

            if (Math.Abs(moveY) > 0.0001)
            {
                _spriteRenderer.sprite = moveY > 0 ? UpSprite : DownSprite;
                float move = NormalizeMoveSpeed(moveY * SpeedMultiplier, MaxSpeed);
                _rigidbody2D.velocity = new Vector2(0, move);
            }
        }

        private void GetSick()
        {
            _spriteRenderer.color = Color.magenta;
            // ToDo
        }

        private void Die()
        {
            SceneManager.GameOver();
            Destroy(gameObject);
        }

        private static float NormalizeMoveSpeed(float movement, float max)
        {
            if (Mathf.Abs(movement) >= max)
                return movement > 0 ? max : -max;
            else
                return movement;
        }
    }
}
