using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PaterlandGlobal
{

    // Controle do jogo
    internal static int VersaoCorrente = 1000;
    internal static Webcam currentWebcam;
    internal static bool autorizadoMovimento;

    internal static string SomaMd5(string string_A_Encriptar)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(string_A_Encriptar);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    internal static string SomaSHA256(string string_A_Encriptar)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(string_A_Encriptar);

        // encrypt bytes
        System.Security.Cryptography.SHA256Managed sha256 = new System.Security.Cryptography.SHA256Managed();
        byte[] hashBytes = sha256.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    // Originalmente de https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
    public static bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    public static void ApagarConteudoApplicationPath()
    {
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths)
        {
            File.Delete(filePath);
#if UNITY_WEBGL
            Application.ExternalEval("FS.syncfs(false, function (err) {})");
            Debug.Log("Tentou dar sync no disco.");
#endif
        }

    }
}
