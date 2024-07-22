using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
    public List<MeshRenderer> pieces;

    public Texture grass01;
    public Texture grass02;
    public Texture ground;
    public Texture rocks;

    public enum MaterialList
    {
        GRASS01,
        GRASS02,
        GROUND,
        ROCKS
    }

    public void MaterialChanger(MaterialList textureOption)
    {
        foreach(MeshRenderer piece in pieces)
        {
            if (textureOption == MaterialList.GRASS01) piece.materials[0].SetTexture("Grass01", grass01);
            if (textureOption == MaterialList.GRASS02) piece.materials[0].SetTexture("Grass02", grass02);
            if (textureOption == MaterialList.GROUND) piece.materials[0].SetTexture("Ground", ground);
            if (textureOption == MaterialList.ROCKS) piece.materials[0].SetTexture("Rocks", rocks);
        }
    }
}

[System.Serializable]
public class ColorSetup
{
    public ColorManager.MaterialList materialList;
}
