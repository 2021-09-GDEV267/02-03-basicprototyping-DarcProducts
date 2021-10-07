using UnityEngine;

public enum TileType { None, Gravel, Road, Water, Log, Grass, Other }

public class LevelTile : MonoBehaviour
{
    [SerializeField] TileType tileType;

    public TileType GetTileType() => tileType;
}