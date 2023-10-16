using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class AdaptionInfoFileManager
{
    private static string filePath = "adaption_data.txt"; // Nome do arquivo a ser criado ou modificado

    public static void SaveDataToFile()
    {
        if (!File.Exists(filePath))
        {
            // Se arquivo não existir, cria
            File.Create(filePath).Dispose();
        }

        using (StreamWriter streamWriter = File.AppendText(filePath))
        {
            streamWriter.WriteLine("Partida: " + VariaveisGlobais.partidaCorrente);
            streamWriter.WriteLine("minX: " + VariaveisGlobais.minX);
            streamWriter.WriteLine("minY: " + VariaveisGlobais.minY);
            streamWriter.WriteLine("maxX: " + VariaveisGlobais.maxX);
            streamWriter.WriteLine("maxY: " + VariaveisGlobais.maxY);
            streamWriter.WriteLine("timePerCoinTopToBottom: " + VariaveisGlobais.timePerCoinTopToBottom);
            streamWriter.WriteLine("timePerCoinBottomToTop: " + VariaveisGlobais.timePerCoinBottomToTop);
            streamWriter.WriteLine("timePerCoinLeftToRight: " + VariaveisGlobais.timePerCoinLeftToRight);
            streamWriter.WriteLine("timePerCoinRightToLeft: " + VariaveisGlobais.timePerCoinRightToLeft);
        }
    }
}
