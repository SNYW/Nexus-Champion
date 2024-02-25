using System;
using System.Collections.Generic;

namespace SystemEvents
{
    public static class SystemEventManager
    {
        public enum SystemEventType
        {
            SpellSelected,
            SpellDeselected,
            SpellCast,
            EnemyDamaged,
            PlayerDamaged,
            GameEnd,
            GameStart
        }

        private static Dictionary<SystemEventType, Action<object>> _eventListeners;

        public static void Init()
        {
            _eventListeners = new Dictionary<SystemEventType, Action<object>>();
            foreach (SystemEventType type in Enum.GetValues(typeof(SystemEventType)))
            {
                _eventListeners[type] = delegate {  };
            }
        }

        public static void Subscribe(SystemEventType type, Action<object> action)
        {
            _eventListeners ??= new Dictionary<SystemEventType, Action<object>>();
            if (!_eventListeners.TryGetValue(type, out var e)) return;
        
            if (e != null)
            {
                _eventListeners[type] += action;
            }
            else
            {
                e = action;
                _eventListeners[type] = e;
            }
        }
    
        public static void Unsubscribe(SystemEventType type, Action<object> action)
        {
            if (!_eventListeners.TryGetValue(type, out var e)) return;
        
            if (e != null)
            {
                _eventListeners[type] -= action;
            }
        }

        public static void RaiseEvent(SystemEventType type, object payload)
        {
            if (_eventListeners != null && _eventListeners.TryGetValue(type, out var e))
            {
                e?.Invoke(payload);
            }
        }
    }
}
