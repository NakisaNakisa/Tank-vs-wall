using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Reads input field. 
/// Displays default message at input field. 
/// Clamps input field and allows only numbers. 
/// Has delegate for on value changed
/// </summary>
[RequireComponent(typeof(InputField))]
public class ValueClamp : MonoBehaviour
{
    [SerializeField]
    int minValue = 0;
    [SerializeField]
    int maxValue = 1;
    [SerializeField]
    string message = "DEFAULT MESSAGE";

    public int GetMaxValue => maxValue;
    //public int GetMinValue => minValue;

    public delegate void OnValueChange(int _value);
    public OnValueChange onValueChange;

    InputField inputField = null;

    private void Start()
    {
        inputField = GetComponent<InputField>();
        message += "(" + minValue.ToString() + "-" + maxValue.ToString() + ")";
        inputField.placeholder.GetComponent<Text>().text = message;
    }

    public void ExecuteValueChanged(string _value)
    {
        int _translatedValue;
        if (int.TryParse(_value, out _translatedValue))
        {
            int _finalValue = _translatedValue < minValue ? minValue : _translatedValue > maxValue ? maxValue : _translatedValue;
            inputField.text = _finalValue.ToString();
            onValueChange?.Invoke(_finalValue);
        }
        else
            inputField.text = "";
    }
}
