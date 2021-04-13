using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Stick _stickTemplate;
    [SerializeField] private MarkStick _markStickTemplate;
    [Space]
    [SerializeField] private float _maxJumpForce;
    [SerializeField] private float _maxDistanceTouch;

    private Stick _stick;
    private Rigidbody _rigidbody;

    private Vector3 _firstTouch;
    private float _jumpForce;

    public event UnityAction PlayerTakeCoin;
    public event UnityAction<Rigidbody> PlayerFinishGame;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        BindingStick();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstTouch = Input.mousePosition;
            EventHandling();
        }
        
        if (Input.GetMouseButton(0))
        {
            FollowStick();
            TensionHandlerStick();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            UntieStick();
            Jump();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            if (coin.IsActiv)
            {
                PlayerTakeCoin?.Invoke();
                coin.DeleteCoin();
            }
        }
    }

    private void EventHandling()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out Block block))
            {
                _rigidbody.velocity = Vector3.zero;
            }
            else if (hitInfo.collider.TryGetComponent(out Segment segment))
            {
                _rigidbody.isKinematic = true;
                _rigidbody.velocity = Vector3.zero;

                BindingStick();
            }
            else if (hitInfo.collider.TryGetComponent(out Finish finish))
            {
                PlayerFinishGame?.Invoke(_rigidbody);
                BindingStick();
            }

            SetMarkStick(hitInfo.point);
        }
    }

    private void Jump()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _rigidbody.AddRelativeTorque(Vector3.left * _jumpForce, ForceMode.Impulse);

    }

    private void FollowStick()
    {
        if (_stick)
        {
            transform.position = _stick.LastBone.position;
        }
    }

    private void BindingStick()
    {
        UntieStick();

        Stick stick = Instantiate(_stickTemplate);

        Vector3 positionStick = new Vector3(0, transform.position.y, -3);
        stick.transform.position = positionStick;

        _stick = stick;
    }

    private void UntieStick()
    {
        if (_stick)
        {
            Destroy(_stick.gameObject);
        }
    }

    private void SetMarkStick(Vector3 markPosition)
    {
        Instantiate(_markStickTemplate, markPosition, Quaternion.identity);
    }

    private void TensionHandlerStick()
    {
        if (!_stick)
            return;

        float distance = Vector3.Distance(_firstTouch, Input.mousePosition);

        if(distance < _maxDistanceTouch)
        {
            float estimatedValue = Mathf.Clamp(distance, 0, _maxDistanceTouch);
            estimatedValue = 1 / (_maxDistanceTouch) * (_maxDistanceTouch - estimatedValue);
            estimatedValue = -Mathf.Clamp(estimatedValue, 0f, 1f) + 1;

            _jumpForce = _maxJumpForce * estimatedValue;

            _stick.SetIncline(estimatedValue);
        }
    }
}
