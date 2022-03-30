using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
  private TilemapRenderer tilemapRenderer;

  public int contentOrder;

  public int FloorOrder { get => tilemapRenderer.sortingOrder; }

  void Awake()
  {
    tilemapRenderer = GetComponent<TilemapRenderer>();
  }
}
