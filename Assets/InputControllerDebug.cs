using UnityEngine;
using UnityEngine.UI;

public class InputControllerDebug : MonoBehaviour
{
    [SerializeField]
    PlayerController _playerController;
    
    [SerializeField]
    Button W;
    [SerializeField]
    Button A;
    [SerializeField]
    Button S;
    [SerializeField]
    Button D;
    [SerializeField]
    Button Shoot;

    private void Awake()
    {
        if(_playerController != null)
        {
            // forward
            if (W != null)
            {
                W.onClick.AddListener(() =>
                {
                    _playerController.IsMovingForward = !_playerController.IsMovingForward;
                });
            }

            // backward
            if (S != null)
            {
                S.onClick.AddListener(() =>
                {
                    _playerController.IsMovingBackward = !_playerController.IsMovingBackward;
                });
            }

            // left
            if (A != null)
            {
                A.onClick.AddListener(() =>
                {
                    _playerController.IsRotatingLeft = !_playerController.IsRotatingLeft;
                });
            }

            // right
            if (D != null)
            {
                D.onClick.AddListener(() =>
                {
                    _playerController.IsRotatingRight = !_playerController.IsRotatingRight;
                });
            }

            // shoot
            if (Shoot != null)
            {
                Shoot.onClick.AddListener(() =>
                {
                    _playerController.ShootGun();
                });
            }
        }
    }
}
