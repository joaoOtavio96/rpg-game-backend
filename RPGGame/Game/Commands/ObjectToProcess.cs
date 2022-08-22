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
    }
}
