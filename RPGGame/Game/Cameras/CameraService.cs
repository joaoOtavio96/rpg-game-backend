using RPGGame.Config;
using RPGGame.Game.Commands;

namespace RPGGame.Game.Cameras
{
    public class CameraService
    {
        public void SetPositions(List<ObjectToProcess> objectToProccess)
        {
            var mainObject = objectToProccess.Single(c => c.GameObject.Camera.Main);
            var secondaryObject = objectToProccess.Where(c => !c.GameObject.Camera.Main);

            foreach (var collisionBody in secondaryObject.SelectMany(s => s.GameObject.Collision.CollisionBodies))
            {
                collisionBody.Position.SetRelativePosition(mainObject.GameObject);
            }

            foreach (var secondary in secondaryObject)
            {
                secondary.GameObject.Position.SetRelativePosition(mainObject.GameObject);
            }

            mainObject.GameObject.Position.CenterRelativePosition();
        }
    }
}
