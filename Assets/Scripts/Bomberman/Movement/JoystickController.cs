using UnityEngine;

namespace Bomberman.Movement
{
    public class JoystickController : IMovementController
    {
        private readonly string _xAxis;

        private readonly string _yAxis;

        private readonly string _bombButton;

        public JoystickController(string xAxis, string yAxis, string bombButton)
        {
            _xAxis = xAxis;
            _yAxis = yAxis;
            _bombButton = bombButton;
        }

        public float GetMovementX()
        {
            return Input.GetAxis(_xAxis);
        }

        public float GetMovementY()
        {
            return Input.GetAxis(_yAxis);
        }

        public bool BombPressed()
        {
            return Input.GetButtonDown(_bombButton);
        }
    }
}
