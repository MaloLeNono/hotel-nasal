using UnityEngine;
using UnityEngine.Tilemaps;

public class DanceFloor : MonoBehaviour
{
   [SerializeField] private Vector2Int leftFloorTopLeft;
   [SerializeField] private Vector2Int leftFloorBottomRight;
   [SerializeField] private Vector2Int rightFloorTopLeft;
   [SerializeField] private Vector2Int rightFloorBottomRight;
   [SerializeField] private Tilemap tilemap;
   
   private void Update()
   {
      ColorRegion(leftFloorTopLeft, leftFloorBottomRight);
      ColorRegion(rightFloorTopLeft, rightFloorBottomRight);
   }

   private void ColorRegion(Vector2Int topLeft, Vector2Int bottomRight)
   {
      for (int x = topLeft.x; x <= bottomRight.x; x++)
      {
         for (int y = topLeft.y; y >= bottomRight.y; y--)
         {
            float wave = Vector2.Distance(new Vector2(x, y), new Vector2((topLeft.x + bottomRight.x) / 2f, (topLeft.y + bottomRight.y) / 2f));
            float hue = (Time.time * 0.5f - wave * 0.05f) % 1f;
            Color color = Color.HSVToRGB(hue, 1f, 1f);

            tilemap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
            tilemap.SetColor(new Vector3Int(x, y, 0), color);
         }
      }
   }
}