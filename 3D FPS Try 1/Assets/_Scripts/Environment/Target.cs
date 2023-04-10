
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health = 20f;
    [SerializeField] ParticleSystem Explosion;
    [SerializeField] MeshRenderer _renderer;
    private Collider _collider;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _collider = GetComponent<Collider>();
        Explosion.Play();
        _renderer.enabled = false;
        _collider.enabled = false;
        Destroy(gameObject, 0.5f);
    }

}
