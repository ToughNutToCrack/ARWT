using UnityEngine;

public class BlastForce : MonoBehaviour
{
    static readonly System.Random _rng = new System.Random();

    const int MIN_BLAST_DAMAGE = 5;
    const int MAX_BLAST_DAMAGE = 10;

    BlastForceSystem _system;

    void Awake()
    {
        _system = GetComponentInChildren<BlastForceSystem>();
    }

    public void Blast()
    {
        if(_system != null)
        {
            var collisions = _system.GetCollided();
            foreach(Collider collision in collisions)
            {
                // does the object take damage?
                if(collision.gameObject.TryGetComponent<DamageComponent>(out DamageComponent damageComponent))
                {
                    damageComponent.TakeDamage((float)_rng.Next(MIN_BLAST_DAMAGE, MAX_BLAST_DAMAGE));
                }

                // is the object a crate?
                if (collision.gameObject.TryGetComponent<CrateComponent>(out CrateComponent crateComponent))
                {
                    crateComponent.BlowUp();
                }
            }
        }
    }
}
