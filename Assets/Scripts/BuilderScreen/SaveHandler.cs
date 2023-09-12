using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.IO;

public class SaveHandler : Singleton<SaveHandler> {
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    Dictionary<TileBase, BuildingObjectBase> tileBaseToBuildingObject = new Dictionary<TileBase, BuildingObjectBase>();
    Dictionary<string, TileBase> guidToTileBase = new Dictionary<string, TileBase>();
    [SerializeField] Tilemap previewMap;

    [SerializeField] BoundsInt bounds;
    [SerializeField] GameObject moeda;
    public GameObject grid;
    internal string filename = "mazeData.json";

    public void onSave() {
        // Lista que será salva posteriormente
        List<CelulaData> data = new List<CelulaData>();


        foreach (Transform col in grid.transform) {
            foreach (Transform cel in col.transform) {
                CelulaData celData = new CelulaData();
                celData.position = cel.position;
                celData.selecionadoSprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
                celData.nomeSelecionadoSprite = celData.selecionadoSprite.name;
                data.Add(celData);
            }
        }

        FileHandler.SaveToJSON<CelulaData>(data, filename);
    }

    public void onLoad() {
        List<CelulaData> data = FileHandler.ReadListFromJSON<CelulaData>(filename);
        int i = 0;

        foreach (Transform col in grid.transform) {
            foreach (Transform cel in col.transform) {
                Debug.Log(data[i].nomeSelecionadoSprite);
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = Resources.Load<Sprite>("Sprites" + Path.DirectorySeparatorChar + data[i].nomeSelecionadoSprite);
                cel.GetComponent<Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
                i++;
            }
        }
    }
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
    public String nomeSelecionadoSprite;
}