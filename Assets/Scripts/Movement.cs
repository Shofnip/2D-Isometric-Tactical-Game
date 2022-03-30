using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] private bool test;
  [SerializeField] private Vector3Int destinationPos;

  private SpriteRenderer spriteRenderer;
  private Transform jumper;
  private TileLogic currentTile;

  void Awake()
  {
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    jumper = transform.Find("Jumper");
  }

  void Update()
  {
    if (!test) return;

    test = false;
    StopAllCoroutines();
    StartCoroutine(Move());
  }

  IEnumerator Move()
  {
    TileLogic destinationTile = Board.instance.tiles[destinationPos];

    Vector3 startPos = transform.position;
    Vector3 endPos = destinationTile.WorldPos;
    float movementDuration = 1f;
    float time = 0;

    if (currentTile == null) currentTile = destinationTile;

    if (currentTile.Floor != destinationTile.Floor) {
      StartCoroutine(Jump(destinationTile, movementDuration));
      currentTile = destinationTile;
    }

    while(transform.position != endPos) {
      time += Time.deltaTime;
      float percTime = time / movementDuration;
      transform.position = Vector3.Lerp(startPos, endPos, percTime);
      yield return null;
    }

    spriteRenderer.sortingOrder = destinationTile.ContentOrder;
    destinationTile.Content = gameObject;
  }

  IEnumerator Jump(TileLogic destinationTile, float movementDuration) {
    yield return null;
    Vector3 halfwayPos = jumper.localPosition;
    Vector3 startPos = halfwayPos;
    halfwayPos.y += 0.5f;
    float time = 0;

    float currentTileAnchorY = currentTile.Floor.tilemap.tileAnchor.y;
    float destinationTileAnchorY = destinationTile.Floor.tilemap.tileAnchor.y;

    if (currentTileAnchorY < destinationTileAnchorY) {
      spriteRenderer.sortingOrder = destinationTile.ContentOrder;
    }

    while (jumper.localPosition != halfwayPos) {
      time += Time.deltaTime;
      float percTime = time / (movementDuration / 2);
      jumper.localPosition = Vector3.Lerp(startPos, halfwayPos, percTime);
      yield return null;
    }

    time = 0;

    while (jumper.localPosition != startPos) {
      time += Time.deltaTime;
      float percTime = time / (movementDuration / 2);
      jumper.localPosition = Vector3.Lerp(halfwayPos, startPos, percTime);
      yield return null;
    }
  }
}
