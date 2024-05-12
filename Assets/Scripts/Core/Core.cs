using Lance.Weapons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lance.CoreSystem
{
    public class Core : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        public GameObject Root => root;

        private Movement movement;
        public Movement Movement => movement;

        private CollisionSenses collisionSenses;
        public CollisionSenses CollisionSenses => collisionSenses;

        private readonly List<CoreComponent> coreComponents = new List<CoreComponent>();

        private void Awake()
        {
            // Initialize references
            movement = GetComponentInChildren<Movement>();
            collisionSenses = GetComponentInChildren<CollisionSenses>();

            // Assign root if not set
            if (!root)
                root = transform.parent.gameObject;
        }

        public void LogicUpdate()
        {
            foreach (CoreComponent component in coreComponents)
            {
                component.LogicUpdate();
            }
        }

        public void AddComponent(CoreComponent component)
        {
            if (!coreComponents.Contains(component))
            {
                coreComponents.Add(component);
            }
        }

        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var comp = coreComponents.OfType<T>().FirstOrDefault();

            if (comp)
                return comp;

            comp = GetComponentInChildren<T>();

            if (comp)
                return comp;

            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            return null;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }
    }
}
