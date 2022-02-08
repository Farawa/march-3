using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Vector2Int fieldSize;
    [SerializeField] private Vector2 spacing;
    [SerializeField] private GameObject cellPrefab;
    private static Dictionary<Vector2Int, Cell> cells;
    private Vector2 cellSize;
    public static Action GenerateDone;

    internal static Cell GetCell(Vector2Int index)
    {
        return cells[index];
    }

    private void Start()
    {
        cells = new Dictionary<Vector2Int, Cell>();
        GenerateField();
    }

    private void GenerateField()
    {
        cellSize = (cellPrefab.transform as RectTransform).sizeDelta;
        float startX = (fieldSize.x / 2 * (cellSize.x + spacing.x)) * -1;
        float startY = fieldSize.y / 2 * (cellSize.y + spacing.y);
        var cellPosition = new Vector3(startX, startY);
        for (int i = 0; i < fieldSize.y; i++)
        {
            for (int j = 0; j < fieldSize.x; j++)
            {
                var cell = Instantiate(cellPrefab, transform).GetComponent<Cell>();
                cells.Add(new Vector2Int(j, i), cell);
                cell.transform.localPosition = cellPosition;
                cellPosition.x += spacing.x + cellSize.x;
                CellType cellType;
                if (i == 0)
                    cellType = CellType.spawner;
                else
                    cellType = CellType.active;
                cell.Setup(cellType, new Vector2Int(j, i));
            }
            cellPosition.x = startX;
            cellPosition.y -= spacing.y + cellSize.y;
        }
        GenerateDone?.Invoke();
    }

    public static Vector3? GetCellPosition(Vector2Int index)
    {
        Cell cell;
        if (cells.TryGetValue(index, out cell))
        {
            return cell.transform.position;
        }
        return null;
    }

    public static void CaptureCell(Vector2Int index, int id)
    {
        cells[index].Capture(id);
    }

    public static void ClearCell(Vector2Int index)
    {
        cells[index].Clear();
    }

    public static bool IsCellFree(Vector2Int index)
    {
        Cell cell;
        if (cells.TryGetValue(index, out cell))
        {
            if (cell)
            {
                return cells[index].captureBallId == -1 ? true : false;
            }
        }
        return false;
    }
}
