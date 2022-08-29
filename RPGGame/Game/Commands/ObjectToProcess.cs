namespace RPGGame.Game.Commands
{
    public class ObjectToProcess
    {
        public ObjectToProcess(IGameObject gameObject, Key key, Action completed)
        {
            GameObject = gameObject;
            Key = key;
            Completed = completed;
        }
        
        public IGameObject GameObject { get; set; }
        public ICommandObject CommandObject => GameObject as ICommandObject;
        public ICollisionObject CollisionObject => GameObject as ICollisionObject;
        public ICollisionObject StaticCollisionObject => GameObject as ICollisionObject;
        public ICameraObject CameraObject => GameObject as ICameraObject;
        public Key Key { get; set; }
        public Action Completed { get; set; }

        public bool IsCommandObject()
        {
            return GameObject is ICommandObject;
        }

        public bool IsCollisionObject()
        {
            return GameObject is ICollisionObject;
        }

        public bool IsStaticCollisionObject()
        {
            return GameObject is ICollisionObject;
        }
    }
}
