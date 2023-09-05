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
    [SerializeField] GameObject tempLimiter, tempAnteriorLimiter;
    [SerializeField] GameObject tempSimulate, tempAnteriorSimulate;
    Vector2 localLimiterPoint = new Vector2(0, 0);
    public Sprite referenceVoid;
    Sprite currentCellPosition;
    Sprite lastCellPosition;

    GameObject newGrid, previewGrid;

    int colStartIndex, rowStartIndex;
    bool isUnderLeftClick = false;

    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
        // _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _camera = GameObject.Find("BuilderCamera").GetComponent<Camera>();
        newGrid = GameObject.Find("NewGrid");
        previewGrid = GameObject.Find("PreviewGrid");
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
                if (go.gameObject.name.StartsWith("Simulate"))
                {
                    isPointerOverLimiter = true;
                    tempAnteriorSimulate = tempSimulate;
                    tempSimulate = go.gameObject;
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

            }*/
            if (holdActive)
            {
                HandleDrawing();
            }
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
                    // holdStartPosition = currentGridPosition;
                    // holdStartPosition =  Vector3Int.FloorToInt(tempLimiter.transform.position);
                    colStartIndex = tempLimiter.transform.GetSiblingIndex();
                    rowStartIndex = tempLimiter.transform.parent.GetSiblingIndex();
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
            

        if (tempAnteriorSimulate)
            tempAnteriorSimulate.GetComponent<Image>().sprite = tempAnteriorSimulate.GetComponent<CelulaInfo>().selecionadoSprite;

        // if (tempAnteriorLimiter)
        //     tempAnteriorLimiter.GetComponent<Image>().sprite = tempAnteriorLimiter.GetComponent<CelulaInfo>().selecionadoSprite;
   

        // Set current tile to current mouse positoins tile
        Tile tempTileBase = (Tile)tileBase;
        // currentCellPosition = tempTileBase ? tempTileBase.sprite : tempAnteriorLimiter.GetComponent<Image>().sprite;
        currentCellPosition = tempTileBase ? tempTileBase.sprite : tempAnteriorSimulate.GetComponent<Image>().sprite;
        // if (tempLimiter)
        // {
        //     tempLimiter.GetComponent<Image>().sprite = currentCellPosition;
        // }
        if (tempSimulate)
        {
            tempSimulate.GetComponent<Image>().sprite = currentCellPosition;
        }
        //previewMap.SetTile(currentGridPosition, tileBase);
    }

    private void HandleDrawing()
    {
        if (selectedObj != null)
        {
            switch (selectedObj.PlaceType)
            {
                case PlaceType.Rectangle:
                    RectanglePreviewRenderer();
                    break;
                case PlaceType.Single: 
                default:
                    DrawPreviewItem();
                    break;
            }

        }
    }

    private void HandleDrawRelease()
    {
        if (selectedObj != null)
        {

            switch(selectedObj.PlaceType)
            {
                case PlaceType.Rectangle:
                    DrawBounds();
                    // previewMap.ClearAllTiles();
                    // ClearPreviewGrid();
                    break;
                case PlaceType.Single: 
                default:
                    DrawItem();
                    break;
            }
        }
        ClearPreviewGrid();
    }

    private void RectanglePreviewRenderer()
    {
        // Render Preview on UI Map, draw real one on Release
        // previewMap.ClearAllTiles();
        ClearPreviewGrid();
        bounds.xMin = tempLimiter.transform.parent.GetSiblingIndex() < rowStartIndex ? tempLimiter.transform.parent.GetSiblingIndex() : rowStartIndex;
        bounds.xMax = tempLimiter.transform.parent.GetSiblingIndex() > rowStartIndex ? tempLimiter.transform.parent.GetSiblingIndex() : rowStartIndex;
        bounds.yMin = tempLimiter.transform.GetSiblingIndex() < colStartIndex ? tempLimiter.transform.GetSiblingIndex() : colStartIndex;
        bounds.yMax = tempLimiter.transform.GetSiblingIndex() > colStartIndex ? tempLimiter.transform.GetSiblingIndex() : colStartIndex;

        DrawPreviewBounds();
    }

    private void DrawPreviewBounds()
    {
        // Draw bounds on given map
        for (int col = bounds.xMin; col <= bounds.xMax; col++)
        {
            for (int row = bounds.yMin; row <= bounds.yMax; row++)
            {
                // if (selectedObj.name.StartsWith("Eraser"))
                //     currentCellPosition = referenceVoid;
                previewGrid.transform.GetChild(col).GetChild(row).GetComponent<CelulaInfo>().selecionadoSprite = currentCellPosition;
                previewGrid.transform.GetChild(col).GetChild(row).GetComponent<UnityEngine.UI.Image>().sprite = previewGrid.transform.GetChild(col).GetChild(row).GetComponent<CelulaInfo>().selecionadoSprite;
            }
        }
    }

    private void DrawPreviewItem() {
        // if (!selectedObj.name.StartsWith("Eraser"))
        // else
        tempSimulate.GetComponent<CelulaInfo>().selecionadoSprite = currentCellPosition;
            // tempSimulate.GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;

    }

    private void DrawBounds()
    {
        // Draw bounds on given map
        for (int col = bounds.xMin; col <= bounds.xMax; col++)
        {
            for (int row = bounds.yMin; row <= bounds.yMax; row++)
            {
                if (selectedObj.name.StartsWith("Eraser"))
                    newGrid.transform.GetChild(col).GetChild(row).GetComponent<UnityEngine.UI.Image>().sprite = referenceVoid;
                else {
                    newGrid.transform.GetChild(col).GetChild(row).GetComponent<CelulaInfo>().selecionadoSprite = currentCellPosition;
                    newGrid.transform.GetChild(col).GetChild(row).GetComponent<UnityEngine.UI.Image>().sprite = previewGrid.transform.GetChild(col).GetChild(row).GetComponent<CelulaInfo>().selecionadoSprite;
                }
            }
        }
    }

    private void DrawItem() {

        // if (map != previewMap && selectedObj.GetType() == typeof(BuildingTool)) {
        //     // it is a tool
        //     BuildingTool tool = (BuildingTool)selectedObj;

        //     tool.Use(position);

        // } else {
        //     map.SetTile(position, tileBase);
        // }

        if (!selectedObj.name.StartsWith("Eraser")) {   
            tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite = currentCellPosition;
            tempLimiter.GetComponent<UnityEngine.UI.Image>().sprite = tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite;
        }
        else {
            tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;
            tempLimiter.GetComponent<UnityEngine.UI.Image>().sprite = tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite;
        }

    }

    private void ClearPreviewGrid () {

        foreach (Transform col in previewGrid.transform) {
            foreach (Transform cel in col.transform) {
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;
                cel.GetComponent<UnityEngine.UI.Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
                // cel.GetComponent<UnityEngine.UI.Image>().sprite = data[i].selecionadoSprite;
            }
        }
    }
}   