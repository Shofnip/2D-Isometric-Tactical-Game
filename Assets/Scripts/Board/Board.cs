using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
  [SerializeField] private List<Floor> floors;

  private Grid grid;
  public Dictionary<Vector3Int, TileLogic> tiles;

  public static Board instance;

  void Awake()
  {
    grid = GetComponent<Grid>();
    tiles = new Dictionary<Vector3Int, TileLogic>();
    instance = this;
  }

  void Start()
  {
    InitBoard();
  }

  void InitBoard()
  {
    LoadFloors();
    ShadowOrdering();
  }

  void LoadFloors()
  {
    for (int i = 0; i < floors.Count; i++)
    {
      List<Vector3Int> floorTiles = floors[i].LoadTiles();
      for (int j = 0; j < floorTiles.Count; j++)
      {
        if (!tiles.ContainsKey(floorTiles[j]))
        {
          TileCreate(floorTiles[j], floors[i]);
        }
      }
    }
  }

  void TileCreate(Vector3Int pos, Floor floor)
  {
    Vector3 worldPos = grid.CellToWorld(pos);
    worldPos.y += (floor.tilemap.tileAnchor.y / 2) + 0.25f;
    TileLogic tileLogic = new TileLogic(pos, worldPos, floor);
    tiles.Add(pos, tileLogic);
  }

  void ShadowOrdering () {
    foreach (TileLogic tile in tiles.Values)
    {
      int floorIndex = floors.IndexOf(tile.Floor);
      floorIndex -= 2;

      if (floorIndex < 0) {
        continue;
      }

      Floor floorToCheck = floors[floorIndex];
      Vector3Int tilePos = tile.TilePos;

      if ( HasFloorInPosition(floorToCheck, tilePos+Vector3Int.right) ||
      HasFloorInPosition(floorToCheck, tilePos+Vector3Int.up) ||
      HasFloorInPosition(floorToCheck, tilePos+Vector3Int.right+Vector3Int.up)) {
        tile.ContentOrder = floorToCheck.FloorOrder;
      }
    }
  }

  bool HasFloorInPosition (Floor floor, Vector3Int position) {
    if (floor.tilemap.HasTile(position)) return true;

    return false;
  }
}
