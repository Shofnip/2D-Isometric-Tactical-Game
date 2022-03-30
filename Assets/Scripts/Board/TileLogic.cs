using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic
{
  private Vector3Int _tilePos;
  private Vector3 _worldPos;
  private GameObject _content;
  private Floor _floor;
  private int _contentOrder;

  public Vector3Int TilePos { get => _tilePos; }
  public Vector3 WorldPos { get => _worldPos; }
  public Floor Floor { get => _floor; }
  
  public int ContentOrder { get => _contentOrder; set => _contentOrder = value; }
  public GameObject Content { get => _content; set => _content = value; }

  public TileLogic() { }

  public TileLogic(Vector3Int tilePos, Vector3 worldPos, Floor floor)
  {
    _tilePos = tilePos;
    _worldPos = worldPos;
    _floor = floor;
    _contentOrder = floor.ContentOrder;
  }

  public static TileLogic CreateTileLogic(Vector3Int tilePos, Vector3 worldPos, Floor floor)
  {
    TileLogic tileLogic = new TileLogic(tilePos, worldPos, floor);
    return tileLogic;
  }
}
