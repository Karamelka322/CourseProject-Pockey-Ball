using UnityEngine;

public class MarkStick : MonoBehaviour
{
    private readonly float _lifeTime = .5f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
