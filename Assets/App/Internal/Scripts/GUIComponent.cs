using UnityEngine;
using UnityEngine.UI;

public class GUIComponent : MonoBehaviour
{
    const float FLASH_DURATION = 4f;

    [SerializeField]
    Image _outline;

    [SerializeField]
    Image _fill;

    float flashDelta;

    private void Awake()
    {
        flashDelta = 0;
        HideHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(flashDelta > 0)
        {
            ShowHealth();

            flashDelta -= Time.deltaTime;

            if(flashDelta < 0)
            {
                HideHealth();
            }        
        }
    }

    void ShowHealth() 
    {
        if (!(_outline == null) && !_outline.gameObject.activeInHierarchy)
        {
            _outline.gameObject.SetActive(true);
        }
        if (!(_fill == null) && !_fill.gameObject.activeInHierarchy)
        {
            _fill.gameObject.SetActive(true);
        }
    }

    void HideHealth()
    {
        if (!(_outline == null) && _outline.gameObject.activeInHierarchy)
        {
            _outline.gameObject.SetActive(false);
        }
        if (!(_fill == null) && _fill.gameObject.activeInHierarchy)
        {
            _fill.gameObject.SetActive(false);
        }
    }

    public void FlashHealth()
    {
        flashDelta = FLASH_DURATION;
    }

    public void SetHealth(float health)
    {
        if(health >= 0)
        {
            if (_fill != null)
            {
                _fill.fillAmount = health;
            }
        }
    }
}
