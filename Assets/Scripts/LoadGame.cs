using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic; 
using System.Linq;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

public class LoadGame : Singleton<SaveHandler> {
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    Dictionary<TileBase, BuildingObjectBase> tileBaseToBuildingObject = new Dictionary<TileBase, BuildingObjectBase>();
    Dictionary<string, TileBase> guidToTileBase = new Dictionary<string, TileBase>();

    [SerializeField] BoundsInt bounds;
    [SerializeField] GameObject handGear;
    [SerializeField] GameObject grid;
    [SerializeField] string filename;

    private void OnEnable() {
        if (VariaveisGlobais.partidaCorrente != 2001)
        {
            if (VariaveisGlobais.partidaCorrente == 0)
            {
                filename = "mazeData.json";
            } else if (VariaveisGlobais.partidaCorrente < 0) 
            {
                filename = "calibration" + (VariaveisGlobais.partidaCorrente * -1).ToString() + ".json";
            } else 
            {
                if (VariaveisGlobais.partidaCorrente >= 21)
                {
                    filename = "noadaption" + (VariaveisGlobais.partidaCorrente - 20).ToString() + ".json";
                }
                else 
                {
                    filename = "level" + VariaveisGlobais.partidaCorrente.ToString() + ".json";
                }
            }
            StartCoroutine(onLoad());
            Debug.Log("Carregando partida " + filename); 
        }
    }

    private void OnDisable() {
        handGear.SetActive(false);

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

    public IEnumerator onLoad() {

#if ((UNITY_WEBGL) && (!UNITY_EDITOR))
        if (!File.Exists(Path.Combine(Application.persistentDataPath, filename)))
        {
            string relativeURL;

            if ((Application.absoluteURL.IndexOf("index.htm") == -1))
                relativeURL = Application.absoluteURL;
            else relativeURL = Application.absoluteURL.Remove(Application.absoluteURL.IndexOf("index.htm"));

            using (UnityWebRequest uwr = new UnityWebRequest(System.String.Concat(relativeURL, "StreamingAssets/", filename), UnityWebRequest.kHttpVerbGET))
            {
                uwr.timeout = 30;
                uwr.downloadHandler = new DownloadHandlerFile(Path.Combine(Application.persistentDataPath, filename));

                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError || uwr.error != null)
                    Debug.LogError(uwr.error);
                else
                    Debug.Log("Arquivo baixado para " + Path.Combine(Application.persistentDataPath, filename));
                yield return new WaitForEndOfFrame();
            }
        }
#endif


        int totalMoedasNaPartida = 0;
        List<CelulaData> data = FileHandler.ReadListFromJSON<CelulaData>(filename);
        int i = 0; 
        foreach (Transform col in grid.transform) {
            foreach (Transform cel in col.transform) {
                // Limpa informações anteriores
                cel.SetLocalPositionAndRotation(cel.localPosition, new Quaternion(0, 0, 0, 0));
                cel.gameObject.tag = "Untagged";

                if (cel.gameObject.TryGetComponent<BlocoImpeditivo>(out BlocoImpeditivo bloco))
                    Destroy(bloco);
                if (cel.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid))
                    Destroy(rigid);
                if (cel.gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D box))
                    Destroy(box);
                if (cel.gameObject.TryGetComponent<Coin>(out Coin coin))
                    Destroy(coin);

                // Povoa com o que precisa
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
                else if (data[i].nomeSelecionadoSprite.StartsWith("static")) 
                {
                    cel.gameObject.AddComponent<Coin>();
                    cel.gameObject.AddComponent<Rigidbody2D>();
                    cel.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    cel.gameObject.AddComponent<BoxCollider2D>();
                    cel.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    cel.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(57, 57);
                    totalMoedasNaPartida++;
                }
                else if (data[i].nomeSelecionadoSprite.StartsWith("start"))
                {
                    cel.gameObject.tag = "Start";
                    handGear.transform.position = cel.transform.position;
                    handGear.SetActive(true);
                }
                else if (data[i].nomeSelecionadoSprite.StartsWith("finish")) 
                {
                    cel.gameObject.AddComponent<FinalizouPartida>();
                    cel.gameObject.AddComponent<Rigidbody2D>();
                    cel.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    cel.gameObject.AddComponent<BoxCollider2D>();
                    cel.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(57, 57);
                    cel.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                }
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
        VariaveisGlobais.totalMoedasNaPartida = totalMoedasNaPartida;

        yield return new WaitForEndOfFrame();
    }
}