using UnityEngine;

public class Timer
{
    private float startTime;
    public float timeSeconds;
    public bool running;

    public float ElapsedTimeSec => running ? Time.time - startTime : 0f;
    public bool IsDone() => running && ElapsedTimeSec >= timeSeconds;

    public void Stop() => running = false;

    public void Restart()
    {
        startTime = Time.time;
        running = true;
    }
}
