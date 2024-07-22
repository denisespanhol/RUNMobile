using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform levelContainer;

    public List<GameObject> levels;

    [Header("Pieces")]
    public List<GameObject> levelPieces;
    public List<GameObject> startPieces;
    public List<GameObject> endPieces;
    public int piecesNumber = 5;
    public int startNumber = 1;
    public int endNumber = 1;

    private GameObject _currentLevel;
    private Transform _endPosition;
    private int _index = 0;

    private List<GameObject> _spawnedPieces = new();

    private void Awake()
    {
        GenerateLevel();
    }

    private void Update()
    {
        //GenerateLevel();
    }

    private void GenerateLevel()
    {
        CleanSpawnedPieces();

        for(int i = 0; i < startNumber; i++)
        {
            CreateLevelPiece(startPieces);
        }

        for (int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece(levelPieces);
        }

        for (int i = 0; i < endNumber; i++)
        {
            CreateLevelPiece(endPieces);
        }
    }

    private void CreateLevelPiece(List<GameObject> pieces)
    {
        var piece = pieces[Random.Range(0, pieces.Count)];
        var spawnedPiece = Instantiate(piece, levelContainer);
        _endPosition = spawnedPiece.transform.Find("EndPoint");

        if(_spawnedPieces.Count > 0)
        {
            spawnedPiece.transform.position = _endPosition.transform.position + new Vector3(0, 0, 4.8f);
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces()
    {
        foreach(GameObject piece in _spawnedPieces)
        {
            Destroy(piece.gameObject);
        }

        _spawnedPieces.Clear();
    }
}
