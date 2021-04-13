using UnityEngine;
using UnityEngine.UI;

public class CounterCoins : MonoBehaviour
{
    [SerializeField] private Text _counterText;
    [SerializeField] private Ball _ball;

    private int _counter;

    private void OnEnable()
    {
        _ball.PlayerTakeCoin += OnPlayerTakeCoin;
    }

    private void OnDisable()
    {
        _ball.PlayerTakeCoin -= OnPlayerTakeCoin;        
    }

    private void OnPlayerTakeCoin()
    {
        _counter++;
        _counterText.text = _counter.ToString();
    }
}
