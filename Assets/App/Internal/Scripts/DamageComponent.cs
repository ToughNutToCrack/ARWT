using UnityEngine;
using UnityEngine.Events;

public class DamageComponent : MonoBehaviour
{
    // component should be on same object as collider & rigidbody
    [SerializeField]
    public float HP = 1.0f;

    public UnityEvent OnDeath;

    bool _died = false;

    [SerializeField]
    GUIComponent _guiComponent;

    public void TakeDamage(float damage)
    {
        if (!_died)
        {
            var normalized = damage / 25f;
            HP -= normalized;
            HP = HP < 0 ? 0 : HP;

            if (_guiComponent != null && HP >= 0)
            {
                _guiComponent.SetHealth(HP);
                _guiComponent.FlashHealth();
            }
        }

        if(!_died && HP <= 0)
        {
            _died = true;
            OnDeath?.Invoke();
        }
    }
}
