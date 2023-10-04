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
    PlayerInput playerInput;

    TileBase tileBase;
    BuildingObjectBase selectedObj;

    Camera _camera;

    Vector2 mousePos;
    
    bool holdActive;

    BoundsInt bounds;
    bool isPointerOverLimiter = false;

    // Vari�veis de teste 
    [SerializeField] GameObject tempLimiter, tempAnteriorLimiter;
    [SerializeField] GameObject tempSimulate, tempAnteriorSimulate;
    public Sprite referenceVoid;
    Sprite currentCellPosition;
    Sprite lastCellPosition;

    public GameObject newGrid, previewGrid;

    int colStartIndex, rowStartIndex;

    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
        // _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _camera = GameObject.Find("BuilderCamera").GetComponent<Camera>();
        //newGrid = GameObject.Find("NewGrid");
        //previewGrid = GameObject.Find("PreviewGrid");
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
            }
        }

        if (selectedObj != null)
        {
            UpdatePreview();

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
        if (selectedObj != null && isPointerOverLimiter) 
        {
            if (ctx.phase == InputActionPhase.Started) 
            {
                holdActive = true;

                if (ctx.interaction is TapInteraction) 
                {
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

    public void OnButtonOutsideAreaClick()
    {
        SelectedObj = null;
    }

    public void ObjectSelected(BuildingObjectBase obj)
    {
        SelectedObj = obj;
    }

    private void UpdatePreview()
    {
        if (tempAnteriorSimulate)
            tempAnteriorSimulate.GetComponent<Image>().sprite = tempAnteriorSimulate.GetComponent<CelulaInfo>().selecionadoSprite;

        // Set current tile to current mouse position's tile
        Tile tempTileBase = (Tile)tileBase;
        currentCellPosition = tempTileBase ? tempTileBase.sprite : tempAnteriorSimulate.GetComponent<Image>().sprite;

        if (tempSimulate)
        {
            tempSimulate.GetComponent<Image>().sprite = currentCellPosition;
        }
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
        tempSimulate.GetComponent<CelulaInfo>().selecionadoSprite = currentCellPosition;
    }

    private void DrawBounds()
    {
        // Draw bounds on given map
        for (int col = bounds.xMin; col <= bounds.xMax; col++)
        {
            for (int row = bounds.yMin; row <= bounds.yMax; row++)
            {
                if (selectedObj.name.StartsWith("Eraser")) {
                    newGrid.transform.GetChild(col).GetChild(row).GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;
                    newGrid.transform.GetChild(col).GetChild(row).GetComponent<UnityEngine.UI.Image>().sprite = referenceVoid;
                }
                else {
                    newGrid.transform.GetChild(col).GetChild(row).GetComponent<CelulaInfo>().selecionadoSprite = currentCellPosition;
                    newGrid.transform.GetChild(col).GetChild(row).GetComponent<UnityEngine.UI.Image>().sprite = previewGrid.transform.GetChild(col).GetChild(row).GetComponent<CelulaInfo>().selecionadoSprite;
                }
            }
        }
    }

    private void DrawItem() {

        if (!selectedObj.name.StartsWith("Eraser")) {   
            tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite = currentCellPosition;
            tempLimiter.GetComponent<UnityEngine.UI.Image>().sprite = tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite;
        }
        else {
            tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;
            tempLimiter.GetComponent<UnityEngine.UI.Image>().sprite = tempLimiter.GetComponent<CelulaInfo>().selecionadoSprite;
        }

    }

    public void ClearPreviewGrid () {

        foreach (Transform col in previewGrid.transform) {
            foreach (Transform cel in col.transform) {
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;
                cel.GetComponent<UnityEngine.UI.Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
            }
        }
    }

    public void ClearAllGrids () {

        foreach (Transform col in previewGrid.transform) {
            foreach (Transform cel in col.transform) {
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;
                cel.GetComponent<UnityEngine.UI.Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
            }
        }

        foreach (Transform col in newGrid.transform) {
            foreach (Transform cel in col.transform) {
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = referenceVoid;
                cel.GetComponent<UnityEngine.UI.Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
            }
        }
    }
}   