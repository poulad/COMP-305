using System.Linq;
using Bomberman.Movement;
using UnityEngine;

namespace Bomberman
{
    public class SceneManager : MonoBehaviour
    {
        public GameObject Block;

        public GameObject Wall;

        public GameObject PowerUp;

        public GameObject Bomberman;

        public GameObject Bomb;

        private int[][] _map;

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

            {
                var bomberman = Instantiate(Bomberman, new Vector2(1, 1), Quaternion.identity);
                bomberman.name = "Player 1";
                var behavior = bomberman.GetComponent<Bomberman>();
                behavior.MovementController = new KeyboardController(
                    KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space
                );
            }
            {
                var bomberman = Instantiate(Bomberman, new Vector2(1, 11), Quaternion.identity);
                bomberman.name = "Player 2";
                var behavior = bomberman.GetComponent<Bomberman>();
                behavior.MovementController = new JoystickController("XBox LeftJoystick X", "XBox LeftJoystick Y", "XBox A");
            }
            {
                var bomberman = Instantiate(Bomberman, new Vector2(13, 11), Quaternion.identity);
                bomberman.name = "Player 3";
                var behavior = bomberman.GetComponent<Bomberman>();
                behavior.MovementController = new KeyboardController(
                    KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Return
                );
            }
            {
                var bomberman = Instantiate(Bomberman, new Vector2(13, 1), Quaternion.identity);
                bomberman.name = "Player 4";
                var behavior = bomberman.GetComponent<Bomberman>();
                behavior.MovementController = new JoystickController("PS LeftJoystick X", "PS LeftJoystick Y", "PS X");
            }
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
                    {
                        map[i][j] = Constants.CellKinds.Block;
                    }
                    else
                    {
                        map[i][j] = Random.Range(0, 2) == 0
                            ? Constants.CellKinds.Empty
                            : Constants.CellKinds.Wall;

                        if (map[i][j] == Constants.CellKinds.Wall)
                        {
                            int random = Random.Range(0, 999997) % 10;
                            if (new[]
                                {
                                    Constants.CellKinds.WallExtraBomb, Constants.CellKinds.WallExpander,
                                    Constants.CellKinds.WallSkull
                                }
                                .Contains(random)
                            )
                            {
                                map[i][j] = random;
                            }
                        }
                    }
                }
            }

            map[1][1] = map[1][2] = map[2][1] = Constants.CellKinds.Empty;
            map[1][11] = map[2][11] = map[1][10] = Constants.CellKinds.Empty;
            map[13][11] = map[12][11] = map[13][10] = Constants.CellKinds.Empty;
            map[13][1] = map[2][11] = map[1][10] = Constants.CellKinds.Empty;
            return map;
        }

        private void Draw(int x, int y, int kind)
        {
            if (kind == Constants.CellKinds.Empty)
                return;

            if (kind > Constants.CellKinds.Wall)
            {
                var powerup = Instantiate(PowerUp, new Vector2(x, y), Quaternion.identity);
                var powerupBehavior = powerup.GetComponent<PowerUp>();
                powerupBehavior.Kind = kind;
            }

            var objKind = kind == Constants.CellKinds.Block ? Block : Wall;
            Instantiate(objKind, new Vector2(x, y), Quaternion.identity);
        }
    }
}
