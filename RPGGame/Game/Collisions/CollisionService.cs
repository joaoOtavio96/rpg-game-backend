using RPGGame.Game.Commands;
using RPGGame.Infrastructure;

namespace RPGGame.Game.Collisions
{
    public class CollisionService
    {
        public void CheckCollision(List<ObjectToProcess> objectsToProcess)
        {
            foreach (var movingObjects in objectsToProcess.Where(o => !o.GameObject.Collision.Static))
            {
                movingObjects.GameObject.Collision.UpdateColisionBodyPosition();
            }

            var combinations = objectsToProcess
                    .SelectMany(o => o.GameObject.Collision.CollisionBodies)
                    .GetPermutations(2);

            foreach (var combination in combinations)
            {
                var mainCombiationObject = objectsToProcess.FirstOrDefault(o => o.GameObject == combination.First().GameObject);
                var secondaryCombination = combination.Last();

                if (mainCombiationObject.Key == Key.Default)
                    continue;

                var mainCommand = mainCombiationObject.GameObject.Command.CommandMap.GetCommand(mainCombiationObject.Key);

                if (mainCommand.GameObject.IsCloseTo(secondaryCombination))
                {
                    if (Intersect(mainCommand.NextPosition(), secondaryCombination))
                    {
                        mainCombiationObject.GameObject.Collision.ObjectsWithCollision.Add(secondaryCombination);
                    }
                }
            }
        }

        private bool Intersect(IGameObject obj1, CollisionBody obj2)
        {
            return obj1.Bounds.MinX <= obj2.Bounds.MaxX && obj1.Bounds.MaxX >= obj2.Bounds.MinX &&
                    obj1.Bounds.MinY <= obj2.Bounds.MaxY && obj1.Bounds.MaxY >= obj2.Bounds.MinY;
        }
    }
}
