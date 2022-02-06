using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private CellType cellType;

    public void Setup(CellType cellType)
    {
        this.cellType = cellType;
    }
}