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
    [SerializeField] string filename = "mazeData.json";

    private void Start() {
        handGear = GameObject.Find("HandGear");
        grid = GameObject.Find("NewGrid");
        onLoad();
    }

    public void onLoad() {

        List<CelulaData> data = FileHandler.ReadListFromJSON<CelulaData>(filename);

        int i = 0; 
        foreach (Transform col in grid.transform) {
            foreach (Transform cel in col.transform) {
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = Resources.Load<Sprite>("Sprites" + Path.DirectorySeparatorChar + data[i].nomeSelecionadoSprite);
                cel.GetComponent<UnityEngine.UI.Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
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
                else if (data[i].nomeSelecionadoSprite.StartsWith("static")) 
                {
                    cel.gameObject.AddComponent<Coin>();
                    cel.gameObject.AddComponent<Rigidbody2D>();
                    cel.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    cel.gameObject.AddComponent<BoxCollider2D>();
                    cel.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    cel.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(29, 29);
                }
                //else if (data[i].selecionadoSprite.ToString().StartsWith("start")) {
                else if (data[i].nomeSelecionadoSprite.StartsWith("start"))
                {
                    handGear.transform.position = cel.gameObject.transform.position;
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
}