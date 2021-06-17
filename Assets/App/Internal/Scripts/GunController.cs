using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    Explosion _explosionPrefab;

    public void Shoot()
    {
        Ray ray = new Ray(transform.position, -transform.right);
        Debug.DrawRay(transform.position, -transform.right, Color.yellow, 5f);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (ExplosionPool.TryGetRandomExplosion(out Explosion e))
            {
                e.transform.position = hit.point;
                e.Explode();
            }
        }
    }
}
