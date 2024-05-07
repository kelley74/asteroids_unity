using System;
using System.Collections.Generic;

namespace Game.BindingContainer
{
    public class DiContainer
    {
        private static DiContainer _container;
        
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private Dictionary<string, object> _bindingDictionary = new Dictionary<string, object>();

        public static void Create()
        {
            _container = new DiContainer();
        }

        public static void Bind<T>(T binding)
        {
            var key = typeof(T).ToString();
            _container._bindingDictionary.Add(key, binding);
        }

        public static T Resolve<T>()
        {
            var key = typeof(T).ToString();
            if (_container._bindingDictionary.TryGetValue(key, out var value))
            {
                return (T)value;
            }

            throw new Exception($"Binding {key} not found");
        }
    }
}