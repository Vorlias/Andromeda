using System;
using System.Collections.Generic;
using System.Linq;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Events
{
    /// <summary>
    /// Handles messaging between views in the same state
    /// </summary>
    public class ViewEvents
    {
        
        public class EventListener
        {
            public event EventListenerMessage OnEvent;

            public string Id
            {
                get;
            }

            public EventListener(string id)
            {
                Id = id;
            }

            internal void Invoke(params object[] args)
            {
                Console.WriteLine("Invoke " + args);
                OnEvent?.Invoke(args);
            }
        }

        public delegate void EventListenerEventMessage(string messageId, params object[] args);
        public delegate void EventListenerMessage(params object[] args);

        public event EventListenerEventMessage OnListenerEvent;

        Dictionary<string, EventListener> listeners = new Dictionary<string, EventListener>();

        public EventListener this[string messageId]
        {
            get
            {
                return GetListener(messageId);
            }
        }

        /// <summary>
        /// Registers the event queue
        /// </summary>
        /// <param name="messageId">The id of the messages</param>
        public EventListener AddEvent(string messageId)
        {
            if (!listeners.ContainsKey(messageId))
            {
                EventListener newListener = new EventListener(messageId);
                listeners.Add(messageId, newListener);
                return newListener;
            }
            else
            {
                return listeners[messageId];
            }
        }

        /// <summary>
        /// Removes the event
        /// </summary>
        /// <param name="messageId">The message id</param>
        public void RemoveEvent(string messageId)
        {
            if (listeners.ContainsKey(messageId))
                listeners.Remove(messageId);
        }

        public EventListener GetListener(string messageId)
        {
            if (!listeners.ContainsKey(messageId))
            {
                return null;
            }
            else
            {
                return listeners[messageId];
            }
        }

        public void Invoke(string messageId, params object[] args)
        {
            OnListenerEvent?.Invoke(messageId, args);
            listeners.Where(pair => pair.Key == messageId).Select(pair => pair.Value).ForEach(listener => listener.Invoke(args));
        }
    }
}
