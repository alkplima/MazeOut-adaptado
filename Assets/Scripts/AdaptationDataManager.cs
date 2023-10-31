using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class AdaptationDataManager
{
    private static string filePath = "adaption_data.txt"; // Nome do arquivo a ser criado ou modificado

    public static void SaveData()
    {
        if (VariaveisGlobais.partidaCorrente < 0 && VariaveisGlobais.partidaCorrente > -8) // 7 fases de calibração
        {
            string saveData = 
                "minX:" + VariaveisGlobais.minX +
                ";minY:" + VariaveisGlobais.minY +
                ";maxX:" + VariaveisGlobais.maxX +
                ";maxY:" + VariaveisGlobais.maxY +
                ";timePerCoinBottomToTop:" + VariaveisGlobais.timePerCoinBottomToTop +
                ";timePerCoinTopToBottom:" + VariaveisGlobais.timePerCoinTopToBottom +
                ";timePerCoinLeftToRight:" + VariaveisGlobais.timePerCoinLeftToRight +
                ";timePerCoinRightToLeft:" + VariaveisGlobais.timePerCoinRightToLeft;

            PlayerPrefs.SetString("CalibrationData" + (VariaveisGlobais.partidaCorrente * -1).ToString(), saveData);
        }




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
            streamWriter.WriteLine("timePerCoinBottomToTop: " + VariaveisGlobais.timePerCoinBottomToTop);
            streamWriter.WriteLine("timePerCoinTopToBottom: " + VariaveisGlobais.timePerCoinTopToBottom);
            streamWriter.WriteLine("timePerCoinLeftToRight: " + VariaveisGlobais.timePerCoinLeftToRight);
            streamWriter.WriteLine("timePerCoinRightToLeft: " + VariaveisGlobais.timePerCoinRightToLeft);
        }
    }
}
