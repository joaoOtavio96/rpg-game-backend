using RPGGame.Game;
using RPGGame.Game.Collisions;

namespace RPGGame.Infrastructure
{
    // TODO verificar se pode alterar extensao para position
    public static class GameObjectExtensions
    {
        public static bool IsCloseTo(this IGameObject main, IGameObject secondary)
        {
            return Math.Abs(main.Position.RelativeX - secondary.Position.RelativeX) <= 16 && Math.Abs(main.Position.RelativeY - secondary.Position.RelativeY) <= 16;
        }

        public static bool IsCloseTo(this IGameObject main, CollisionBody secondary)
        {
            return Math.Abs(main.Position.RelativeX - secondary.Position.RelativeX) <= 16 && Math.Abs(main.Position.RelativeY - secondary.Position.RelativeY) <= 16;
        }
    }
}
