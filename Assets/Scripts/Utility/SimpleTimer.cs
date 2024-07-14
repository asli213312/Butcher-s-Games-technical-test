using System;
using System.Timers;

public class SimpleTimer
{
    public bool IsStarted { get; private set; }
    public bool IsCompleted { get; private set;}
    private Timer _timer;
    private Action _onComplete;
    private float _duration;

    public SimpleTimer(float durationInSeconds, Action onComplete = null)
    {
        _duration = durationInSeconds;

        if (onComplete != null) _onComplete = onComplete;

        _timer = new Timer(durationInSeconds * 1000);
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = false;
    }

    public void Start()
    {
        _timer.Start();
        IsStarted = true;
        IsCompleted = true;
    }

    public void Stop()
    {
        _timer.Stop();
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _onComplete?.Invoke();
        IsCompleted = true;
    }
}