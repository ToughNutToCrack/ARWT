using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    [SerializeField]
    PlayerController _playerController;
    
    public enum PointerCommands
    {
        None,
        MoveForward,
        MoveBackward,
        RotateRight,
        RotateLeft,
        Shoot,
    }

    Dictionary<PointerCommands, System.Action<bool>> _pointerEvents;

    public static void ProcessInput(PointerCommands pointerCommand, bool pointerDown)
    {
        if(Instance != null)
        {
            var events = Instance._pointerEvents;
            if(events.TryGetValue(pointerCommand, out System.Action<bool> e))
            {
                e.Invoke(pointerDown);
            }
        }
    }

    void Awake()
    {
        Instance = this;

        _pointerEvents = _pointerEvents ?? new Dictionary<PointerCommands, System.Action<bool>>()
        {
            { PointerCommands.MoveForward, OnMoveForward },
            { PointerCommands.MoveBackward, OnMoveBackward },
            { PointerCommands.RotateLeft, OnRotateLeft },
            { PointerCommands.RotateRight, OnRotateRight },
            { PointerCommands.Shoot, OnShoot },
        };
    }

    void OnMoveForward(bool pointerDown) 
    {
        if(_playerController != null)
        {
            _playerController.IsMovingForward = pointerDown;
        }
    }

    void OnMoveBackward(bool pointerDown)
    {
        if (_playerController != null)
        {
            _playerController.IsMovingBackward = pointerDown;
        }
    }

    void OnRotateRight(bool pointerDown)
    {
        if (_playerController != null)
        {
            _playerController.IsRotatingRight = pointerDown;
        }
    }

    void OnRotateLeft(bool pointerDown)
    {
        if (_playerController != null)
        {
            _playerController.IsRotatingLeft = pointerDown;
        }
    }

    void OnShoot(bool pointerDown)
    {
        if (_playerController != null)
        {
            _playerController.ShootGun();
        }
    }
}
