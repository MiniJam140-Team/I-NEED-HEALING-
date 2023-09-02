using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapVisualizer : MonoBehaviour
{
   [SerializeField]private Tilemap floorTilemap;

   [SerializeField]private List<TileBase> possibleFloorTile;
   

   public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
   {
       PaintTiles(floorPositions, floorTilemap, possibleFloorTile);
   }

   private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, List<TileBase> tile)
   {
       foreach (var position in positions)
       {
           TileBase selectedTile = tile[Random.Range(0, tile.Count)];
           PaintSingleTile(tilemap,selectedTile,position);  
       }
   }
  private void PaintSingleTile(Tilemap tilemap,TileBase tile,Vector2Int position)
  {
    var tilePosition = tilemap.WorldToCell((Vector3Int)position);
    tilemap.SetTile(tilePosition, tile);
  }

  public void ClearMap()
  {
      floorTilemap.ClearAllTiles();
  }

}
