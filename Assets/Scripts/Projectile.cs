using UnityEngine;

public class Projectile : MonoBehaviour
{
  public float speed;
  public float lifetime;

  private void Start()
  {
    Invoke(nameof(Deactivate), lifetime);
  }

  private void Update()
  {
    transform.Translate(Vector3.forward * speed * Time.deltaTime);
  }

  private void Deactivate()
  {
    gameObject.SetActive(false);
  }
}
