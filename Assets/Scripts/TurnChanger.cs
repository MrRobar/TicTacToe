using System;
using UnityEngine;

public class TurnChanger : MonoBehaviour
{
    [SerializeField] private UIController _controller;
    [SerializeField] private int _currentNumber = 1;
    [SerializeField] private Cell[] _cells;

    public event Action<int> OnTurnChanged;

    public int CurrentNumber
    {
        get { return _currentNumber; }
    }

    private void Awake()
    {
        _currentNumber = 1;
    }

    private void OnEnable()
    {
        _controller.OnRestartCalled += SetDefaults;
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].OnCellClicked += ChangeTurn;
        }
    }

    private void OnDisable()
    {
        _controller.OnRestartCalled -= SetDefaults;
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].OnCellClicked -= ChangeTurn;
        }
    }

    private void ChangeTurn(int cellId = -1)
    {
        _currentNumber = (_currentNumber == 1) ? 0 : 1;
        OnTurnChanged?.Invoke(_currentNumber);
    }

    private void SetDefaults()
    {
        _currentNumber = 1;
    }
}