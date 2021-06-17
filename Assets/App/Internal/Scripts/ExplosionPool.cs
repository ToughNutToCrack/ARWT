using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    static readonly System.Random _rng = new System.Random();

    public static ExplosionPool Instance;

    [SerializeField]
    GameObject[] _explosionPrefabs;

    class CachedExplosion
    {
        public Explosion Explosion { get; set; }
        public float Lifespan { get; set; }
    }

    List<CachedExplosion> _cache = new List<CachedExplosion>();

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
                var cachedExplosion = _cache[i];
                cachedExplosion.Lifespan -= Time.deltaTime;
                if (cachedExplosion.Lifespan < 0)
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
                var cachedExplosion = _cache[index];
                _cache.RemoveAt(index);
                cachedExplosion.Explosion.gameObject.SetActive(false);
                GameObject.Destroy(cachedExplosion.Explosion.gameObject);
            }
        }
    }

    public static bool TryGetRandomExplosion(out Explosion explosion)
    {
        explosion = null;

        if(
            Instance != null 
            && Instance.Pool != null
            && Instance._cache != null 
            && Instance._explosionPrefabs != null 
            && Instance._explosionPrefabs.Length > 0)
        {
            var pool = Instance.Pool;
            var cache = Instance._cache;
            var prefabs = Instance._explosionPrefabs;
            var i = _rng.Next(0, prefabs.Length);

            if( i < prefabs.Length )
            {
                var o = GameObject.Instantiate(prefabs[i], pool);
                
                if(o != null)
                {
                    o.SetActive(true);
                    if (o.TryGetComponent<Explosion>(out Explosion e))
                    {
                        explosion = e;
                        cache.Add(new CachedExplosion()
                        {
                            Explosion = e,
                            Lifespan = e.Duration()
                        });
                    }
                }
            }
        }
        return explosion != null;
    }
}
