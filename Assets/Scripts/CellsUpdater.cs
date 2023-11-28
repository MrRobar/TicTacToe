using System;
using UnityEngine;

public class CellsUpdater : MonoBehaviour
{
    [SerializeField] private Cell[] _cells = new Cell[9];
    [SerializeField] private WinChecker _winChecker;

    private void Awake()
    {
        InitCells();
    }

    private void OnEnable()
    {
        _winChecker.OnGameOver += DisableCells;
    }

    private void OnDisable()
    {
        _winChecker.OnGameOver -= DisableCells;
    }

    private void DisableCells()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].Clickable = false;
        }
    }

    public void InitCells()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].CellId = i;
            _cells[i].SetNumber(-1);
            _cells[i].Clickable = true;
        }
    }
}