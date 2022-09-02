namespace RPGGame.Game.Cameras
{
    public class Camera
    {
        private readonly IGameObject _gameObject;
        public Camera(IGameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public bool Main { get; set; }
    }
}
