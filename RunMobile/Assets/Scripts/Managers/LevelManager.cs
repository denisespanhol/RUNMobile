using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform levelContainer;

    public List<LevelPieceBase> levels;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> startPieces;
    public List<LevelPieceBase> endPieces;
    public int piecesNumber = 5;
    public int startNumber = 1;
    public int endNumber = 1;

    private List<LevelPieceBase> _spawnedPieces = new();

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

        ColorManager.Instance.MaterialChanger(ColorManager.MaterialList.ROCKS);

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

    private void CreateLevelPiece(List<LevelPieceBase> pieces)
    {
        var piece = pieces[Random.Range(0, pieces.Count)];
        var spawnedPiece = Instantiate(piece, levelContainer);

        if(_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            spawnedPiece.transform.position = lastPiece.endPosition.transform.position + new Vector3(0, 0, 4.8f);
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces()
    {
        foreach(LevelPieceBase piece in _spawnedPieces)
        {
            Destroy(piece.gameObject);
        }

        _spawnedPieces.Clear();
    }
}
