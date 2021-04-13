using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Stick : MonoBehaviour
{
    [SerializeField] private Transform _lastBone;

    private Animator _animator;

    public Transform LastBone => _lastBone;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIncline(float incline)
    {
        _animator.SetFloat("Blend", incline);
    }
}
