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

  public TileLogic() { }

  public TileLogic(Vector3Int tilePos, Vector3 worldPos, Floor floor)
  {
    _tilePos = tilePos;
    _worldPos = worldPos;
    _floor = floor;
    _contentOrder = floor.contentOrder;
  }

  public static TileLogic CreateTileLogic(Vector3Int tilePos, Vector3 worldPos, Floor floor)
  {
    TileLogic tileLogic = new TileLogic(tilePos, worldPos, floor);
    return tileLogic;
  }
}
