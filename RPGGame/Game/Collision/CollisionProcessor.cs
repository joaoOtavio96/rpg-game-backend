using RPGGame.Game.Commands;
using RPGGame.Infrastructure;

namespace RPGGame.Game
{
    public static class CollisionProcessor
    {
        public static void Process(List<ObjectToProcess> objectsToProcess)
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
                    if(Intersect(mainCommand.NextPosition(), secondaryCombination))
                    {
                        mainCombiationObject.GameObject.Collision.ObjectsWithCollision.Add(secondaryCombination);
                    }
                }                   
            }
        }

        private static bool Intersect(IGameObject obj1, CollisionBody obj2)
        {
            return (obj1.MinX <= obj2.MaxX && obj1.MaxX >= obj2.MinX) &&
                    (obj1.MinY <= obj2.MaxY && obj1.MaxY >= obj2.MinY);
        }
    }
}
