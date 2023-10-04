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
    [SerializeField] GameObject handGear;
    [SerializeField] GameObject grid;
    [SerializeField] string filename = "mazeData.json";

    private void OnEnable() {
        // handGear = GameObject.Find("HandGear");
        if (VariaveisGlobais.partidaCorrente == -1) {
            filename = "mazeData.json";
        } else {
            filename = "level" + (VariaveisGlobais.partidaCorrente + 1).ToString() + ".json";
        }
        grid = GameObject.Find("NewGrid");
        // Chama a função onLoad() após 0.3 segundos
        Invoke("onLoad", 0.3f);
    }

    private void OnDisable() {

        foreach (Transform col in grid.transform) {
            foreach (Transform cel in col.transform) {

                // Define a sprite para vazioBloco
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = Resources.Load<Sprite>("Sprites" + Path.DirectorySeparatorChar + "vazioBloco");
                cel.GetComponent<UnityEngine.UI.Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;

                // Remove scripts e componentes específicos (exceto CelulaInfo e Image)
                Component[] components = cel.gameObject.GetComponents<Component>();
                foreach (Component component in components)
                {
                    // Verifica se o componente é um script que não deve ser removido
                    if (component is MonoBehaviour && component.GetType() != typeof(CelulaInfo) && !(component is UnityEngine.UI.Image))
                    {
                        Destroy(component);
                    }

                    // Remove componentes específicos (Rigidbody2D e BoxCollider2D)
                    if (component is Rigidbody2D || component is BoxCollider2D)
                    {
                        Destroy(component);
                    }
                }
            }
        }
    }

    public void onLoad() {

        List<CelulaData> data = FileHandler.ReadListFromJSON<CelulaData>(filename);
        Debug.Log(grid);
        int i = 0; 
        foreach (Transform col in grid.transform) {
            foreach (Transform cel in col.transform) {
                Debug.Log(cel);
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = Resources.Load<Sprite>("Sprites" + Path.DirectorySeparatorChar + data[i].nomeSelecionadoSprite);
                cel.GetComponent<UnityEngine.UI.Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
                if (data[i].nomeSelecionadoSprite.StartsWith("Tiles"))
                {
                    cel.gameObject.AddComponent<BlocoImpeditivo>();
                    cel.gameObject.AddComponent<Rigidbody2D>();
                    cel.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    cel.gameObject.AddComponent<BoxCollider2D>();
                    cel.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(57, 57);
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
                    cel.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(57, 57);
                }
                //else if (data[i].selecionadoSprite.ToString().StartsWith("start")) {
                else if (data[i].nomeSelecionadoSprite.StartsWith("start"))
                {
                    cel.gameObject.tag = "Start";
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
}