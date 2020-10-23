using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

/// <summary>
/// Creates and colors wall
/// </summary>
public class WallManager : MonoBehaviour
{
    [SerializeField]
    GameObject stoneTemplate = null;
    [Header("Width relation to height")]
    [SerializeField]
    int widthRelation = 5;
    [Header("The height the wall has to reach before the width gets increased")]
    [SerializeField]
    int startMaxHeight = 3;
    WallFieldStorage fieldStorage = null;
    ObjectPool<Stone> objectPool = null;
    public List<Stone> StoneAccess => objectPool.GetActiveList();
    [SerializeField]
    Vector3 buildDirectionUp = new Vector3(0, 1, 0);
    [SerializeField]
    Vector3 buildDirectionRight = new Vector3(1, 0, 0);

    List<Color> wallColors = null;

    private void Start()
    {
        if (!stoneTemplate)
            CreateStone();
        fieldStorage = FindObjectOfType<WallFieldStorage>();
        fieldStorage.SetWallManager(this);
        objectPool = new ObjectPool<Stone>(stoneTemplate, fieldStorage.MaxStones, "StonePool");
    }

    void CreateStone()
    {
        stoneTemplate = GameObject.CreatePrimitive(PrimitiveType.Cube);
        stoneTemplate.isStatic = true;
    }

    public void SetStoneNumber(int _stoneNr)
    {
        //Improvable. Done here because of how GetGameObject in pool works
        objectPool.DeactivatePool();
        int _width = CalculateWidth(_stoneNr);
        for (int i = 0; i < _stoneNr; ++i)
        {
            //Get stone from pool and calculate position
            GameObject _stone = objectPool.GetGameObject();
            _stone.SetActive(true);
            _stone.transform.position = -buildDirectionRight * _width / 2; 
            _stone.transform.position += buildDirectionUp * (i / _width);
            _stone.transform.position += buildDirectionRight * (i % _width);
        }
        RecolorWall();
    }


    /// <summary>
    /// Calculate width based on width relation.
    /// Makes sure wall allways looks "nice" and with a good relation from width to height
    /// </summary>
    int CalculateWidth(int _stoneAmnt)
    {
        int _width = widthRelation;
        int _relativeHeight = startMaxHeight;
        int _countTillIncrHeight = 0;
        
        while (true)
        {
            ++_countTillIncrHeight;
            if (_stoneAmnt > _width * _relativeHeight)
            {
                if (_countTillIncrHeight <= widthRelation)
                    ++_width;
                else
                {
                    ++_relativeHeight;
                    _countTillIncrHeight = 0;
                }
            }
            else
                return _width;
        }
    }

    public void SetNewColorAmount(List<Color> _newColors)
    {
        wallColors = _newColors;
        RecolorWall();
    }

    public void RecolorWall()
    {
        if (wallColors == null)
            return;
        List<Stone> _activeStones = objectPool.GetActiveList();
        List<Color> _shuffledList = ShuffleColorList(wallColors);
        int _max = _activeStones.Count;
        for (int i = 0; i < _max; i++)
        {
            if (_shuffledList.Count < 1)
                _shuffledList = ShuffleColorList(wallColors);
            _activeStones[i].SetBaseColor(_shuffledList[0]);
            _shuffledList.RemoveAt(0);
        }
    }

    List<Color> ShuffleColorList(List<Color> _toShuffle, int _strength = 1)
    {
        List<Color> _newList = new List<Color>();
        foreach (var color in _toShuffle)
        {
            _newList.Add(color);
        }
        int _count = _newList.Count;
        _strength *= _count;
        for (int i = 0; i < _strength; i++)
        {
            if (_count < 2)
                break;
            int r1 = Random.Range(0, _count);
            int r2 = Random.Range(0, _count);
            while (r1 == r2)
                r2 = Random.Range(0, _count);
            Color _temp = _newList[r1];
            _newList[r1] = _newList[r2];
            _newList[r2] = _temp;
        }
        return _newList;
    }
}
