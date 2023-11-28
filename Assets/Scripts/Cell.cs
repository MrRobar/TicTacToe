using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private TurnChanger _turnChanger;
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private Button _cellButton;
    [SerializeField] private int _cellValue = -1;
    [SerializeField] private bool _clickable = true;
    private int _cellId;

    public event Action<int> OnCellClicked;

    public int CellValue
    {
        get { return _cellValue; }
        set { _cellValue = value; }
    }

    public int CellId
    {
        set { _cellId = value; }
    }

    public bool Clickable
    {
        set
        {
            _clickable = value;
            _cellButton.enabled = _clickable;
        }
    }

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _cellButton.onClick.AddListener(TryClick);
    }

    private void OnDisable()
    {
        _cellButton.onClick.RemoveListener(TryClick);
    }

    private void Init()
    {
        SetNumber(-1);
    }

    private void TryClick()
    {
        if (_clickable == false)
        {
            return;
        }

        SetNumber(_turnChanger.CurrentNumber);
        Clickable = false;
        OnCellClicked?.Invoke(_cellId);
    }

    public void SetNumber(int value)
    {
        _cellValue = value;
        _numberText.text = NumberToSymbol.GetSymbol(value);
        if (value == -1)
        {
            _numberText.text = "";
        }
    }
}