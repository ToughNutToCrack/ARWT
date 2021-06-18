using System.Collections.Generic;
using UnityEngine;

public class CrateComponent : MonoBehaviour
{
    bool _destroyed = false;

    [SerializeField]
    Collider _crateCollider;

    [SerializeField]
    Rigidbody _crateRigidbody;

    [SerializeField]
    MeshRenderer _meshRenderer;

    [SerializeField]
    Transform _destroyedOrigin;

    public void BlowUp()
    {
        if(!_destroyed)
        {
            if (_meshRenderer != null)
            {
                _meshRenderer.enabled = false;
            }

            _crateCollider.enabled = false;
            _crateRigidbody.isKinematic = true;

            if (_destroyedOrigin != null)
            {
                _destroyedOrigin.gameObject.SetActive(true);

                int children = _destroyedOrigin.childCount;
                var shards = new GameObject[children];

                for (int i = 0; i < children; i += 1)
                {
                    Transform child = _destroyedOrigin.GetChild(i);
                    child.gameObject.SetActive(true);
                    child.gameObject.tag = "Shard";
                    child.gameObject.AddComponent<ConstantForce>();
                    var cf = child.GetComponent<ConstantForce>();
                    cf.force = new Vector3(0, 0, 25f);

                    shards[i] = child.gameObject;
                }

                System.Array.ForEach<GameObject>(shards, (s) =>
                {
                    CrateShardPool.Instance?.AddToPool(s);
                });

                shards = null;
            }

            _destroyed = true;
        }
    }
}
