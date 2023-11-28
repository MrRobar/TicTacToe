using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentTurn;
    [SerializeField] private TextMeshProUGUI _winner;
    [SerializeField] private TextMeshProUGUI _gameover;
    [SerializeField] private Button _restartButton;
    [SerializeField] private WinChecker _winChecker;
    [SerializeField] private TurnChanger _turnChanger;
    [SerializeField] private CellsUpdater _cellsUpdater;

    public event Action OnRestartCalled;

    private void OnEnable()
    {
        _winChecker.OnGameOver += UpdateGameOver;
        _turnChanger.OnTurnChanged += UpdateTurn;
        _restartButton.onClick.AddListener(_cellsUpdater.InitCells);
        _restartButton.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        _winChecker.OnGameOver -= UpdateGameOver;
        _turnChanger.OnTurnChanged -= UpdateTurn;
        _restartButton.onClick.RemoveListener(_cellsUpdater.InitCells);
        _restartButton.onClick.RemoveListener(Restart);
    }

    private void UpdateGameOver()
    {
        var symbol = NumberToSymbol.GetSymbol(_winChecker.Winner);
        _winner.text = "Winner is " + symbol;
        _gameover.text = "Game over: True";
    }

    private void UpdateTurn(int digit)
    {
        _currentTurn.text = "Turn: " + NumberToSymbol.GetSymbol(digit);
    }

    private void Restart()
    {
        _currentTurn.text = "Turn: X";
        _gameover.text = "Game over: False";
        _winner.text = "";
        OnRestartCalled?.Invoke();
    }
}