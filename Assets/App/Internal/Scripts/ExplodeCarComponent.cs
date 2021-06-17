using UnityEngine;

public class ExplodeCarComponent : MonoBehaviour
{
    [SerializeField]
    public MeshRenderer _meshRenderer;

    [SerializeField]
    public GameObject _carDestroyed;

    [SerializeField]
    public ParticleSystem _fire;

    [SerializeField]
    public ParticleSystem _smoke;

    [SerializeField]
    public ParticleSystem _explosion;

    float _explosionDuration;

    private void Update()
    {
        if(_explosionDuration > 0)
        {
            _explosionDuration -= Time.deltaTime;
            if(_explosionDuration <= 0)
            {
                if(_explosion != null)
                {
                    _explosion.Clear();
                    _explosion.Stop();
                }
            }
        }
    }

    public void ExplodeCar()
    {
        if(_fire != null)
        {
            _fire.gameObject.SetActive(true);
            _fire.Play();
        }
        if(_smoke != null)
        {
            _smoke.gameObject.SetActive(true);
            _smoke.Play();
        }
        if(_explosion != null)
        {
            _explosion.gameObject.SetActive(true);
            _explosion.Play();
            _explosionDuration = _explosion.main.duration - 0.01f;
        }
        if(_meshRenderer != null)
        {
            _meshRenderer.enabled = false;
        }
        if(_carDestroyed != null)
        {
            _carDestroyed.SetActive(true);
        }
    }
}
