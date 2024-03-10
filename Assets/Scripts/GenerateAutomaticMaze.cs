using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic; 
using System.Linq;
using System.IO;

public class GenerateAutomaticMaze : Singleton<SaveHandler>
{
    public GameObject grid;
    public GameObject handGear;

    private int maxColumns, maxRows;
    private string[,] gridMatrix;
    private float minX, maxX, minY, maxY, timePerCoinBottomToTop, timePerCoinTopToBottom, timePerCoinLeftToRight, timePerCoinRightToLeft;
    int xMaxColumnIndex, yMaxCellIndex, xMinColumnIndex, yMinCellIndex;
    private int maxCellGrowthOrShrink = 1;

    int xCurrentIndexInGrid;
    int yCurrentIndexInGrid;
    private Vector3[] cantosCelula = new Vector3[4];
    bool firstOrSecondCoinInMaze;
    bool firstAutomaticMaze = true;
    bool wantToRetry = false;
    int coinCount;

    void OnEnable()
    {
        if (VariaveisGlobais.partidaCorrente == 2001)
        {
            // if (!wantToRetry)
                GenerateAutomaticMazeFromCalibration();
            // else
            //     LoadAutoGeneratedMaze();
            //     wantToRetry = false;
        }
    }

    public void Retry()
    {
        if (VariaveisGlobais.partidaCorrente == 2001 && !firstAutomaticMaze)
            wantToRetry = true;
    }

    void OnDisable()
    {
        Debug.Log("--------------------------------------------------");
        Debug.Log("OnDisable: Qual tempo usou pra adaptar essa fase que acabou");
        Debug.Log("TimePerCoinBottomToTop: " + timePerCoinBottomToTop);
        Debug.Log("TimePerCoinTopToBottom: " + timePerCoinTopToBottom);
        Debug.Log("TimePerCoinLeftToRight: " + timePerCoinLeftToRight);
        Debug.Log("TimePerCoinRightToLeft: " + timePerCoinRightToLeft);
        Debug.Log("--------------------------------------------------");
    }

    private void GenerateAutomaticMazeFromCalibration()
    {
        ResetGridMatrixAndValues();
        ProcessCalibrationData();
        GenerateMaze();
        LoadAutoGeneratedMaze();

        if (firstAutomaticMaze)
        {
            firstAutomaticMaze = false;
        }
        // PrintTimePerCoinInEachDirectionForThisMaze();
    }

    private void ResetGridMatrixAndValues()
    {
        for (int j = 0; j < maxColumns; j++)
        {
            for (int i = 0; i < maxRows; i++)
            {
                gridMatrix[i, j] = null;
            }
        }
        firstOrSecondCoinInMaze = true;
        coinCount = 0;
    }

    private void ProcessCalibrationData()
    {
        int dataProcessingMode;

        // if (firstAutomaticMaze)
        // {
        //     dataProcessingMode = 3;
        // }
        // else 
        // {
            dataProcessingMode = PlayerPrefs.GetInt("DataProcessingMode");
        // }
        
        switch (dataProcessingMode)
        {
            case 1:
                WeightedAverageFromAll();
                break;
            case 2:
                PerformanceFromPreviousMatchOnly();
                break;
            // case 3:
            //     PerformanceFromCalibrationOnly();
            //     break;
            default:
                PerformanceFromPreviousMatchOnly();
                break;
        }

    }

    private void GenerateMaze() 
    {
        // Tamanho da matrix que representará o novo grid
        maxRows = grid.transform.GetChild(0).childCount;
        maxColumns = grid.transform.childCount;
        gridMatrix = new string[maxRows, maxColumns];

        // Índices das colunas e células que delimitam o novo grid personalizado
        int[] limitIndexes = GetGridLimitsFromCalibrationCoordinates();
        xMaxColumnIndex = limitIndexes[0];
        yMaxCellIndex = limitIndexes[1];
        xMinColumnIndex = limitIndexes[2];
        yMinCellIndex = limitIndexes[3];

        DrawRectangle();

        // Percorre uma última vez garantindo que não tem espaço vazio
        FillOutlineWithWalls();
    }

    private int[] GetGridLimitsFromCalibrationCoordinates()
    {
        int xMaxColumnIndex = -1;
        int yMaxCellIndex = -1;
        int xMinColumnIndex = -1;
        int yMinCellIndex = -1;
        bool foundXMax = false;
        bool foundXMin = false;
        bool foundYMax = false;
        bool foundYMin = false;

        // Procura a coluna que tem o maxX e a coluna que tem o minX
        for (int col = 0; col < maxColumns; col++)
        {
            Transform column = grid.transform.GetChild(col);
            column.GetComponent<RectTransform>().GetWorldCorners(cantosCelula);
            float cellWidth = Mathf.Abs(cantosCelula[1].x - cantosCelula[2].x); // largura da célula

            // Verifica se a coluna tem o maxX
            if (!foundXMax && maxX >= column.transform.position.x - cellWidth / 2 && maxX <= column.transform.position.x + cellWidth / 2) 
            {
                xMaxColumnIndex = col;
                foundXMax = true;
            }
            // Verifica se a coluna tem o minX
            else if (!foundXMin && minX >= column.transform.position.x - cellWidth / 2 && minX <= column.transform.position.x + cellWidth / 2) 
            {
                xMinColumnIndex = col;
                foundXMin = true;
            }

            if (foundXMax && foundXMin) 
            {
                break;
            }
        }

        // Procura a célula que tem o maxY e a célula que tem o minY
        if (xMaxColumnIndex != -1 && xMinColumnIndex != -1) 
        {
            for (int cel = 0; cel < maxRows; cel++)
            {
                Transform cell = grid.transform.GetChild(0).GetChild(cel);
                cell.GetComponent<RectTransform>().GetWorldCorners(cantosCelula);
                float cellHeight = Mathf.Abs(cantosCelula[0].y - cantosCelula[1].y); // altura da célula

                // Verifica se a célula tem o maxY
                if (!foundYMax && maxY >= cell.transform.position.y - cellHeight / 2 && maxY <= cell.transform.position.y + cellHeight / 2) 
                {
                    yMaxCellIndex = cel;
                    foundYMax = true;
                }
                // Verifica se a célula tem o minY
                else if (!foundYMin && minY >= cell.transform.position.y - cellHeight / 2 && minY <= cell.transform.position.y + cellHeight / 2) 
                {
                    yMinCellIndex = cel;
                    foundYMin = true;
                }

                if (foundYMax && foundYMin) 
                {
                    break;
                }
            }
        }

        int xMaxColumnFinalIndex;
        int xMinColumnFinalIndex;
        int yMaxCellFinalIndex;
        int yMinCellFinalIndex;

        // Como só tem uma calibração agora, isso não é necessário
        // if (firstAutomaticMaze)
        // {
        //     // reduz o tamanho do grid para que não fique muito grande na primeira partida
        //     if ((xMaxColumnIndex-2) - (xMinColumnIndex+2) > 2)
        //     {
        //         xMaxColumnFinalIndex = xMaxColumnIndex - 2;
        //         xMinColumnFinalIndex = xMinColumnIndex + 2;
        //     }
        //     else if ((xMaxColumnIndex-1) - (xMinColumnIndex+1) > 2)
        //     {
        //         xMaxColumnFinalIndex = xMaxColumnIndex - 1;
        //         xMinColumnFinalIndex = xMinColumnIndex + 1;
        //     }
        //     else
        //     {
        //         xMaxColumnFinalIndex = xMaxColumnIndex;
        //         xMinColumnFinalIndex = xMinColumnIndex;
        //     }

        //     if ((yMinCellIndex-2) - (yMaxCellIndex+2) > 2)
        //     {
        //         yMaxCellFinalIndex = yMaxCellIndex + 2;
        //         yMinCellFinalIndex = yMinCellIndex - 2;
        //     }
        //     else if ((yMinCellIndex-1) - (yMaxCellIndex+1) > 2)
        //     {
        //         yMaxCellFinalIndex = yMaxCellIndex + 1;
        //         yMinCellFinalIndex = yMinCellIndex - 1;
        //     }
        //     else
        //     {
        //         yMaxCellFinalIndex = yMaxCellIndex;
        //         yMinCellFinalIndex = yMinCellIndex;
        //     }
        // }
        // else
        // {
            xMaxColumnFinalIndex = IndexWithPseudoGrowthOrShrink(xMaxColumnIndex, timePerCoinLeftToRight, false, true, xMinColumnIndex); // canto direito - usa leftToRight
            xMinColumnFinalIndex = IndexWithPseudoGrowthOrShrink(xMinColumnIndex, timePerCoinRightToLeft, true, true, xMaxColumnIndex); // canto esquerdo - usa rightToLeft
            yMaxCellFinalIndex = IndexWithPseudoGrowthOrShrink(yMaxCellIndex, timePerCoinBottomToTop, true, false, yMinCellIndex); // canto superior - usa bottomToTop
            yMinCellFinalIndex = IndexWithPseudoGrowthOrShrink(yMinCellIndex, timePerCoinTopToBottom, false, false, yMaxCellIndex); // canto inferior - usa topToBottom
        // }

        return new int[] { xMaxColumnFinalIndex, yMaxCellFinalIndex, xMinColumnFinalIndex, yMinCellFinalIndex };
    }

    private void WeightedAverageFromAll() 
    {
        // considera as médias ponderadas das partidas anteriores
        // if (firstAutomaticMaze)
        // {
        //     PerformanceFromCalibrationOnly();
        // }
        // else
        // {
            Debug.Log("Usou média ponderada");
            minX = (!float.IsNaN(VariaveisGlobais.minX)) ? (VariaveisGlobais.minX < minX) ? VariaveisGlobais.minX : (0.7f * VariaveisGlobais.minX + 0.3f * minX) : minX;
            minY = (!float.IsNaN(VariaveisGlobais.minY)) ? (VariaveisGlobais.minY < minY) ? VariaveisGlobais.minY : (0.7f * VariaveisGlobais.minY + 0.3f * minY) : minY;
            maxX = (!float.IsNaN(VariaveisGlobais.maxX)) ? (VariaveisGlobais.maxX > maxX) ? VariaveisGlobais.maxX : (0.7f * VariaveisGlobais.maxX + 0.3f * maxX) : maxX;
            maxY = (!float.IsNaN(VariaveisGlobais.maxY)) ? (VariaveisGlobais.maxY > maxY) ? VariaveisGlobais.maxY : (0.7f * VariaveisGlobais.maxY + 0.3f * maxY) : maxY;
            timePerCoinBottomToTop = (!float.IsNaN(VariaveisGlobais.timePerCoinBottomToTop)) ? (0.7f * VariaveisGlobais.timePerCoinBottomToTop + 0.3f * timePerCoinBottomToTop) : timePerCoinBottomToTop;
            timePerCoinTopToBottom = (!float.IsNaN(VariaveisGlobais.timePerCoinTopToBottom)) ? (0.7f * VariaveisGlobais.timePerCoinTopToBottom + 0.3f * timePerCoinTopToBottom) : timePerCoinTopToBottom;
            timePerCoinLeftToRight = (!float.IsNaN(VariaveisGlobais.timePerCoinLeftToRight)) ? (0.7f * VariaveisGlobais.timePerCoinLeftToRight + 0.3f * timePerCoinLeftToRight) : timePerCoinLeftToRight;
            timePerCoinRightToLeft = (!float.IsNaN(VariaveisGlobais.timePerCoinRightToLeft)) ? (0.7f * VariaveisGlobais.timePerCoinRightToLeft + 0.3f * timePerCoinRightToLeft) : timePerCoinRightToLeft;
        // }
    }

    private void PerformanceFromPreviousMatchOnly()
    {
        Debug.Log("Usou desempenho da partida anterior somente");
        // considera a última partida
        minX = VariaveisGlobais.minX;
        minY = VariaveisGlobais.minY;
        maxX = VariaveisGlobais.maxX;
        maxY = VariaveisGlobais.maxY;
        timePerCoinBottomToTop = (!float.IsNaN(VariaveisGlobais.timePerCoinBottomToTop)) ? VariaveisGlobais.timePerCoinBottomToTop : 0;
        timePerCoinTopToBottom = (!float.IsNaN(VariaveisGlobais.timePerCoinTopToBottom)) ? VariaveisGlobais.timePerCoinTopToBottom : 0;
        timePerCoinLeftToRight = (!float.IsNaN(VariaveisGlobais.timePerCoinLeftToRight)) ? VariaveisGlobais.timePerCoinLeftToRight : 0;
        timePerCoinRightToLeft = (!float.IsNaN(VariaveisGlobais.timePerCoinRightToLeft)) ? VariaveisGlobais.timePerCoinRightToLeft : 0;
    }

    // Essa função ficou inútil 
    // private void PerformanceFromCalibrationOnly() 
    // {
    //     Debug.Log("Usou desempenho na calibração somente");
    //     // considera somente as partidas de calibração
    //     int countTopToBottom = 0;
    //     int countBottomToTop = 0;
    //     int countLeftToRight = 0;
    //     int countRightToLeft = 0;
    //     timePerCoinBottomToTop = 0;
    //     timePerCoinTopToBottom = 0;
    //     timePerCoinLeftToRight = 0;
    //     timePerCoinRightToLeft = 0;

    //     for (int i = 1; i < 8; i++)
    //     {
    //         string saveData = PlayerPrefs.GetString("CalibrationData" + i.ToString());
    //         string[] dataParts = saveData.Split(';');

    //         foreach (string dataPart in dataParts)
    //         {
    //             string[] keyValue = dataPart.Split(':');
    //             string key = keyValue[0];
    //             string value = keyValue[1];

    //             if (string.IsNullOrEmpty(value) || float.IsNaN(float.Parse(value)))
    //             {
    //                 continue;
    //             }

    //             float parsedValue = float.Parse(value);

    //             switch (key)
    //             {
    //                 case "minX":
    //                     minX = (i == 1 || parsedValue < minX) ? parsedValue : minX;
    //                     break;
    //                 case "minY":
    //                     minY = (i == 1 || parsedValue < minY) ? parsedValue : minY;
    //                     break;
    //                 case "maxX":
    //                     maxX = (i == 1 || parsedValue > maxX) ? parsedValue : maxX;
    //                     break;
    //                 case "maxY":
    //                     maxY = (i == 1 || parsedValue > maxY) ? parsedValue : maxY;
    //                     break;
    //                 case "timePerCoinBottomToTop":
    //                     timePerCoinBottomToTop += parsedValue;
    //                     countBottomToTop++;
    //                     break;
    //                 case "timePerCoinTopToBottom":
    //                     timePerCoinTopToBottom += parsedValue;
    //                     countTopToBottom++;
    //                     break;
    //                 case "timePerCoinLeftToRight":
    //                     timePerCoinLeftToRight += parsedValue;
    //                     countLeftToRight++;
    //                     break;
    //                 case "timePerCoinRightToLeft":
    //                     timePerCoinRightToLeft += parsedValue;
    //                     countRightToLeft++;
    //                     break;
    //             }
    //         }
    //     }

    //     timePerCoinBottomToTop /= countBottomToTop;
    //     timePerCoinTopToBottom /= countTopToBottom;
    //     timePerCoinLeftToRight /= countLeftToRight;
    //     timePerCoinRightToLeft /= countRightToLeft;
    // }

    public int IndexWithPseudoGrowthOrShrink(int index, float timePerCoinInDirection, bool shouldDecreaseToGrow, bool isColumn, int otherCoordinateExtremeIndex)
    {
        int cellGrowth;

        // Regra nova definida pelo Professor Carlos
        if (timePerCoinInDirection < 0.6f && timePerCoinInDirection != 0)
        {
            // Checa se o índice já está no limite do grid
            if (index == 0 || (isColumn && index==maxColumns - 1) || (!isColumn && index==maxRows - 1)) 
            {
                return index;
            }
            cellGrowth = maxCellGrowthOrShrink;
        }
        else {
            // Conferindo se pode diminuir
            if ((shouldDecreaseToGrow && otherCoordinateExtremeIndex - index > 3) || (!shouldDecreaseToGrow && index - otherCoordinateExtremeIndex > 3))
            {
                if (timePerCoinInDirection < 1)
                    cellGrowth = 0;
                else 
                {
                    cellGrowth = -maxCellGrowthOrShrink;
                }
            }
            else {
                cellGrowth = 0;
            }
        }

        // Regra antiga
        // if (timePerCoinInDirection < 2 && timePerCoinInDirection != 0)
        // {
        //     // Checa se o índice já está no limite do grid
        //     if (index == 0 || (isColumn && index==maxColumns - 1) || (!isColumn && index==maxRows - 1)) 
        //     {
        //         return index;
        //     }

        //     if (timePerCoinInDirection < 1)
        //     {
        //         cellGrowth = maxCellGrowthOrShrink;
        //     }
        //     else
        //     {
        //         cellGrowth = UnityEngine.Random.Range(0, maxCellGrowthOrShrink + 1);
        //     }
        // }
        // else {
        //     // Conferindo se pode diminuir
        //     if ((shouldDecreaseToGrow && otherCoordinateExtremeIndex - index > 3) || (!shouldDecreaseToGrow && index - otherCoordinateExtremeIndex > 3))
        //     {
        //         if (timePerCoinInDirection < 5)
        //             cellGrowth = UnityEngine.Random.Range(-maxCellGrowthOrShrink, 0 + 1);
        //         else 
        //         {
        //             cellGrowth = -maxCellGrowthOrShrink;
        //         }
        //     }
        //     else {
        //         cellGrowth = 0;
        //     }
        // }

        if (shouldDecreaseToGrow) // Índice negativo → deve subtrair para crescer 
        {
            return index - cellGrowth;
        }
        return index + cellGrowth;
    }

    private void DrawRectangle()
    {
        bool[] hasGone = new bool[4];
        float[] timePerCoin = new float[] { timePerCoinBottomToTop, timePerCoinLeftToRight, timePerCoinTopToBottom, timePerCoinRightToLeft };
        int[] directions = new int[] { 0, 1, 2, 3 }; // 0: cima, 1: direita, 2: baixo, 3: esquerda

        // Array.Sort(timePerCoin, (x, y) => y.CompareTo(x)); // Ordena do maior para o menor

        // Início aleatório, mas colado em uma das paredes
        int xStartIndex = UnityEngine.Random.Range(xMinColumnIndex, xMaxColumnIndex + 1);
        int yStartIndex = (xStartIndex == xMinColumnIndex || xStartIndex == xMaxColumnIndex) ? UnityEngine.Random.Range(yMaxCellIndex, yMinCellIndex + 1) : (UnityEngine.Random.Range(0, 2) == 0 ? yMinCellIndex : yMaxCellIndex);
        gridMatrix[yStartIndex, xStartIndex] = "start";

        xCurrentIndexInGrid = xStartIndex;
        yCurrentIndexInGrid = yStartIndex;

        int preventInfiniteLoopCounter = 0;

        bool[] walls;

        int lastDirection = -1;

        while (!(hasGone[0] && hasGone[1] && hasGone[2] && hasGone[3]))
        {
            for (int i = 0; i < 4; i++)
            {
                walls = new bool[] { 
                    yCurrentIndexInGrid == yMaxCellIndex, // top
                    xCurrentIndexInGrid == xMaxColumnIndex, // right
                    yCurrentIndexInGrid == yMinCellIndex, // bottom
                    xCurrentIndexInGrid == xMinColumnIndex //left
                };

                if (walls[i])
                {
                    int randomZeroOrOne = UnityEngine.Random.Range(0, 2);
                    int chosenOption;
                    int[] options;
                    if (walls[(i + 1) % 4]) // em um dos cantos do limite
                    {
                        options = new int[] { (i + 2) % 4, (i + 3) % 4 };
                    }
                    else if (walls[(i + 3) % 4]) // no outro canto (mas mesma parede) do limite
                    {
                        options = new int[] { (i + 1) % 4, (i + 2) % 4 };
                    }
                    else // no meio de uma parede
                    {
                        options = new int[] { (i + 1) % 4, (i + 3) % 4 };
                    }

                    int firstChosenOption = options[randomZeroOrOne];
                    int secondChosenOption = options[(randomZeroOrOne + 1) % 2];

                    if (!hasGone[firstChosenOption] && lastDirection != directions[(firstChosenOption + 2) % 4])
                    {
                        chosenOption = firstChosenOption;
                    }
                    else if (!hasGone[secondChosenOption] && lastDirection != directions[(secondChosenOption + 2) % 4]) 
                    {
                        chosenOption = secondChosenOption;
                    }
                    else // já foi para ambos os sentidos possíveis
                    {
                        continue;
                    }

                    DrawLineUntilPossible(chosenOption);

                    if (!(firstOrSecondCoinInMaze && !(walls.Count(b => b)==2))) // se não estava num canto e era a primeira moeda, deve poder voltar
                    {
                       hasGone[chosenOption] = true;
                    }
                    lastDirection = chosenOption;
                }
            }
            
            if (preventInfiniteLoopCounter > 1000) 
            {
                Debug.Log("Não conseguiu gerar o labirinto completo");
                break;
            }
            preventInfiniteLoopCounter++;
        }

        gridMatrix[yCurrentIndexInGrid, xCurrentIndexInGrid] = "finish";
    }

    public void DrawLineUntilPossible(int direction)
    {
        int xStep = 0;
        int yStep = 0;

        switch (direction)
        {
            case 0: // Ir para cima
                yStep = -1;
                break;
            case 1: // Ir para a direita
                xStep = 1;
                break;
            case 2: // Ir para baixo
                yStep = 1;
                break;
            case 3: // Ir para a esquerda
                xStep = -1;
                break;
        }

        while (true)
        {
            if ((yCurrentIndexInGrid == yMaxCellIndex && yStep < 0) ||
                (yCurrentIndexInGrid == yMinCellIndex && yStep > 0) ||
                (xCurrentIndexInGrid == xMinColumnIndex && xStep < 0) ||
                (xCurrentIndexInGrid == xMaxColumnIndex && xStep > 0))
            {
                Debug.Log("Cheguei em algum limite: "+gridMatrix);
                return;
            }

            int nextX = xCurrentIndexInGrid + xStep;
            int nextY = yCurrentIndexInGrid + yStep;

            // Checa se já não está no limite do grid
            if (nextX < 0 || nextX >= maxColumns || nextY < 0 || nextY >= maxRows || gridMatrix[nextY, nextX] != null)
            {
                return;
            }
            
            int twoBlocksAwayX = nextX + xStep;
            int twoBlocksAwayY = nextY + yStep;
            
            // Checa se próximos 2 blocos não estão preenchidos
            if (twoBlocksAwayX >= 0 && twoBlocksAwayX < maxColumns && 
                twoBlocksAwayY >= 0 && twoBlocksAwayY < maxRows && 
                gridMatrix[twoBlocksAwayY, twoBlocksAwayX] != null)
            {
                return;
            }

            if (firstOrSecondCoinInMaze && coinCount>2) 
            {
                firstOrSecondCoinInMaze = false;
            }
            coinCount++;

            // Checa se o início não está em volta
            if (!firstOrSecondCoinInMaze && 
                (
                    // adjacente
                    nextY - 1 >= 0 && gridMatrix[nextY - 1, nextX] == "start" ||
                    nextY + 1 <= maxRows - 1 && gridMatrix[nextY + 1, nextX] == "start" ||
                    nextX - 1 >= 0 && gridMatrix[nextY, nextX - 1] == "start" ||
                    nextX + 1 <= maxColumns - 1 && gridMatrix[nextY, nextX + 1] == "start" ||
                    // diagonais
                    nextY - 1 >= 0 && nextX - 1 >= 0 && gridMatrix[nextY - 1, nextX - 1] == "start" ||
                    nextY + 1 <= maxRows - 1 && nextX - 1 >= 0 && gridMatrix[nextY + 1, nextX - 1] == "start" ||
                    nextY - 1 >= 0 && nextX + 1 <= maxColumns - 1 && gridMatrix[nextY - 1, nextX + 1] == "start" ||
                    nextY + 1 <= maxRows - 1 && nextX + 1 <= maxColumns - 1 && gridMatrix[nextY + 1, nextX + 1] == "start"
                ))
            {
                return;
            }

            gridMatrix[nextY, nextX] = "staticMoedaAmarela";

            xCurrentIndexInGrid = nextX;
            yCurrentIndexInGrid = nextY;
        }
    }

    private void FillOutlineWithWalls()
    {
        // preenche todos espaços vazios com parede
        for (int colIndex = 0; colIndex < maxColumns; colIndex++)
        {
            for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
            {
                // preenche contorno com parede
                if (gridMatrix[rowIndex, colIndex] == null &&
                    (   
                        // checa blocos adjacentes
                        (colIndex > 0 && gridMatrix[rowIndex, colIndex - 1] != null && !gridMatrix[rowIndex, colIndex - 1].StartsWith("Tiles")) ||
                        (colIndex < maxColumns - 1 && gridMatrix[rowIndex, colIndex + 1] != null && !gridMatrix[rowIndex, colIndex + 1].StartsWith("Tiles")) ||
                        (rowIndex > 0 && gridMatrix[rowIndex - 1, colIndex] != null && !gridMatrix[rowIndex - 1, colIndex].StartsWith("Tiles")) ||
                        (rowIndex < maxRows - 1 && gridMatrix[rowIndex + 1, colIndex] != null && !gridMatrix[rowIndex + 1, colIndex].StartsWith("Tiles")) ||
                        // checa blocos nas diagonais
                        (colIndex > 0 && rowIndex > 0 && gridMatrix[rowIndex - 1, colIndex - 1] != null && !gridMatrix[rowIndex - 1, colIndex - 1].StartsWith("Tiles")) ||
                        (colIndex > 0 && rowIndex < maxRows - 1 && gridMatrix[rowIndex + 1, colIndex - 1] != null && !gridMatrix[rowIndex + 1, colIndex - 1].StartsWith("Tiles")) ||
                        (colIndex < maxColumns - 1 && rowIndex > 0 && gridMatrix[rowIndex - 1, colIndex + 1] != null && !gridMatrix[rowIndex - 1, colIndex + 1].StartsWith("Tiles")) ||
                        (colIndex < maxColumns - 1 && rowIndex < maxRows - 1 && gridMatrix[rowIndex + 1, colIndex + 1] != null && !gridMatrix[rowIndex + 1, colIndex + 1].StartsWith("Tiles"))
                    ))
                {
                    gridMatrix[rowIndex, colIndex] = "Tiles_0"; // preenche com parede cinza
                }
                // retira primeira moeda após início
                // else if (gridMatrix[rowIndex, colIndex] == "staticMoedaAmarela" &&
                //             (// checa blocos adjacentes
                //             colIndex > 0 && gridMatrix[rowIndex, colIndex - 1] == "start" ||
                //             colIndex < maxColumns - 1 && gridMatrix[rowIndex, colIndex + 1] == "start" ||
                //             rowIndex > 0 && gridMatrix[rowIndex - 1, colIndex] == "start" ||
                //             rowIndex < maxRows - 1 && gridMatrix[rowIndex + 1, colIndex] == "start"
                //             )
                //         )
                // {
                //     gridMatrix[rowIndex, colIndex] = "vazioBloco"; // preenche com bloco vazio
                // }
            }
        }

        // preenche células vazias com vazioBloco
        for (int colIndex = 0; colIndex < maxColumns; colIndex++)
        {
            for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
            {
                if (gridMatrix[rowIndex, colIndex] == null) // célula vazia
                {
                    gridMatrix[rowIndex, colIndex] = "vazioBloco"; // preenche com bloco vazio
                }
            }
        }
    }

    private void LoadAutoGeneratedMaze() 
    {
        int totalMoedasNaPartida = 0;
        int j = 0; 
        foreach (Transform col in grid.transform) {
            int i = 0;
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
                cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = Resources.Load<Sprite>("Sprites" + Path.DirectorySeparatorChar + gridMatrix[i, j]);
                cel.GetComponent<UnityEngine.UI.Image>().sprite = cel.gameObject.GetComponent<CelulaInfo>().selecionadoSprite;
                if (gridMatrix[i, j].StartsWith("Tiles"))
                {
                    cel.gameObject.AddComponent<BlocoImpeditivo>();
                    cel.gameObject.AddComponent<Rigidbody2D>();
                    cel.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    cel.gameObject.AddComponent<BoxCollider2D>();
                    cel.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(57, 57);
                    cel.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                else if (gridMatrix[i, j].StartsWith("static")) 
                {
                    cel.gameObject.AddComponent<Coin>();
                    cel.gameObject.AddComponent<Rigidbody2D>();
                    cel.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    cel.gameObject.AddComponent<BoxCollider2D>();
                    cel.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    cel.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(57, 57);
                    totalMoedasNaPartida++;
                }
                else if (gridMatrix[i, j].StartsWith("start"))
                {
                    cel.gameObject.tag = "Start";
                    handGear.transform.position = cel.transform.position;
                    handGear.SetActive(true);
                }
                else if (gridMatrix[i, j].StartsWith("finish")) 
                {
                    cel.gameObject.AddComponent<FinalizouPartida>();
                    cel.gameObject.AddComponent<Rigidbody2D>();
                    cel.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    cel.gameObject.AddComponent<BoxCollider2D>();
                    cel.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(57, 57);
                    cel.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                i++;
            }
            j++;
        }
        VariaveisGlobais.totalMoedasNaPartida = totalMoedasNaPartida;
    }
    // private void PrintTimePerCoinInEachDirectionForThisMaze()
    // {
    //     Debug.Log("1. TimePerCoinBottomToTop: " + timePerCoinBottomToTop);
    //     Debug.Log("2. TimePerCoinTopToBottom: " + timePerCoinTopToBottom);
    //     Debug.Log("3. TimePerCoinLeftToRight: " + timePerCoinLeftToRight);
    //     Debug.Log("4. TimePerCoinRightToLeft: " + timePerCoinRightToLeft);
    // } 
}
