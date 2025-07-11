using System;
using UnityEngine;

namespace App.Scripts.ObjectPoolPattern
{
    [Serializable]
    public class PoolData<T>
    {
        public int size;
        public Transform container;
        public T prefab;
    }
}