using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json.Linq;
using NativeWebSocket;

public class hyperateSocket : MonoBehaviour
{
	// Put your websocket Token ID here
    public string websocketToken = "dNivBCD3rciwSBuPTVL45uDhrx64TE8g2fZ4e6981AJhVPYS1neGuVxWNtuQYWVt"; //You don't have one, get it here https://www.hyperate.io/api
    public string hyperateID = "internal-testing";
	// Textbox to display your heart rate in
    Text textBox;
    int currentHR;
	// Websocket for connection with Hyperate
    int framesCountOnMatch;
    float totalMatchHR;
    int currentLine;
    int framesCountOnLine;
    float totalLineHR;
    int frameCountTopToBottom, frameCountBottomToTop, frameCountLeftToRight, frameCountRightToLeft;
    int totalHRTopToBottom, totalHRBottomToTop, totalHRLeftToRight, totalHRRightToLeft;
    WebSocket websocket;
    
    void OnEnable()
    {
        textBox = GetComponent<Text>();
        SetInitialValues();

        if (GotHypeRateID())
        {
            Connect();
        }
    }

    void SetInitialValues()
    {
        textBox.text = "-";
        currentHR = 0;
        framesCountOnMatch = 0;
        totalMatchHR = 0f;
        currentLine = VariaveisGlobais.numReta;
        framesCountOnLine = 0;
        totalLineHR = 0f;
        VariaveisGlobais.maxHRPartidaAnterior = VariaveisGlobais.maxHRPartidaAtual;
        VariaveisGlobais.maxHRPartidaAtual = -1;
        VariaveisGlobais.minHRPartidaAtual = -1;
        VariaveisGlobais.avgHRPartidaAtual = -1;
    }

    bool GotHypeRateID()
    {
        if (PlayerPrefs.HasKey("HypeRateID") && PlayerPrefs.GetString("HypeRateID") != "")
        {
            hyperateID = PlayerPrefs.GetString("HypeRateID");
            Debug.Log("Achou o HypeRateID: " + hyperateID);
            return true;
        }
        return false;
    }

    async void Connect()
    {
        websocket = new WebSocket("wss://app.hyperate.io/socket/websocket?token=" + websocketToken);
        Debug.Log("Connect!");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            SendWebSocketMessage();
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
        // getting the message as a string
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            var msg = JObject.Parse(message);

            if (msg["event"].ToString() == "hr_update")
            {
                // Change textbox text into the newly received Heart Rate (integer like "86" which represents beats per minute)
                currentHR = (int) msg["payload"]["hr"];
                textBox.text = (string) msg["payload"]["hr"];
            }
        };

        // Send heartbeat message every 25seconds in order to not suspended the connection
        InvokeRepeating("SendHeartbeat", 1.0f, 25.0f);

        // waiting for messages
        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
    	if (websocket != null && PlayerPrefs.HasKey("HypeRateID") && PlayerPrefs.GetString("HypeRateID") != "")
        {
            // Atualiza o HR máximo da partida
            if (currentHR > VariaveisGlobais.maxHRPartidaAtual)
            {
                VariaveisGlobais.maxHRPartidaAtual = currentHR;
                Debug.Log("Current HR: " + currentHR + " Max HR: " + VariaveisGlobais.maxHRPartidaAtual);
            }

            if ( currentHR != 0) 
            {
                // Atualiza o HR mínimo da partida
                if ((VariaveisGlobais.minHRPartidaAtual == -1 || currentHR < VariaveisGlobais.minHRPartidaAtual))
                {
                    VariaveisGlobais.minHRPartidaAtual = currentHR;
                }

                // Atualiza o HR médio da partida
                totalMatchHR += currentHR;
                framesCountOnMatch++;
                VariaveisGlobais.avgHRPartidaAtual = (int) totalMatchHR / framesCountOnMatch;

                // Atualiza o HR médio da reta
                UpdateAvgHRInCurrentLine();
            }

            websocket.DispatchMessageQueue();
        }
#endif
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Log into the "internal-testing" channel
            await websocket.SendText("{\"topic\": \"hr:"+hyperateID+"\", \"event\": \"phx_join\", \"payload\": {}, \"ref\": 0}");
        }
    }
    async void SendHeartbeat()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Send heartbeat message in order to not be suspended from the connection
            await websocket.SendText("{\"topic\": \"phoenix\",\"event\": \"heartbeat\",\"payload\": {},\"ref\": 0}");
        }
    }

    private void UpdateAvgHRInCurrentLine()
    {
        // Acabou de mudar de reta
        if (currentLine != VariaveisGlobais.numReta) 
        {
            currentLine = VariaveisGlobais.numReta;
            framesCountOnLine = 0;
            totalLineHR = 0f;
        }

        // Atualiza o HR da reta
        switch (VariaveisGlobais.currentCollectedCoinDirection)
        {
            case '0':
                totalHRBottomToTop += currentHR;
                frameCountBottomToTop++;
                VariaveisGlobais.avgHRBottomToTop = (int) totalHRBottomToTop / frameCountBottomToTop;
                break;
            case '1':
                totalHRLeftToRight += currentHR;
                frameCountLeftToRight++;
                VariaveisGlobais.avgHRLeftToRight = (int) totalHRLeftToRight / frameCountLeftToRight;
                break;
            case '2':
                totalHRTopToBottom += currentHR;
                frameCountTopToBottom++;
                VariaveisGlobais.avgHRTopToBottom = (int) totalHRTopToBottom / frameCountTopToBottom;
                break;
            case '3':
                totalHRRightToLeft += currentHR;
                frameCountRightToLeft++;
                VariaveisGlobais.avgHRRightToLeft = (int) totalHRRightToLeft / frameCountRightToLeft;
                break;
            default:
                break;
        }

        totalLineHR += currentHR;
        framesCountOnLine++;
        VariaveisGlobais.avgHRRetaAtual = (int) totalLineHR / framesCountOnLine;
    }

    private async void OnApplicationQuit()
    {
        if (websocket != null)
            if (websocket.State == WebSocketState.Open)
                await websocket.Close();
    }

    void OnDisable()
    {
        VariaveisGlobais.avgHRPartidaAnterior = VariaveisGlobais.avgHRPartidaAtual;
        // Stop the WebSocket connection when the script is disabled
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            // Close the WebSocket connection
            websocket.Close();
        }
    }

}

public class HyperateResponse
{
    public string Event { get; set; }
    public string Payload { get; set; }
    public string Ref { get; set; }
    public string Topic { get; set; }
}
