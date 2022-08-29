using RPGGame.Game.Commands;
using RPGGame.Infrastructure;

namespace RPGGame.Game
{
    public static class CollisionProcessor
    {
        //public static void Process(List<ObjectToProcess> objectsToProcess)
        //{
        //    var combinations = objectsToProcess
        //            .Where(o => o.IsCollisionObject() || o.IsStaticCollisionObject())
        //            .GetPermutations(2);


        //    foreach (var combination in combinations.Where(c => !c.First().IsStaticCollisionObject()))
        //    {
        //        var mainCombiationObject = objectsToProcess.FirstOrDefault(o => o.CollisionObject == combination.First().CollisionObject);

        //        if (mainCombiationObject.Key == Key.Default)
        //            continue;

        //        var secondaryCombiationObject = objectsToProcess.FirstOrDefault(o => o.CollisionObject == combination.Last().CollisionObject);

        //        var mainCommand = mainCombiationObject.CommandObject.CommandMap.GetCommand(mainCombiationObject.Key);
        //        var mainCurrrentState = mainCommand.CurrentState();

        //        if (mainCurrrentState.IsCloseTo(secondaryCombiationObject.GameObject))
        //            mainCombiationObject.CollisionObject.HasCollision = Intersect(mainCommand.NextPosition(), secondaryCombiationObject.GameObject);
        //    }
        //}

        public static void Process(List<ObjectToProcess> objectsToProcess)
        {

            objectsToProcess.ForEach(objectToProcess => objectToProcess.GameObject.UpdateColisionBody());

            var combinations = objectsToProcess
                    .Where(o => o.IsCollisionObject())
                    .SelectMany(o => o.GameObject.CollisionBodies)
                    .GetPermutations(2);

            foreach (var combination in combinations)
            {
                var mainCombiationObject = objectsToProcess.FirstOrDefault(o => o.CollisionObject == combination.First().GameObject);

                if (mainCombiationObject.Key == Key.Default)
                    continue;

                var secondaryCombiationObject = objectsToProcess.FirstOrDefault(o => o.CollisionObject == combination.Last().GameObject);

                var mainCommand = mainCombiationObject.CommandObject.CommandMap.GetCommand(mainCombiationObject.Key);
                var mainCurrrentState = mainCommand.CurrentState();

                var closeCollisionBody = secondaryCombiationObject.GameObject.CollisionBodies.FirstOrDefault(o => mainCurrrentState.IsCloseTo(o));
                if (closeCollisionBody != null)
                {
                    if(Intersect(mainCommand.NextPosition(), closeCollisionBody))
                    {
                        mainCombiationObject.CollisionObject.ObjectsWithCollision.Add(closeCollisionBody);
                    }
                }                   
            }
        }

        private static bool Intersect(IGameObject obj1, IGameObject obj2)
        {
            return (obj1.MinX <= obj2.MaxX && obj1.MaxX >= obj2.MinX) &&
                    (obj1.MinY <= obj2.MaxY && obj1.MaxY >= obj2.MinY);
        }

        private static bool Intersect(IGameObject obj1, CollisionBody obj2)
        {
            return (obj1.MinX <= obj2.MaxX && obj1.MaxX >= obj2.MinX) &&
                    (obj1.MinY <= obj2.MaxY && obj1.MaxY >= obj2.MinY);
        }
    }
}
