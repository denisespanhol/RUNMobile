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
    private ColorManager.MaterialList materialFloor;
    private ColorManager.MaterialList materialWall;
    private int _randomNumber;

    private void Start()
    {
        RandomizeAllTheMaterials();
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

        List<MeshRenderer> renderers = new();

        foreach(var piece in _spawnedPieces)
        {
            MeshRenderer objectWithRenderer = piece.transform.Find("PRF_floor").GetComponent<MeshRenderer>();
            if (objectWithRenderer != null) renderers.Add(objectWithRenderer);

        }
        if (renderers != null) ColorManager.Instance.pieces = renderers;

        ColorManager.Instance.MaterialFloorChanger(materialFloor);

        List<MeshRenderer> wallRenderers = new();

        foreach (var piece in _spawnedPieces)
        {
            if (piece.transform.Find("PRF_Wall") != null)
            {
                Transform objectWithRenderer = piece.transform.Find("PRF_Wall");
                MeshRenderer childRenderer = objectWithRenderer.GetComponentInChildren<MeshRenderer>();
               if (childRenderer != null) wallRenderers.Add(childRenderer);

            }    
        }
        if (wallRenderers != null) ColorManager.Instance.walls = wallRenderers;

        
        ColorManager.Instance.MaterialWallChanger(materialWall);
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

    private void RandomizeFloorMaterials()
    {
        _randomNumber = Random.Range(1, 4);
        Debug.Log("Floor: " + _randomNumber);

        if (_randomNumber == 1) materialFloor = ColorManager.MaterialList.GRASS01;
        if (_randomNumber == 2) materialFloor = ColorManager.MaterialList.GRASS02;
        if (_randomNumber == 3) materialFloor = ColorManager.MaterialList.GROUND;
        if (_randomNumber == 4) materialFloor = ColorManager.MaterialList.ROCKS;
    }

    private void RandomizeWallMaterials()
    {
        _randomNumber = Random.Range(1, 4);
        Debug.Log("Wall: " + _randomNumber);

        if (_randomNumber == 1) materialWall = ColorManager.MaterialList.GRASS01;
        if (_randomNumber == 2) materialWall = ColorManager.MaterialList.GRASS02;
        if (_randomNumber == 3) materialWall = ColorManager.MaterialList.GROUND;
        if (_randomNumber == 4) materialWall = ColorManager.MaterialList.ROCKS;
    }

    private void RandomizeAllTheMaterials()
    {
        RandomizeFloorMaterials();
        RandomizeWallMaterials();
    }
}
