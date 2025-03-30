
using System;
using mode;
using UnityEngine.Events;

public static class GameEvent {
    public static readonly UnityEvent<int, int> ManagersProgressEvent = new UnityEvent<int, int>();
    public static readonly UnityEvent ManagersStartedEvent = new UnityEvent();
    
    public static readonly UnityEvent<float> SpeedChangeEvent = new UnityEvent<float>();
    public static readonly UnityEvent<float> SoundChangeEvent = new UnityEvent<float>();
    
    public static readonly UnityEvent HitEvent = new UnityEvent();
    
    public static readonly UnityEvent PauseGameEvent = new UnityEvent();
    public static readonly UnityEvent ResumeGameEvent = new UnityEvent();
    public static readonly UnityEvent LevelCompleteEvent = new UnityEvent();
    public static readonly UnityEvent LevelFailedEvent = new UnityEvent();
    public static readonly UnityEvent GameCompleteEvent = new UnityEvent();

    public static readonly UnityEvent<int, int> HealthChangedEvent = new UnityEvent<int, int>();

    public static readonly UnityEvent WeatherUpdatedEvent = new UnityEvent();
    
    // UI
    public static readonly UnityEvent<string> OnSelectEventInventoryItem = new UnityEvent<string>();
    
    // Game modes
    public static readonly UnityEvent<GameMode> GameModeChangeEvent = new UnityEvent<GameMode>();
}