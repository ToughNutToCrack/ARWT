using UnityEngine;

public class CarComponent : MonoBehaviour
{
    [SerializeField]
    DamageComponent _damageComponent;

    [SerializeField]
    ExplodeCarComponent _explodeCarComponent;

    void Awake()
    {
        if(_damageComponent != null && _explodeCarComponent != null)
        {
            _damageComponent.OnDeath.AddListener(() => {
                _explodeCarComponent.ExplodeCar();
            });
        }
    }
}
