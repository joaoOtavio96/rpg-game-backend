using RPGGame.Config;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace RPGGame.Game
{
    public class Animation
    {
        public Animation()
        {
            ResetFrame();
            Animations = new Dictionary<string, List<Point>>();
        }
        public Dictionary<string, List<Point>> Animations { get; set; }
        public int FrameLimit { get; private set; }
        public int CurrentFrame { get; private set; }

        public byte[] GetAnimationImage(byte[] image, string name, int width, int height)
        {
            using (var memoryStream = new MemoryStream())
            using (var loadedImage = Image.Load(image))
            {                          
                var animationsFrame = Animations[name];

                if (animationsFrame.Count <= CurrentFrame)
                    CurrentFrame = AnimationConfig.CurrentFrame;

                var currentAnimationFrame = animationsFrame.ElementAtOrDefault(CurrentFrame);
                var pointToCrop = new Point((currentAnimationFrame.X * height), (currentAnimationFrame.Y * width));

                loadedImage.Clone(ctx => 
                    ctx.Crop(new Rectangle(pointToCrop, new Size(width, height))))
                       .Save(memoryStream, new PngEncoder()
                );             

                FrameLimit -= 1;

                if (FrameLimit <= 0)
                {
                    FrameLimit = AnimationConfig.FrameLimit;
                    CurrentFrame += 1;
                }

                return memoryStream.ToArray();
            }
        }

        public Animation AddAnimation(string name, List<Point> positions)
        {
            Animations.Add(name, positions);

            return this;
        }

        public void ResetFrame()
        {
            FrameLimit = AnimationConfig.FrameLimit;
            CurrentFrame = AnimationConfig.CurrentFrame;
        }

    }
}
