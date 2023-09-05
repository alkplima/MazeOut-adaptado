using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class BuildingCreator : Singleton<BuildingCreator>
{
    [SerializeField] Tilemap previewMap, defaultMap;
    PlayerInput playerInput;

    TileBase tileBase;
    BuildingObjectBase selectedObj;

    Camera _camera;

    Vector2 mousePos;
    Vector3Int currentGridPosition;
    Vector3Int lastGridPosition;
    
    bool holdActive;
    Vector3Int holdStartPosition;

    BoundsInt bounds;

    bool isPointerOverGameObject = false;
    bool isPointerOverLimiter = false;

    // Vari�veis de teste 
    GameObject tempLimiter, tempAnteriorLimiter;
    Vector2 localLimiterPoint = new Vector2(0, 0);
    public Sprite referenceVoid;
    Sprite currentCellPosition;
    Sprite lastCellPosition;

    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
        // _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _camera = GameObject.Find("BuilderCamera").GetComponent<Camera>();
    }

    private void OnEnable()
    {
        playerInput.Enable();

        playerInput.Gameplay.MousePosition.performed += OnMouseMove;

        playerInput.Gameplay.MouseLeftClick.performed += OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.started += OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.canceled += OnLeftClick;

        playerInput.Gameplay.MouseRightClick.performed += OnRightClick;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Gameplay.MousePosition.performed -= OnMouseMove;

        playerInput.Gameplay.MouseLeftClick.performed -= OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.started -= OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.canceled -= OnLeftClick;

        playerInput.Gameplay.MouseRightClick.performed -= OnRightClick;
    }

    private BuildingObjectBase SelectedObj 
    {
        set 
        {
            selectedObj = value;

            tileBase = selectedObj != null ? selectedObj.TileBase : null;

            UpdatePreview();
        }
    }

    private Tilemap tilemap
    {
        get
        {
            if (selectedObj != null && selectedObj.Category != null && selectedObj.Category.Tilemap != null)
            {
                return selectedObj.Category.Tilemap;
            }

            return defaultMap;
        }
    }

    private void Update()
    {
        isPointerOverLimiter = false;

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if(raycastResults.Count > 0)
        {
            foreach(var go in raycastResults)
            {  
                //if (go.gameObject.name == "Limiter")
                if (go.gameObject.name.StartsWith("Cell"))
                {
                    isPointerOverLimiter = true;
                    tempAnteriorLimiter = tempLimiter;
                    tempLimiter = go.gameObject;
                }
                //else tempLimiter = null;
            }
        }
        // isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
        // isPointerEnterHandler = EventSystem.IPointerEnterHandler();
        // If something is selected - show preview
        if (selectedObj != null)
        {
            //Vector3 pos = _camera.ScreenToWorldPoint(mousePos);

            //Vector3Int gridPos = previewMap.WorldToCell(pos);
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(tempLimiter.GetComponent<RectTransform>(), mousePos, _camera, out localLimiterPoint);
            //Vector3Int gridPos = previewMap.LocalToCell(localLimiterPoint);

            UpdatePreview();
            /*if (gridPos != currentGridPosition && isPointerOverLimiter)
            {
                lastGridPosition = currentGridPosition;
                currentGridPosition = gridPos;

                UpdatePreview();

                if (holdActive)
                {
                    HandleDrawing();
                }
            }*/
        }
    }

    private void OnMouseMove(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }

    private void OnLeftClick (InputAction.CallbackContext ctx) 
    {
        // if (selectedObj != null && !isPointerOverGameObject) 
        if (selectedObj != null && isPointerOverLimiter) 
        {
            if (ctx.phase == InputActionPhase.Started) 
            {
                holdActive = true;

                if (ctx.interaction is TapInteraction) 
                {
                    holdStartPosition = currentGridPosition;
                }
                HandleDrawing ();
            } 
            else 
            {
                if (ctx.interaction is SlowTapInteraction || ctx.interaction is TapInteraction && ctx.phase == InputActionPhase.Performed) 
                {
                    holdActive = false;
                    HandleDrawRelease();
                }
            }
        }
    }

    private void OnRightClick(InputAction.CallbackContext ctx)
    {
        SelectedObj = null;
    }

    public void ObjectSelected(BuildingObjectBase obj)
    {
        SelectedObj = obj;

        // Set preview where mouse is
        // on click draw
        // on right click cancel drawing
    }

    private void UpdatePreview()
    {
        // Remove old tile if existing
        //previewMap.SetTile(lastGridPosition, null);

        //lastCellPosition = referenceVoid;
        if (tempAnteriorLimiter)
            tempAnteriorLimiter.GetComponent<Image>().sprite = tempAnteriorLimiter.GetComponent<CelulaInfo>().selecionadoSprite;
   

        // Set current tile to current mouse positoins tile
        Tile tempTileBase = (Tile)tileBase;
        currentCellPosition = tempTileBase ? tempTileBase.sprite : tempAnteriorLimiter.GetComponent<Image>().sprite;
        if (tempLimiter)
        {
            tempLimiter.GetComponent<Image>().sprite = currentCellPosition;
        }
            
        //previewMap.SetTile(currentGridPosition, tileBase);
    }

    private void HandleDrawing()
    {
        if (selectedObj != null)
        {
            if (!selectedObj.name.StartsWith("Eraser"))
                tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite = currentCellPosition;
            else
                tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;

/*            switch (selectedObj.PlaceType)
            {
                case PlaceType.Line:
                    LineRenderer();
                    break;
                case PlaceType.Rectangle:
                    RectangleRenderer();
                    break;
            }
*/
        }
    }

    private void HandleDrawRelease()
    {
        if (selectedObj != null)
        {
            if (!selectedObj.name.StartsWith("Eraser"))
                tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite = currentCellPosition;
            else
                tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;

/*            switch(selectedObj.PlaceType)
            {
                case PlaceType.Line:
                case PlaceType.Rectangle:
                    DrawBounds(tilemap);
                    previewMap.ClearAllTiles();
                    break;
                case PlaceType.Single: 
                default:
                    DrawItem(tilemap, currentGridPosition, tileBase);
                    break;
            }
*/
        }
    }

    private void RectangleRenderer()
    {
        // Render Preview on UI Map, draw real one on Release
        previewMap.ClearAllTiles();

        bounds.xMin = currentGridPosition.x < holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
        bounds.xMax = currentGridPosition.x > holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
        bounds.yMin = currentGridPosition.y < holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;
        bounds.yMax = currentGridPosition.y > holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;

        DrawBounds(previewMap);
    }

    private void LineRenderer() 
    {
        //  Render Preview on UI Map, draw real one on Release
        previewMap.ClearAllTiles ();

        float diffX = Mathf.Abs (currentGridPosition.x - holdStartPosition.x);
        float diffY = Mathf.Abs (currentGridPosition.y - holdStartPosition.y);

        bool lineIsHorizontal = diffX >= diffY;

        if (lineIsHorizontal) 
        {
            bounds.xMin = currentGridPosition.x < holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
            bounds.xMax = currentGridPosition.x > holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
            bounds.yMin = holdStartPosition.y;
            bounds.yMax = holdStartPosition.y;
        } 
        else 
        {
            bounds.xMin = holdStartPosition.x;
            bounds.xMax = holdStartPosition.x;
            bounds.yMin = currentGridPosition.y < holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;
            bounds.yMax = currentGridPosition.y > holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;
        }

        DrawBounds(previewMap);
    }

    private void DrawBounds(Tilemap map)
    {
        // Draw bounds on given map
        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                DrawItem(map, new Vector3Int(x, y, 0), tileBase);
            }
        }
    }

    private void DrawItem(Tilemap map, Vector3Int position, TileBase tileBase) {

        if (map != previewMap && selectedObj.GetType() == typeof(BuildingTool)) {
            // it is a tool
            BuildingTool tool = (BuildingTool)selectedObj;

            tool.Use(position);

        } else {
            map.SetTile(position, tileBase);
        }

    }
}   