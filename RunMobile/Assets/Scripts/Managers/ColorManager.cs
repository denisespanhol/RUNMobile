using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
    public List<MeshRenderer> pieces;
    public List<ColorSetup> setups;

    public Material grass01;
    public Material grass02;
    public Material ground;
    public Material rocks;

    public enum MaterialList
    {
        GRASS01,
        GRASS02,
        GROUND,
        ROCKS
    }

    public void MaterialChanger(MaterialList material)
    {
        foreach(MeshRenderer piece in pieces)
        {
            setups.ForEach(setup => {
                if (setup.materialList == material)
                {
                    foreach(MeshRenderer piece in pieces)
                    {
                        if (material == MaterialList.GRASS01) piece.sharedMaterial = grass01;
                        if (material == MaterialList.GRASS02) piece.sharedMaterial = grass02;
                        if (material == MaterialList.GROUND) piece.sharedMaterial = ground;
                        if (material == MaterialList.ROCKS) piece.sharedMaterial = rocks;
                    }
                }
            });
        }
    }
}

[System.Serializable]
public class ColorSetup
{
    public ColorManager.MaterialList materialList;
}
