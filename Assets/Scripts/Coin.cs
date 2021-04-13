using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyCointEffect;
    [SerializeField] private float _delayDestroy;

    private Rigidbody _rigidbody;

    public bool IsActiv { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        IsActiv = true;
    }

    public void DeleteCoin()
    {
        IsActiv = false;
        _rigidbody.isKinematic = false;

        DestroyEffect(_destroyCointEffect);
        Destroy(gameObject, _delayDestroy);
    }

    private void DestroyEffect(ParticleSystem effect)
    {
        Instantiate(effect, transform.position, Quaternion.identity, null);
    }
}
