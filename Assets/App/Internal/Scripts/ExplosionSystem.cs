using UnityEngine;

public class ExplosionSystem : MonoBehaviour
{
    ParticleSystem _myParticleSystem;

    float _delta = 0;
    float _duration = 1f;

    public float Duration => _duration;

    void Awake()
    {
        _myParticleSystem = GetComponent<ParticleSystem>();
        if (_myParticleSystem != null)
        {
            _myParticleSystem.Clear();
            _myParticleSystem.Stop();
            _duration = _myParticleSystem.main.duration;
        }
    }

    void Update()
    {
        if(_delta > 0)
        {
            _delta -= Time.deltaTime;
            if(_delta < 0)
            {
                StopExplosion();
            }
        } 
    }

    void StopExplosion()
    {
        if (_myParticleSystem != null)
        {
            _myParticleSystem.Clear();
            _myParticleSystem.Stop();
        }
    }

    public void PlayExplosion()
    {
        if (_myParticleSystem != null)
        {
            StopExplosion();
            _myParticleSystem.Play();

            _duration = _duration < 0 ? 0 : _duration;
            _delta = _duration - (_duration / 16f);
        }
    }
}
