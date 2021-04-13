using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private void OnEnable()
    {
        _ball.PlayerFinishGame += OnPlayerFinishGame;
    }

    private void OnDisable()
    {
        _ball.PlayerFinishGame -= OnPlayerFinishGame;        
    }

    private void OnPlayerFinishGame(Rigidbody rigidbody)
    {
        _ball.enabled = false;
        rigidbody.isKinematic = true;
    }
}
