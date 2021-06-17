using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControlComponent : MonoBehaviour
{
    public Transform CollisionCapsuleFront;
    public Transform CollisionCapsuleBack;

    bool _initialized = false;

    void Start()
    {
        if(CollisionCapsuleFront != null && CollisionCapsuleBack != null)
        {
            _initialized = true;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if(_initialized)
        {
            RaycastHit hit;
            Vector3 p1 = CollisionCapsuleFront.position;
            Vector3 p2 = CollisionCapsuleBack.position;

            if(Physics.CapsuleCast(p1, p2, 100f, transform.forward, out hit, 100f))
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
