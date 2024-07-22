using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
    public List<MeshRenderer> pieces;
    public List<MeshRenderer> walls;

    public Material grass01;
    public Material grass02;
    public Material ground;
    public Material rocks;

    private MeshRenderer wallRenderer;

    public enum MaterialList
    {
        GRASS01,
        GRASS02,
        GROUND,
        ROCKS
    }

    public void MaterialFloorChanger(MaterialList material)
    {
        foreach (MeshRenderer piece in pieces)
        {
            if (material == MaterialList.GRASS01) piece.sharedMaterial = grass01;
            else if (material == MaterialList.GRASS02) piece.sharedMaterial = grass02;
            else if (material == MaterialList.GROUND) piece.sharedMaterial = ground;
            else if (material == MaterialList.ROCKS) piece.sharedMaterial = rocks;
        }
    }

    public void MaterialWallChanger(MaterialList material)
    {


        foreach (MeshRenderer wall in walls)
        {
            if (material == MaterialList.GRASS01) wall.sharedMaterial = grass01;
            else if (material == MaterialList.GRASS02) wall.sharedMaterial = grass02;
            else if (material == MaterialList.GROUND) wall.sharedMaterial = ground;
            else if (material == MaterialList.ROCKS) wall.sharedMaterial = rocks;
        }
    }
}