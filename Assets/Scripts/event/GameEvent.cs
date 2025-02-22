
using System;
using UnityEngine.Events;

public static class GameEvent {
    public static readonly UnityEvent<float> SpeedChangeEvent = new UnityEvent<float>();
    public static readonly UnityEvent<float> SoundChangeEvent = new UnityEvent<float>();
    
    public static readonly UnityEvent HitEvent = new UnityEvent();
    
    public static readonly UnityEvent PauseGameEvent = new UnityEvent();
    public static readonly UnityEvent ResumeGameEvent = new UnityEvent();
    
    public static readonly UnityEvent WeatherUpdatedEvent = new UnityEvent();
    
    // UI
    public static readonly UnityEvent<string> OnSelectEventInventoryItem = new UnityEvent<string>();
}