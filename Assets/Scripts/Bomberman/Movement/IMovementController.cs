namespace Bomberman.Movement
{
    public interface IMovementController
    {
        float GetMovementX();

        float GetMovementY();

        bool BombPressed();
    }
}
