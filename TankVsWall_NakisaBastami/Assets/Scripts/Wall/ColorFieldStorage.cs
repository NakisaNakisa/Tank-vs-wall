using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Connection from input field to wall manager
/// Informs wall manager about changes and number of colors
/// </summary>
public class ColorFieldStorage : MonoBehaviour
{
    ValueClamp infoSource = null;
    WallManager wallManager = null;

    private void Start()
    {
        wallManager = FindObjectOfType<WallManager>();
        infoSource = GetComponent<ValueClamp>();
        if (infoSource)
            infoSource.onValueChange += ChangeColors;
    }

    public void ChangeColors(int _value)
    {
        List<Color> _newColors = new List<Color>();
        for (int i = 0; i < _value; ++i)
        {
            Color _newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            while(_newColors.Contains(_newColor))
                _newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            _newColors.Add(_newColor);
        }
        wallManager.SetNewColorAmount(_newColors);
    }
}
