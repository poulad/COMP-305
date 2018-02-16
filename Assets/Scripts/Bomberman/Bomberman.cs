using System;
using System.Collections.Generic;
using System.Linq;
using Bomberman.Movement;
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

        public float SlowSpeed = 18;

        public GameObject Bomb;

        public int MaxBombs = 10;

        public int MaxBombRange = 10;

        [HideInInspector]
        public IMovementController MovementController { get; set; }

        private float _speedMultiplier = 10;

        private int _bombCount = 1;

        private int _bombRange = 1;

        private float _initialSpeedMultiplier;

        private List<GameObject> _bombs = new List<GameObject>(10);

        private Rigidbody2D _rigidbody2D;

        private SpriteRenderer _spriteRenderer;

        public void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _initialSpeedMultiplier = _speedMultiplier;
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
            if (!MovementController.BombPressed())
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
            float moveX = MovementController.GetMovementX();
            float moveY = MovementController.GetMovementY();

            if (Math.Abs(moveX) > 0)
            {
                _spriteRenderer.sprite = moveX > 0 ? RightSprite : LeftSprite;
                float move = NormalizeMoveSpeed(moveX * _speedMultiplier, MaxSpeed);
                _rigidbody2D.velocity = new Vector2(move, 0);
            }

            if (Math.Abs(moveY) > 0.0001)
            {
                _spriteRenderer.sprite = moveY > 0 ? UpSprite : DownSprite;
                float move = NormalizeMoveSpeed(moveY * _speedMultiplier, MaxSpeed);
                _rigidbody2D.velocity = new Vector2(0, move);
            }
        }

        #region Power Up - Skull

        private void GetSick()
        {
            _spriteRenderer.color = Color.magenta;
            int random = UnityEngine.Random.Range(0, 98787645) % 3;
            switch (random)
            {
                case 0:
                    Debug.LogWarning("Skull: Temporarily reducing speed (6 seconds)");
                    _speedMultiplier = 3;
                    Invoke("RestoreSpeed", 6);
                    break;
                case 1:
                    Debug.LogWarning("Skull: Making Bomberman invisible");
                    _spriteRenderer.sprite = UpSprite = RightSprite = DownSprite = LeftSprite = null;
                    break;
                case 2:
                    Debug.LogWarning("Skull: Allowing only one minimum-range bomb to be laid at a time");
                    _bombCount = 1;
                    _bombRange = 1;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void RestoreSpeed()
        {
            _speedMultiplier = _initialSpeedMultiplier;
        }

        #endregion

        private void Die()
        {
            Debug.LogErrorFormat("Bomberman died: '{0}'", name);
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
