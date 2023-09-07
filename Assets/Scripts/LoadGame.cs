using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic; 
using System.Linq;
using System.IO;

public class LoadGame : Singleton<SaveHandler> {
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    Dictionary<TileBase, BuildingObjectBase> tileBaseToBuildingObject = new Dictionary<TileBase, BuildingObjectBase>();
    Dictionary<string, TileBase> guidToTileBase = new Dictionary<string, TileBase>();

    [SerializeField] BoundsInt bounds;
    [SerializeField] GameObject ParedeCinza;
    [SerializeField] GameObject ParedeAmarela;
    [SerializeField] GameObject Inicio;
    [SerializeField] GameObject CheckMark;
    [SerializeField] GameObject Fim;
    [SerializeField] GameObject MoedaAmarela;
    [SerializeField] GameObject MoedaVerde;
    [SerializeField] GameObject MoedaAzul;
    [SerializeField] GameObject handGear;
    [SerializeField] GameObject grid;
    [SerializeField] string filename = "tilemapData.json";

    private void Start() {
        // InitTilemaps();
        // Debug.Log("Starting Game");
        // InitTileReferences();
        handGear = GameObject.Find("HandGear");
        grid = GameObject.Find("NewGrid");
        onLoad();
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
        List<TilemapData> data = new List<TilemapData>();

        // para cada mapa
        foreach (var mapObj in tilemaps) {
            TilemapData mapData = new TilemapData();
            mapData.key = mapObj.Key;

            // alternativa: usar mapObj.Value.cellBounds
            // https://docs.unity3d.com/ScriptReference/Tilemaps.Tilemap-cellBounds.html

            BoundsInt boundsForThisMap = mapObj.Value.cellBounds;

            for (int x = boundsForThisMap.xMin; x < boundsForThisMap.xMax; x++) {
                for (int y = boundsForThisMap.yMin; y < boundsForThisMap.yMax; y++) {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase tile = mapObj.Value.GetTile(pos);

                    if (tile != null && tileBaseToBuildingObject.ContainsKey(tile)) {
                        String guid = tileBaseToBuildingObject[tile].name;
                        TileInfo ti = new TileInfo(pos, guid);
                        mapData.tiles.Add(ti);
                    }
                }
            }

            data.Add(mapData);
        }
        FileHandler.SaveToJSON<TilemapData>(data, filename);
    }

    public void onLoad() {
        // List<TilemapData> data = FileHandler.ReadListFromJSON<TilemapData>(filename);

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
        //             Debug.Log(tile);

        //             if (guidToTileBase.ContainsKey(tile.guidForBuildable)) {
        //                 map.SetTile(tile.position, guidToTileBase[tile.guidForBuildable]);
        //                 // Debug.Log("Tipo de Tile: "+guidToTileBase[tile.guidForBuildable].ToString());
        //                 switch (guidToTileBase[tile.guidForBuildable].ToString())
        //                 {
        //                     // Gera os GameObjects na posição dos tiles no tilemap
        //                     case "Moeda (UnityEngine.Tilemaps.AnimatedTile)":
        //                         GenerateTilemapElementsFromFile(MoedaAmarela, tile.position);
        //                         break;
        //                     case "MoedaVerde (UnityEngine.Tilemaps.AnimatedTile)":
        //                         GenerateTilemapElementsFromFile(MoedaVerde, tile.position);
        //                         break;
        //                     case "MoedaAzul (UnityEngine.Tilemaps.AnimatedTile)":
        //                         GenerateTilemapElementsFromFile(MoedaAzul, tile.position);
        //                         break;
        //                     case "Tiles_0 (UnityEngine.Tilemaps.Tile)":
        //                         GenerateTilemapElementsFromFile(ParedeCinza, tile.position);
        //                         break;
        //                     case "Tiles_2 (UnityEngine.Tilemaps.Tile)":
        //                         GenerateTilemapElementsFromFile(ParedeAmarela, tile.position);
        //                         break;
        //                     case "Start (UnityEngine.Tilemaps.Tile)":
        //                         GenerateTilemapElementsFromFile(Inicio, tile.position);
        //                         break;
        //                     case "Finish (UnityEngine.Tilemaps.Tile)":
        //                         GenerateTilemapElementsFromFile(Fim, tile.position);
        //                         break;
        //                 }
        //             } else {
        //                 Debug.LogError("Reference " + tile.guidForBuildable + " could not be found.");
        //             }

        //         }
        //     }
        // }
        List<CelulaData> data = FileHandler.ReadListFromJSON<CelulaData>(filename);

        int i = 0; 
        foreach (Transform col in grid.transform) {
            foreach (Transform cel in col.transform) {
                //cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = data[i].selecionadoSprite;
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = Resources.Load<Sprite>("Sprites" + Path.DirectorySeparatorChar + data[i].nomeSelecionadoSprite);
                cel.GetComponent<UnityEngine.UI.Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
                //if (data[i].selecionadoSprite.ToString().StartsWith("Tiles")) {
                if (data[i].nomeSelecionadoSprite.StartsWith("Tiles"))
                {
                    cel.gameObject.AddComponent<BlocoImpeditivo>();
                    cel.gameObject.AddComponent<Rigidbody2D>();
                    cel.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    cel.gameObject.AddComponent<BoxCollider2D>();
                    cel.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(29, 29);
                    cel.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                //else if (data[i].selecionadoSprite.ToString().StartsWith("moeda")) {}
                else if (data[i].nomeSelecionadoSprite.StartsWith("moeda")) { }
                //else if (data[i].selecionadoSprite.ToString().StartsWith("start")) {
                else if (data[i].nomeSelecionadoSprite.StartsWith("start"))
                {
                    // handGear.transform.position = cel.gameObject.transform.position;
                    // element.transform.parent = GameObject.Find("Itens").transform;

                }
                //else if (data[i].selecionadoSprite.ToString().StartsWith("fim")) { }
                else if (data[i].nomeSelecionadoSprite.StartsWith("fim")) {}
                // else if (data[i].selecionadoSprite.ToString().StartsWith("check")) {
                //     cel.gameObject.AddComponent<CheckPoint>();
                // }
                // else if (data[i].selecionadoSprite.ToString().StartsWith("vazio")) {}
                // else {
                //     Debug.Log("Sprite não reconhecido: "+data[i].selecionadoSprite.ToString()

                // }
                i++;
            }
        }

    }

    // Instancia os elementos do jogo definidos no Tilemap como GameObjects na tela
    public void GenerateTilemapElementsFromFile(GameObject tile, Vector3Int position) {
        var element = Instantiate(tile, position, Quaternion.identity) as GameObject;
        element.transform.parent = GameObject.Find("GameScreen").transform;
    }
}


// [Serializable]
// public class TilemapData {
//     public string key; // the key of your dictionary for the tilemap - here: the name of the map in the hierarchy
//     public List<TileInfo> tiles = new List<TileInfo>();
// }

// [Serializable]
// public class TileInfo {
//     public string guidForBuildable;
//     public Vector3Int position;

//     public TileInfo(Vector3Int pos, string guid) {
//         position = pos;
//         guidForBuildable = guid;
//     }
// }