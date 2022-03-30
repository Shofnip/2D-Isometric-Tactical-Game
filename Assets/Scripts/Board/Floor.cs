using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
  [SerializeField] private Vector3Int minBoardPos;
  [SerializeField] private Vector3Int maxBoardPos;

  [HideInInspector] public Tilemap tilemap;
  
  private TilemapRenderer tilemapRenderer;
  public int contentOrder;

  public int FloorOrder { get => tilemapRenderer.sortingOrder; }

  void Awake()
  {
    tilemapRenderer = GetComponent<TilemapRenderer>();
    tilemap = GetComponent<Tilemap>();
  }

  public List<Vector3Int> LoadTiles() {
    List<Vector3Int> tiles = new List<Vector3Int>();

    for (int x = minBoardPos.x; x<=maxBoardPos.x; x++) {
      for (int y = minBoardPos.y; y<=maxBoardPos.y; y++) {
        Vector3Int currentPos = new Vector3Int(x, y, 0);
        if (tilemap.HasTile(currentPos)) {
          tiles.Add(currentPos);
        }
      }
    }

    return tiles;
  }
}
