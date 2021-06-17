using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    public Transform Origin;

    void Awake()
    {
        Instance = this;

        if(Origin == null)
        {
            Origin = transform;
        }
    }

    public static Vector3 WorldScale()
    {
        var s = 1.0f;
        var worldScale = new Vector3(s, s, s);
        if (Instance != null)
        {
            var x = worldScale.x * Instance.Origin.localScale.x;
            var y = worldScale.y * Instance.Origin.localScale.y;
            var z = worldScale.z * Instance.Origin.localScale.z;
            worldScale = new Vector3(x, y, z);
        }
        return worldScale;
    }
}
