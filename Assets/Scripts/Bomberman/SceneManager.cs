using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bomberman
{
    public class SceneManager : MonoBehaviour
    {
        public GameObject Block;

        public GameObject Wall;

        public GameObject Bomberman;

        public GameObject Bomb;

        private int[][] _map;

        private GameObject _bomberman;

        private Bomberman _bombermanScript;

        private List<GameObject> _bombs = new List<GameObject>(3);

        public void Start()
        {
            _map = GenerateMap();

            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    int kind = _map[i][j];
                    Draw(i, j, kind);
                }
            }

            _bomberman = Instantiate(Bomberman, new Vector2(1, 1), Quaternion.identity);
            _bombermanScript = _bomberman.GetComponent<Bomberman>();
        }

        public void FixedUpdate()
        {
            PlaceBomb();
        }

        private void PlaceBomb()
        {
            if (!Input.GetKey(KeyCode.Space))
                return;

            _bombs = _bombs.Where(b => b != null).ToList();

            if (_bombs.Count == _bombermanScript.MaxBombs)
                return;

            var bombPosition = new Vector2(
                (float)Math.Round(_bomberman.transform.position.x, MidpointRounding.ToEven),
                (float)Math.Round(_bomberman.transform.position.y, MidpointRounding.ToEven)
            );

            var bomb = Instantiate(Bomb, bombPosition, Quaternion.identity);
            var bombBehavior = bomb.GetComponent<Bomb>();
            bombBehavior.FlameRange = 2; // ToDo
            _bombs.Add(bomb);
        }

        private static int[][] GenerateMap()
        {
            int[][] map = new int[15][];
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new int[13];
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (
                        i == 0 || j == 0 || i == map.Length - 1 || j == map[i].Length - 1 ||
                        (i % 2 == 0 && j % 2 == 0)
                    )
                        map[i][j] = 1;
                    else
                        map[i][j] = UnityEngine.Random.Range(0, 2) == 0
                            ? 0
                            : 2;
                }
            }

            map[1][1] = map[1][2] = map[2][1] = 0;
            return map;
        }

        private void Draw(int x, int y, int kind)
        {
            if (kind == 0)
                return;

            GameObject objKind;
            switch (kind)
            {
                case 0:
                    return;
                case 1:
                    objKind = Block;
                    break;
                case 2:
                    objKind = Wall;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Instantiate(objKind, new Vector2(x, y), Quaternion.identity);
        }
    }
}
