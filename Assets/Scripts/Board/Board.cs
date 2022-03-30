using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
  private Grid grid;
  private Dictionary<Vector3Int, TileLogic> tiles;
  [SerializeField] private List<Floor> floors;

  private static Board instance;

  void Awake()
  {
    grid = GetComponent<Grid>();
    tiles = new Dictionary<Vector3Int, TileLogic>();
    instance = this;
  }

  void Start() {
    InitBoard();
  }

  void InitBoard() {
    LoadFloors();
    Debug.Log("Foram criados " + tiles.Count + " tiles");
  }

  void LoadFloors() {
    for (int i=0; i<floors.Count; i++) {
      List<Vector3Int> floorTiles = floors[i].LoadTiles();
      for (int j=0; j<floorTiles.Count; j++) {
        if (!tiles.ContainsKey(floorTiles[j])) {
          TileCreate(floorTiles[j], floors[i]);
        }
      }
    }
  }

  void TileCreate(Vector3Int pos, Floor floor) {
    Vector3 worldPos = grid.CellToWorld(pos);
    worldPos.y += floor.tilemap.tileAnchor.y/2;
    TileLogic tileLogic = new TileLogic(pos, worldPos, floor);
    tiles.Add(pos, tileLogic);
  }
}
