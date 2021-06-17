using System.Collections.Generic;
using UnityEngine;

public class CrateShardPool : MonoBehaviour
{
    public static CrateShardPool Instance;

    class CachedShard
    {
        public const float LIFESPAN = 3f;
        public GameObject Object { get; set; }
        public float Lifespan { get; set; } 
    }

    List<CachedShard> _cache = new List<CachedShard>();

    [SerializeField]
    public Transform Pool;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (_cache != null && _cache.Count > 0)
        {
            List<int> expired = new List<int>(_cache.Count);
            for (int i = 0; i < _cache.Count; i += 1)
            {
                var cachedShard = _cache[i];
                cachedShard.Lifespan -= Time.deltaTime;

                if (cachedShard.Lifespan < 0)
                {
                    expired.Add(i);
                }

                if(cachedShard.Object.transform.position.y < -1f)
                {
                    expired.Add(i);
                }
            }
            var array = expired.ToArray();
            System.Array.Sort(array);
            System.Array.Reverse(array);
            for (int j = 0; j < array.Length; j += 1)
            {
                var index = array[j];
                var cachedShard = _cache[index];
                _cache.RemoveAt(index);
                cachedShard.Object.SetActive(false);
                GameObject.Destroy(cachedShard.Object);
            }
        }
    }

    public void AddToPool(GameObject shard)
    {
        if (shard != null)
        {
            if(Pool != null)
            {
                shard.transform.SetParent(Pool);
            }
            _cache.Add(new CachedShard()
            {
                Object = shard,
                Lifespan = CachedShard.LIFESPAN
            });
        }
    }
}
