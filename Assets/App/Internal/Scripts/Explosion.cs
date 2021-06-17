using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    BlastForce _blast;

    [SerializeField]
    ExplosionSystem _system;

    private void Awake()
    {
        if (_system == null)
            _system = GetComponentInChildren<ExplosionSystem>();
    }

    public void Explode()
    {
        if(_system != null)
        {
            _system.PlayExplosion();
        }
        if (_blast != null)
        {
            _blast.Blast();
            _blast.enabled = false;
            _blast.gameObject.SetActive(false);
        }
    }

    public float Duration()
    {
        return _system != null ? _system.Duration : 1f;
    }
}
