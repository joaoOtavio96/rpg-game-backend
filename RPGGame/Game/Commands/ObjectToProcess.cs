namespace RPGGame.Game.Commands
{
    public class ObjectToProcess
    {
        public ObjectToProcess(IGameObject gameObject, Key key, Action completed)
        {
            GameObject = gameObject;
            Completed = completed;
        }
        
        public IGameObject GameObject { get; set; }
        public Action Completed { get; set; }
    }
}
