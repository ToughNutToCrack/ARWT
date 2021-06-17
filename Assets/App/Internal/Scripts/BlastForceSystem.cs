using System.Collections.Generic;
using UnityEngine;

public class BlastForceSystem : MonoBehaviour
{
    public List<Collider> GetCollided()
    {
        List<Collider> collided = new List<Collider>();

        var ws = GameController.WorldScale();
        var collisions = Physics.OverlapSphere(transform.position, 20f * ws.z);
        
        if(collisions != null)
        {
            collided = new List<Collider>(collisions);
        }

        return collided;
    }
}
