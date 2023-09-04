using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class SaveHandler : Singleton<SaveHandler> {
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    Dictionary<TileBase, BuildingObjectBase> tileBaseToBuildingObject = new Dictionary<TileBase, BuildingObjectBase>();
    Dictionary<string, TileBase> guidToTileBase = new Dictionary<string, TileBase>();
    [SerializeField] Tilemap previewMap;

    [SerializeField] BoundsInt bounds;
    [SerializeField] GameObject moeda;
    [SerializeField] GameObject grid;
    [SerializeField] string filename = "tilemapData.json";

    private void Start() {
        // InitTilemaps();
        // InitTileReferences();
        grid = GameObject.Find("NewGrid");
    }

    private void InitTileReferences() {
        BuildingObjectBase[] buildables = Resources.LoadAll<BuildingObjectBase>("Scriptables/Buildables");

        foreach (BuildingObjectBase buildable in buildables) {
            if (!tileBaseToBuildingObject.ContainsKey(buildable.TileBase)) {
                tileBaseToBuildingObject.Add(buildable.TileBase, buildable);
                guidToTileBase.Add(buildable.name, buildable.TileBase);
            } else {
                Debug.LogError("TileBase " + buildable.TileBase.name + " is already in use by " + tileBaseToBuildingObject[buildable.TileBase].name);
            }
        }
    }

    private void InitTilemaps() {
        // pega todos tilemaps da cena e escreve no discionário
        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        foreach (var map in maps) {
            tilemaps.Add(map.name, map);
        }
    }

    public void onSave() {
        // Lista que será salva posteriormente
        // List<TilemapData> data = new List<TilemapData>();

        List<CelulaData> data = new List<CelulaData>();

        // para cada mapa
        // foreach (var mapObj in tilemaps) {
        //     TilemapData mapData = new TilemapData();
        //     mapData.key = mapObj.Key;

        foreach (Transform col in grid.transform) {
            foreach (Transform cel in col.transform) {
                CelulaData celData = new CelulaData();
                celData.position = cel.position;
                celData.selecionadoSprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
                data.Add(celData);
            }
        }

            // alternativa: usar mapObj.Value.cellBounds
            // https://docs.unity3d.com/ScriptReference/Tilemaps.Tilemap-cellBounds.html

            // BoundsInt boundsForThisMap = mapObj.Value.cellBounds;

            // for (int x = boundsForThisMap.xMin; x < boundsForThisMap.xMax; x++) {
            //     for (int y = boundsForThisMap.yMin; y < boundsForThisMap.yMax; y++) {
            //         Vector3Int pos = new Vector3Int(x, y, 0);
            //         TileBase tile = mapObj.Value.GetTile(pos);

            //         if (tile != null && tileBaseToBuildingObject.ContainsKey(tile)) {
            //             String guid = tileBaseToBuildingObject[tile].name;
            //             TileInfo ti = new TileInfo(pos, guid);
            //             mapData.tiles.Add(ti);
            //         }
            //     }
            // }

            // data.Add(mapData);
        // }
        // FileHandler.SaveToJSON<TilemapData>(data, filename);
        FileHandler.SaveToJSON<CelulaData>(data, filename);
    }

    public void onLoad() {
        // List<TilemapData> data = FileHandler.ReadListFromJSON<TilemapData>(filename);
        List<CelulaData> data = FileHandler.ReadListFromJSON<CelulaData>(filename);

        // foreach (var mapData in data) {
        //     if (!tilemaps.ContainsKey(mapData.key)) {
        //         Debug.LogError("Found saved data for tilemap called '" + mapData.key + "', but Tilemap does not exist in scene.");
        //         continue;
        //     }

        //     // pega o mapa correspondente
        //     var map = tilemaps[mapData.key];

        //     // limpa mapa
        //     map.ClearAllTiles();

        //     if (mapData.tiles != null && mapData.tiles.Count > 0) {
        //         foreach (var tile in mapData.tiles) {

        //             if (guidToTileBase.ContainsKey(tile.guidForBuildable)) {
        //                 map.SetTile(tile.position, guidToTileBase[tile.guidForBuildable]);
                        
        //             } else {
        //                 Debug.LogError("Reference " + tile.guidForBuildable + " could not be found.");
        //             }

        //         }
        //     }
        // }
        // previewMap.ClearAllTiles();


// TODO: FAZER O LOAD FUNCIONAR
        foreach (Transform col in grid.transform) {
            foreach (Transform cel in col.transform) {
                for (int i = 0; i < data.Count; i++) {
                    cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = data[i].selecionadoSprite;
                    Debug.Log(cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite);
                    Debug.Log(data[i].selecionadoSprite);
                    break;
                }
            }
        }


            // if (celData.tiles != null && celData.tiles.Count > 0) {
            //     foreach (var tile in celData.tiles) {

            //         if (guidToTileBase.ContainsKey(tile.guidForBuildable)) {
            //             map.SetTile(tile.position, guidToTileBase[tile.guidForBuildable]);
                        
            //         } else {
            //             Debug.LogError("Reference " + tile.guidForBuildable + " could not be found.");
            //         }

            //     }
            // }
        // }
    }
}


[Serializable]
public class TilemapData {
    public string key; // the key of your dictionary for the tilemap - here: the name of the map in the hierarchy
    public List<TileInfo> tiles = new List<TileInfo>();
}

[Serializable]
public class TileInfo {
    public string guidForBuildable;
    public Vector3Int position;

    public TileInfo(Vector3Int pos, string guid) {
        position = pos;
        guidForBuildable = guid;
    }
}

[Serializable]
public class CelulaData {
    public Sprite selecionadoSprite;
    public Vector3 position;

    // public CelulaData(Sprite sprite, Vector3 pos) {
    //     selecionadoSprite = sprite;
    //     position = pos;
    // }
}