using UnityEngine;

public class BumperComponent : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public bool IsColliding { get; private set; }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();    
    }

    void Start()
    {
        if(Rigidbody != null)
        {
            Rigidbody.isKinematic = true;
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.ToLower() != "shard")
        {
            IsColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        IsColliding = false;
    }
}
