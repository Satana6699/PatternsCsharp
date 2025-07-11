using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.ObjectPoolPattern
{
    public class ObjectPool<T> where T : Component
    {
        public int Count => _pool.Count;
        public IEnumerable<T> ListOfElements => _pool;

        private int _size;
        private readonly Transform _container;
        private readonly T _prefab;

        private readonly Queue<T> _pool;
        public Transform Container => _container;

        public ObjectPool(PoolData<T> poolData)
        {
            _size = poolData.size;
            _container = poolData.container;
            _prefab = poolData.prefab;
            _pool = new Queue<T>(_size);
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < _size; i++)
            {
                CreateElementInPool(_prefab);
            }
        }
        
        private void CreateElementInPool(T element)
        {
            var obj = Object.Instantiate(element, _container);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        public T GetElement()
        {
            if (_pool.Count == 0)
            {
                CreateElementInPool(_prefab);
                _size++;
            }

            return _pool.Dequeue();
        }

        public void ReturnElementToPool(T element)
        {
            _pool.Enqueue(element);
        }

        public void DisposeImmediatePool(bool immediate = true)
        {
            foreach (var element in _pool)
            {
                if(element == null) continue;
                
                if (immediate)
                {
                    Object.DestroyImmediate(element.gameObject);
                }
                else
                {
                    Object.Destroy(element.gameObject);
                }
            }
            
            _pool.Clear();
        }
    }
}
