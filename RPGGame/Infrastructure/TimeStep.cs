namespace RPGGame.Infrastructure
{
    public class TimeStep
    {
        public double DeltaTime { get; set; }
        private double Time { get; set; }
        private double LastFrameTime { get; set; }
        public TimeStep()
        {
            LastFrameTime = 0D;
        }

        public void NextTime()
        {
            Time = DateTime.Now.Ticks;
            DeltaTime = Time - LastFrameTime;
            LastFrameTime = Time;
        }
    }
}
