using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;

namespace GameFramework.Event
{
    public static class EventHelper
    {
        private static int s_NextEventId = 1000;
        private static readonly ConcurrentDictionary<Type, int> s_EventIdByType = new ConcurrentDictionary<Type, int>();

        public static int GetEventId(Type eventType)
        {
            return s_EventIdByType.GetOrAdd(eventType, _ => Interlocked.Increment(ref s_NextEventId));
        }
    }
}