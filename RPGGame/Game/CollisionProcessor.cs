using RPGGame.Game.Commands;
using RPGGame.Infrastructure;

namespace RPGGame.Game
{
    public static class CollisionProcessor
    {
        public static void Process(List<ObjectToProcess> objectsToProcess)
        {
            var combinations = objectsToProcess
                    .Where(o => o.IsCollisionObject())
                    .GetPermutations(2);

            foreach (var combination in combinations)
            {
                var firstCombiationObject = objectsToProcess.FirstOrDefault(o => o.CollisionObject == combination.First().CollisionObject);

                if (firstCombiationObject.Key == Key.Default)
                    continue;

                var secondCombiationObject = objectsToProcess.FirstOrDefault(o => o.CollisionObject == combination.Last().CollisionObject);

                var ob1 = firstCombiationObject.CommandObject.CommandMap.GetCommand(firstCombiationObject.Key);
                var ob2 = secondCombiationObject.CommandObject.CommandMap.GetCommand(secondCombiationObject.Key);

                var currentOb1 = ob1.CurrentState();
                var currentOb2 = ob2.CurrentState();

                if (Math.Abs(currentOb1.RelativeX - currentOb2.RelativeX) <= 16 && Math.Abs(currentOb1.RelativeY - currentOb2.RelativeY) <= 16)
                    firstCombiationObject.CollisionObject.HasCollision = Intersect(ob1.NextPosition(), ob2.NextPosition());
            }
        }

        private static bool Intersect(IGameObject obj1, IGameObject obj2)
        {
            return (obj1.MinX <= obj2.MaxX && obj1.MaxX >= obj2.MinX) &&
                    (obj1.MinY <= obj2.MaxY && obj1.MaxY >= obj2.MinY);
        }
    }
}
