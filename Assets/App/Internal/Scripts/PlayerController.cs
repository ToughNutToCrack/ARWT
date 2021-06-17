using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float DEFAULT_ROTATION_SPEED = 60f;
    const float DEFAULT_MOVEMENT_SPEED = 60f;

    [SerializeField]
    BumperComponent FrontBumper;

    [SerializeField]
    BumperComponent RearBumper;

    [SerializeField]
    GunController _gunController;

    [SerializeField]
    float _rotationSpeed = 1f;

    [SerializeField]
    float _movementSpeed = 1f;

    public bool IsMovingForward { get; set; } = false;
    public bool IsMovingBackward { get; set; } = false;
    public bool IsRotatingLeft { get; set; } = false;
    public bool IsRotatingRight { get; set; } = false;

    public float RotationSpeed
    {
        get
        {
            return DEFAULT_ROTATION_SPEED * (_rotationSpeed > 0f ? _rotationSpeed : 1f);
        }
    }

    public float MovementSpeed
    {
        get
        {
            return DEFAULT_MOVEMENT_SPEED * (_movementSpeed > 0f ? _movementSpeed : 1f);
        }
    }

    void Awake()
    {
        if (_movementSpeed <= 0)
        {
            _movementSpeed = 1f;
        }

        if (_rotationSpeed <= 0)
        {
            _rotationSpeed = 1f;
        }
    }

    void FixedUpdate()
    {
        if (IsMovingForward)
        {
            var ms = MovementSpeed * Time.deltaTime;
            var vec = Vector3.up;
            vec.Scale(new Vector3(-ms, -ms, -ms));
            
            transform.Translate(vec);

            if (IsCollidingFront())
            {
                var s = -1.5f;
                vec.Scale(new Vector3(s, s, s));
                transform.Translate(vec);
            }
        }
        else if (IsMovingBackward)
        {
            var ms = MovementSpeed * Time.deltaTime;
            var vec = Vector3.up;
            vec.Scale(new Vector3(ms, ms, ms));

            transform.Translate(vec);

            if (IsCollidingBack())
            {
                var s = -1.5f;
                vec.Scale(new Vector3(s, s, s));
                transform.Translate(vec);
            }
        }
        else if (IsRotatingRight)
        {
            transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
        }
        else if (IsRotatingLeft)
        {
            transform.Rotate(new Vector3(0, 0, (-RotationSpeed) * Time.deltaTime));
        }
    }

    void GetKeyboardInput()
    {
        IsMovingForward = Input.GetKey(KeyCode.W);
        IsMovingBackward = Input.GetKey(KeyCode.S);
        IsRotatingLeft = Input.GetKey(KeyCode.A);
        IsRotatingRight = Input.GetKey(KeyCode.D);
    }

    public bool IsCollidingFront()
    {
        bool colliding = false;
        if (FrontBumper != null)
        {
            colliding = FrontBumper.IsColliding;
        }
        return colliding;
    }

    public bool IsCollidingBack()
    {
        bool colliding = false;
        if (RearBumper != null)
        {
            colliding = RearBumper.IsColliding;
        }
        return colliding;
    }

    public void ShootGun()
    {
        if (_gunController != null)
        {
            _gunController.Shoot();
        }
    }
}
