using RPGGame.Config;

namespace RPGGame.Game
{
    public class Map : Sprite, ICameraObject
    {
        public Map(string name, string path, double x, double y, int width, int height) : base(name, path, x, y, width, height)
        {
            X = (width / 2) * (-1);
            Y = (height / 2) * (-1);
        }

        public bool Main { get; set; }
    }
}
