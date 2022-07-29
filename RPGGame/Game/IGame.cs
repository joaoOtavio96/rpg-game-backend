namespace RPGGame.Game
{
    public interface IGame
    {
        object GetState();
        void Init();
        void Update(double deltaTime);
    }
}
