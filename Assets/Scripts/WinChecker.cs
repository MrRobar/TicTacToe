using System;
using UnityEngine;

public class WinChecker : MonoBehaviour
{
    [SerializeField] private bool _gameOver = false;
    [SerializeField] private int _winner = -1;
    [SerializeField] private int _changedField = 0;
    [SerializeField] private UIController _controller;
    [SerializeField] private Cell[] _field = new Cell[9];

    public event Action OnGameOver;

    public int Winner
    {
        get { return _winner; }
    }

    private void OnEnable()
    {
        SubscribeToCellsClicks();
        _controller.OnRestartCalled += Restart;
    }

    private void OnDisable()
    {
        UnSubscribeToCellsClicks();
        _controller.OnRestartCalled -= Restart;
    }

    private void SubscribeToCellsClicks()
    {
        for (int i = 0; i < _field.Length; i++)
        {
            _field[i].OnCellClicked += UpdateChangedField;
        }
    }

    private void UnSubscribeToCellsClicks()
    {
        for (int i = 0; i < _field.Length; i++)
        {
            _field[i].OnCellClicked -= UpdateChangedField;
        }
    }

    private void UpdateChangedField(int fieldID)
    {
        _changedField = fieldID;
        CheckWin();
    }

    private void Restart()
    {
        _gameOver = false;
    }

    private void CheckWin()
    {
        CheckHorizontal(_changedField);
        CheckVertical(_changedField);
        CheckDiagonals(_changedField);
        if (_gameOver)
        {
            OnGameOver?.Invoke();
        }
    }

    private void CheckHorizontal(int changedId)
    {
        if (_gameOver)
        {
            return;
        }

        _gameOver = true;
        int startId = (changedId / 3) * 3; // 3 means one side
        _winner = _field[startId].CellValue;
        for (int i = startId + 1; i < startId + 3; i++)
        {
            if (_field[i].CellValue != _field[startId].CellValue)
            {
                _gameOver = false;
                _winner = -1;
                break;
            }
        }

        if (_winner == -1)
        {
            _gameOver = false;
        }
    }

    private void CheckVertical(int changedId)
    {
        if (_gameOver)
        {
            return;
        }

        _gameOver = true;
        int startId = changedId % 3;
        _winner = _field[startId].CellValue;
        for (int i = startId + 3; i < 9; i += 3)
        {
            if (_field[i].CellValue != _field[startId].CellValue)
            {
                _gameOver = false;
                _winner = -1;
                break;
            }
        }

        if (_winner == -1)
        {
            _gameOver = false;
        }
    }

    private void CheckDiagonals(int changedId)
    {
        if (_gameOver)
        {
            return;
        }

        _gameOver = true;
        var startId = 0;
        _winner = _field[startId].CellValue;
        if (changedId % 2 == 0 && changedId != 0 && changedId != 8)
        {
            startId = 2;
            for (int i = 4; i < 7; i += 2)
            {
                if (_field[i].CellValue != _field[startId].CellValue)
                {
                    _gameOver = false;
                    _winner = -1;
                    break;
                }
            }
        }
        else if (changedId % 4 == 0)
        {
            for (int i = 4; i < 9; i += 4)
            {
                if (_field[i].CellValue != _field[startId].CellValue)
                {
                    _gameOver = false;
                    _winner = -1;
                    break;
                }
            }
        }
        else
        {
            _gameOver = false;
        }
    }
}