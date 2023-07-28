using System;
using System.Collections.Generic;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public static class EventManager
    {
        private static Dictionary<Type, List<EventListenerBase>> subscriberList;

        static EventManager()
        {
            subscriberList = new Dictionary<Type, List<EventListenerBase>>();
        }

        public static void AddListener<EventStruct>(EventListener<EventStruct> listener) where EventStruct : struct
        {
            Type eventType = typeof(EventStruct);
            if (!subscriberList.ContainsKey(eventType))
            {
                subscriberList[eventType] = new List<EventListenerBase>();
            }
            if( !SubscriptionExists( eventType, listener ) )
                subscriberList[eventType].Add( listener );
        }

        public static void RemoveListener<EventStruct>(EventListener<EventStruct> listener) where EventStruct : struct
        {
            Type eventType = typeof(EventStruct);
            if (!subscriberList.ContainsKey(eventType))
            {
                Debug.LogError($"Cannot remove listener type {eventType}. Type not found");
            }

            List<EventListenerBase> listenerList = subscriberList[eventType];

            for (int i = 0; i < listenerList.Count; i++)
            {
                if (listenerList[i] == listener)
                {
                    listenerList.Remove(listenerList[i]);

                    if (listenerList.Count == 0)
                    {
                        subscriberList.Remove(eventType);
                    }
                    return;
                }
            }
        }

        public static void TriggerEvent<EventStruct>(EventStruct newEvent) where EventStruct : struct
        {
            List<EventListenerBase> list;
            if (!subscriberList.TryGetValue(typeof(EventStruct), out list))
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                (list[i] as EventListener<EventStruct>).OnEvent(newEvent);
            }
        }
        
        private static bool SubscriptionExists( Type type, EventListenerBase receiver )
        {
            List<EventListenerBase> receivers;

            if( !subscriberList.TryGetValue( type, out receivers ) ) return false;

            bool exists = false;

            for (int i=0; i<receivers.Count; i++)
            {
                if( receivers[i] == receiver )
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }
    }

    /// <summary>
    /// Static class that allow any class to start/stop listening to events
    /// </summary>
    public static class EventRegister
    {
        public delegate void Delegate<T>(T eventType);

        public static void EventStartListening<EventStruct>(this EventListener<EventStruct> caller)
            where EventStruct : struct
        {
            EventManager.AddListener<EventStruct>(caller);
        }

        public static void EventStopListening<EventStruct>(this EventListener<EventStruct> caller)
            where EventStruct : struct
        {
            EventManager.RemoveListener(caller);
        }
    }
    
    /// <summary>
    /// Event listener base interface
    /// </summary>
    public interface EventListenerBase { };

    /// <summary>
    /// A public interface you'll need to implement for each type of event you want to listen to.
    /// </summary>
    public interface EventListener<T> : EventListenerBase
    {
        void OnEvent(T eventType);
    }

}
