using System.Collections.Generic;
using UnityEngine.Events;

public static class ExpEventBus
{
    static readonly IDictionary<ExpEvents, UnityEvent> Events =
        new Dictionary<ExpEvents, UnityEvent>();
        
    public static void Subscribe(ExpEvents eventType, UnityAction listener)
    {
        if (Events.TryGetValue(eventType, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }

    public static void Unsubscribe(ExpEvents type, UnityAction listener)
    {
        if (Events.TryGetValue(type, out var thisEvent)) thisEvent.RemoveListener(listener);
    }

    public static void Publish(ExpEvents type)
    {
        if (Events.TryGetValue(type, out var thisEvent)) thisEvent.Invoke();
    }
}