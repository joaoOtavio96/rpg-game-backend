using RPGGame.Game;

namespace RPGGame.Infrastructure
{
    public static class GameObjectExtensions
    {
        public static bool IsCloseTo(this IGameObject main, IGameObject secondary)
        {
            return Math.Abs(main.RelativeX - secondary.RelativeX) <= 16 && Math.Abs(main.RelativeY - secondary.RelativeY) <= 16;
        }
    }
}
