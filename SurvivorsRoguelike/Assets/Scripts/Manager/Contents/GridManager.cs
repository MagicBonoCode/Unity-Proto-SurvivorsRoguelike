using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

class Cell
{
    public HashSet<GameObject> Objects = new HashSet<GameObject>();
}

public class GridManager
{
    private Grid _grid;
    private Dictionary<Vector3Int, Cell> _cells = new Dictionary<Vector3Int, Cell>();

    public void SetGrid()
    {
        if (_grid == null)
        { 
            GameObject grid = Managers.Resource.Instantiate("Grid.prefab");
            _grid = grid.GetComponent<Grid>();
        }
        _grid.transform.position = Vector3.zero;
    }

    private Cell GetCell(Vector3Int cellPos)
    {
        Cell cell = null;
        if (_cells.TryGetValue(cellPos, out cell) == false)
        {
            cell = new Cell();
            _cells.Add(cellPos, cell);
        }

        return cell;
    }

    public void Add(GameObject gameObject)
    {
        Vector3Int cellPos = _grid.WorldToCell(gameObject.transform.position);
        Cell cell = GetCell(cellPos);
        cell.Objects.Add(gameObject);
    }

    public void Remove(GameObject gameObject)
    {
        Vector3Int cellPos = _grid.WorldToCell(gameObject.transform.position);
        Cell cell = GetCell(cellPos);
        cell.Objects.Remove(gameObject);
    }

    public List<GameObject> GatherObjects(Vector3 pos, float range)
    {
        List<GameObject> objects = new List<GameObject>();

        Vector3Int left = _grid.WorldToCell(pos + new Vector3(-range, 0));
        Vector3Int right = _grid.WorldToCell(pos + new Vector3(+range, 0));
        Vector3Int bottom = _grid.WorldToCell(pos + new Vector3(0, -range));
        Vector3Int top = _grid.WorldToCell(pos + new Vector3(0, +range));

        int minX = left.x;
        int maxX = right.x;
        int minY = bottom.y;
        int maxY = top.y;

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if (_cells.ContainsKey(new Vector3Int(x, y, 0)))
                {
                    objects.AddRange(_cells[new Vector3Int(x, y, 0)].Objects);
                }
            }
        }

        return objects;
    }

    public void Clear()
    {
        if (_grid != null)
        {
            _cells.Clear();
            Managers.Resource.Destroy(_grid.gameObject);
        }
    }
}
