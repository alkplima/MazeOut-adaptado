using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System;

public class ConfigBehaviours : MonoBehaviour
{
    bool coroutineIsRunning;

    public string[] parameterName = new string[1];
    public char[] parameterType = new char[1];
    public string[] parameterValue = new string[1];

    public Image imageToHighlight;
    public Color colorNormal, colorHighlighted;
    public bool hasTextToBeShow;
    public InputField InputTextToChange;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(UpdateCoroutine());
    }

    // Update is called once per frame

    void Update()
    {
        if (!coroutineIsRunning)
            StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine()
    {
        coroutineIsRunning = true;
        OnEnable();
        yield return new WaitForSeconds(0.3f);
        coroutineIsRunning = false;
    }

    private void OnEnable()
    {
        coroutineIsRunning = false;
        int attributesSum = 0;

        for (int i = 0; i < parameterName.Length; i++)
        {
            if (PlayerPrefs.HasKey(parameterName[i]))
            {
                switch (parameterType[i])
                {
                    case 'F':
                        if (PlayerPrefs.GetFloat(parameterName[i]) == float.Parse(parameterValue[i]))
                            attributesSum++; ;
                        break;
                    case 'I':
                        if (PlayerPrefs.GetInt(parameterName[i]) == int.Parse(parameterValue[i]))
                            attributesSum++;
                        break;
                    case 'S':
                        if (PlayerPrefs.GetString(parameterName[i]) == parameterValue[i])
                            attributesSum++;
                        break;
                }
            }
        }

        if (imageToHighlight)
        {
            if (attributesSum == parameterName.Length)
                imageToHighlight.color = colorHighlighted;
            else
                imageToHighlight.color = colorNormal;
        }

        if (hasTextToBeShow)
        {
            for (int i = 0; i < parameterName.Length; i++)
                switch (parameterType[i])
                    {
                        case 'T':
                            InputTextToChange.text = PlayerPrefs.GetString(parameterName[i]);
                            break;
                    }
        }
    }
    public void SetConfigValue()
    {
        for (int i = 0; i < parameterName.Length; i++)
        {
            switch (parameterType[i])
            {
                case 'F':
                    PlayerPrefs.SetFloat(parameterName[i],float.Parse(parameterValue[i]));
                    break;
                case 'I':
                    PlayerPrefs.SetInt(parameterName[i], int.Parse(parameterValue[i]));
                    break;
                case 'S':
                    PlayerPrefs.SetString(parameterName[i], parameterValue[i]);
                    break;
                case 'B':
                    if (parameterValue[i]=="")
                    {
                        if (PlayerPrefs.GetInt(parameterName[i]) == 0)
                            PlayerPrefs.SetInt(parameterName[i], 1);
                        else PlayerPrefs.SetInt(parameterName[i], 0);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(parameterName[i], int.Parse(parameterValue[i]));
                    }
                    break;
            }
        }

        VariaveisGlobais.RefreshValues();

    }

    public void SetConfigTextInputValue(InputField TextInput)
    {
        for (int i = 0; i < parameterName.Length; i++)
        {
            switch (parameterType[i])
            {
                case 'T':
                    PlayerPrefs.SetString(parameterName[i], TextInput.text);
                    break;
            }
        }
        VariaveisGlobais.RefreshValues();
    }

    public void ValidateAndSetInputSequence(InputField TextInput)
    {
        TextInput.text = TextInput.text.ToUpper();

        string txt = "";

        for (int i = 0; i < TextInput.text.Length; i++)
        {
            if (((TextInput.text[i] == 'E') || (TextInput.text[i] == 'C') || (TextInput.text[i] == 'D')))
                txt += TextInput.text[i];
        }

        TextInput.text = txt;
        SetConfigTextInputValue(TextInput);
    }

    public string GerarRelatorio(bool comCabecalho, bool comLegenda, string formatoData)
    {
        string str ="";
        string dataAtual;

        if (formatoData == "EUA")
            dataAtual = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        else 
            dataAtual = System.DateTime.Now.ToString("dd'-'MM'-'yyyy' 'HH'h 'mm'm 'ss's'");

        if (comCabecalho)
            str = "IDProtocolo;PartidaProtocolo;Data/Hora;Tipo de interface;Tipo de marcacao;Identificador (Nome);Primeiro arremesso da partida;Duracao total da partida (seg);Tempo restante (seg);" +
            "Acerto ou Erro;Placar Jogador (pts);Placar Adversarios (pts);Largura da tela (X) (pixels);" +
            "Altura da tela (Y) (pixels);Coordenada X do toque na tela (pixels);Coordenada Y do toque na tela (pixels);" +
            "Velocidade do jogo;Posicao - Adversario 01 (graus);Posicao - Adversario 02 (graus);" +
            "Posicao - Adversario 03 (graus);Tempo entre arremessos (seg);Posicao anterior do jogador;" +
            "Posicao do jogador na jogada;Nome do Jogo"+ System.Environment.NewLine;

        //Debug.Log("Tamanho da lista: " + VariaveisGlobais.itensRelatorio.Length);

        for (int i = 0; i < VariaveisGlobais.itensRelatorio.Length; i++)
        {
            double tempoEntreJogadas;
            string tipoDeInterfaceStr, tipoDeMarcacaoStr, tempoEntreJogadasStr, tempoRestanteStr, posAntJog, posAtuJog, pos01graus, pos02graus, pos03graus, X_toque, Y_toque;
            //string tipoDePartidaStr;

            if (VariaveisGlobais.itensRelatorio[i].FirstShot)
                tempoEntreJogadas = VariaveisGlobais.itensRelatorio[i].TotalTimeOfTheMatch
                    - VariaveisGlobais.itensRelatorio[i].RemainingTimeAtDouble;
            else
                tempoEntreJogadas = VariaveisGlobais.itensRelatorio[i - 1].RemainingTimeAtDouble
                    - VariaveisGlobais.itensRelatorio[i].RemainingTimeAtDouble;


            switch (VariaveisGlobais.itensRelatorio[i].InterfaceOfMatch)
            {
                case "Mouse":
                    tipoDeInterfaceStr = "1";
                    break;
                case "Webcam1":
                    tipoDeInterfaceStr = "2";
                    break;
                case "Webcam2":
                    tipoDeInterfaceStr = "3";
                    break;
                default:
                    tipoDeInterfaceStr = "";
                    break;
            }

            switch (VariaveisGlobais.itensRelatorio[i].MarkingOfMatch)
            {
                case "Circular":
                    tipoDeMarcacaoStr = "1";
                    break;
                case "Zona":
                    tipoDeMarcacaoStr = "2";
                    break;
                default:
                    tipoDeMarcacaoStr = "";
                    break;
            }


            if (VariaveisGlobais.SimboloDiv == ",")
            {
                tempoRestanteStr = VariaveisGlobais.itensRelatorio[i].RemainingTime.Replace(".", ",");
                tempoEntreJogadasStr = tempoEntreJogadas.ToString("00.00", CultureInfo.InvariantCulture).Replace(".", ",");
            }

            else
            {
                tempoRestanteStr = VariaveisGlobais.itensRelatorio[i].RemainingTime.Replace(",", ".");
                tempoEntreJogadasStr = tempoEntreJogadas.ToString("00.00", CultureInfo.InvariantCulture).Replace(",", ".");
            }


            if (VariaveisGlobais.itensRelatorio[i].FirstShot)// || VariaveisGlobais.itensRelatorio[i].TypeOfMatch == "C")
                posAntJog = "";
            else
                switch (VariaveisGlobais.itensRelatorio[i - 1].Position_E_C_D)
                {
                    case "E":
                        posAntJog = "1";
                        break;
                    case "C":
                        posAntJog = "2";
                        break;
                    case "D":
                        posAntJog = "3";
                        break;
                    default:
                        posAntJog = "";
                        break;
                }

            //if (VariaveisGlobais.itensRelatorio[i].TypeOfMatch == "C")
            //    posAtuJog = "";
            // else
            switch (VariaveisGlobais.itensRelatorio[i].Position_E_C_D)
            {
                case "E":
                    posAtuJog = "1";
                    break;
                case "C":
                    posAtuJog = "2";
                    break;
                case "D":
                    posAtuJog = "3";
                    break;
                default:
                    posAtuJog = "";
                    break;
            }


            //switch (VariaveisGlobais.itensRelatorio[i].TypeOfMatch)
            switch (VariaveisGlobais.itensRelatorio[i].MarkingOfMatch)
            {
                case "Circular":
                    pos01graus = VariaveisGlobais.itensRelatorio[i].Enemy1_Pos.ToString();
                    pos02graus = VariaveisGlobais.itensRelatorio[i].Enemy2_Pos.ToString();
                    pos03graus = VariaveisGlobais.itensRelatorio[i].Enemy3_Pos.ToString();
                    break;
                default:
                    pos01graus = "";
                    pos02graus = "";
                    pos03graus = "";
                    break;
            }

            //if (VariaveisGlobais.itensRelatorio[i].TypeOfMatch == "C" || VariaveisGlobais.itensRelatorio[i].TypeOfMatch == "Z")
            if (VariaveisGlobais.itensRelatorio[i].InterfaceOfMatch == "Mouse")
            {
                X_toque = VariaveisGlobais.itensRelatorio[i].Touch_X.ToString();
                Y_toque = VariaveisGlobais.itensRelatorio[i].Touch_Y.ToString();
            }
            else
            {
                X_toque = "";
                Y_toque = "";
            }

            str = str +
                VariaveisGlobais.itensRelatorio[i].IDProtocolo + ";" + // Identificador do protocolo
                VariaveisGlobais.itensRelatorio[i].IDPartidaProtocolo + ";" + // Qual partida do protocolo foi executada
                VariaveisGlobais.itensRelatorio[i].DateTime + ";" +     //Data/Hora
                tipoDeInterfaceStr + ";" + //Tipo de interface
                tipoDeMarcacaoStr + ";" + // Tipo de marcacao
                                          //tipoDePartidaStr + ";" +     //Tipo de partida 
                VariaveisGlobais.itensRelatorio[i].Player_ID + ";" +    //Identificador (Nome)
                Convert.ToInt32(VariaveisGlobais.itensRelatorio[i].FirstShot) + ";" +    //Primeiro arremesso da partida
                VariaveisGlobais.itensRelatorio[i].TotalTimeOfTheMatch + ";" + //Duracao total da partida(seg)
                tempoRestanteStr + ";" +  //Tempo restante (seg)
                Convert.ToInt32(VariaveisGlobais.itensRelatorio[i].HitTarget) + ";" + //Acerto ou Erro
                VariaveisGlobais.itensRelatorio[i].ScoreBasquito + ";" + //Placar Jogador (pts)
                VariaveisGlobais.itensRelatorio[i].ScoreAdversarios + ";" + //Placar Adversarios (pts)
                VariaveisGlobais.itensRelatorio[i].ScreenSize_X + ";" + //Largura da tela(X) (pixels)
                VariaveisGlobais.itensRelatorio[i].ScreenSize_Y + ";" + //Altura da tela(Y) (pixels)
                X_toque + ";" + //Coordenada X do toque na tela (pixels)
                Y_toque + ";" + //Coordenada Y do toque na tela (pixels)
                VariaveisGlobais.itensRelatorio[i].Velocity + ";" + //Velocidade do jogo
                pos01graus + ";" + //Posicao - Adversario 01 (graus)
                pos02graus + ";" + //Posicao - Adversario 02 (graus)
                pos03graus + ";" + //Posicao - Adversario 03 (graus)
                tempoEntreJogadasStr + ";" + //Tempo entre arremessos (seg)
                posAntJog + ";" + // Posição anterior do jogador
                posAtuJog + ";" + // Posicao do jogador na jogada
                VariaveisGlobais.NomeDoJogo + ";" + // Nome do jogo (Ex: Basquete, Flores, etc.) 
                VariaveisGlobais.itensRelatorio[i].NumJogada + ";" + // Número da jogada (primeira (1), segunda (2), etc.) 
                VariaveisGlobais.itensRelatorio[i].DateTimeInicioPartida + ";" + // Data e hora em que começou a partida registrada.
                VariaveisGlobais.itensRelatorio[i].TipoLance + // Tipo de lance de jogada (se foi arremesso ou penalidade, por exemplo).
                System.Environment.NewLine;  
        }

        if (comLegenda)
            str = str + System.Environment.NewLine + System.Environment.NewLine +
            "Relatorio emitido as " + dataAtual + System.Environment.NewLine + System.Environment.NewLine +
            "- - - - - LEGENDA - - - - -" + System.Environment.NewLine +
            //"- Tipo de partida:" + System.Environment.NewLine +
            "- Tipo de interface:" + System.Environment.NewLine +
            "   1 para 'Interface de mouse ou toque na tela'" + System.Environment.NewLine +
            "   2 para 'Interface via Webcam com exibicao de uma telinha no canto inferior direito'" + System.Environment.NewLine +
            "   3 para 'Interface via Webcam com exibicao da imagem do jogador em sobreposicao a tela do jogo'" + System.Environment.NewLine +
            //"   1 para 'Interface de toque/mouse, adversarios circulando pelo campo'" + System.Environment.NewLine +
            //"   2 para 'Interface de toque/mouse, adversarios com posicao fixa (Esquerda, Central e Direita)'" + System.Environment.NewLine +
            //"   3 para 'Interface via Webcam com exibicao de uma telinha no canto inferior direito, onde o jogador deve levantar o braco ou membro para efetuar arremessos'" + System.Environment.NewLine +
            //"   4 para 'Interface via Webcam com exibicao da imagem do jogador em sobreposicao a tela do jogo, onde o mesmo deve deslocar o braco ou membro ate bolinhas virtuais dispostas na tela para efetuar arremessos'" + System.Environment.NewLine +
            "- Tipo de marcacao:" + System.Environment.NewLine +
            "   1 para 'Marcacao circular: Adversarios circulando pelo campo'" + System.Environment.NewLine +
            "   2 para 'Marcacao por zona: Adversarios com posicao fixa (Esquerda, Central e Direita)'" + System.Environment.NewLine +
            "- Primeiro arremesso da partida:" + System.Environment.NewLine +
            "   1 para SIM" + System.Environment.NewLine +
            "   0 para NAO" + System.Environment.NewLine +
            "- Acerto ou Erro:" + System.Environment.NewLine +
            "   1 para ACERTO" + System.Environment.NewLine +
            "   0 para ERRO" + System.Environment.NewLine +
            "- Velocidade do jogo:" + System.Environment.NewLine +
            "   1 para BAIXA" + System.Environment.NewLine +
            "   2 para MEDIA" + System.Environment.NewLine +
            "   3 para ALTA" + System.Environment.NewLine +
            "   4 para VARIADA" + System.Environment.NewLine +
            "- Posicao - Adversarios 01, 02, 03" + System.Environment.NewLine +
            "   Escala de graus:" + System.Environment.NewLine +
            "   90 para 'Extremo Esquerdo da tela'" + System.Environment.NewLine +
            "   0 para 'Central'" + System.Environment.NewLine +
            "   -90 para 'Extremo Direito da tela'" + System.Environment.NewLine +
            "- Posicao anterior do jogador / Posicao do jogador na jogada:" + System.Environment.NewLine +
            "   1 para ESQUERDA" + System.Environment.NewLine +
            "   2 para CENTRAL" + System.Environment.NewLine +
            "   3 para DIREITA";

        return str;
    }

    public void SalvarRelatorio_CSV()
    {
        string dataAtualForName = System.DateTime.Now.ToString("dd'-'MM'-'yyyy'_'HH'h'mm'm'ss's'");
        WebGLFileSaver.SaveFile(GerarRelatorio(true, true, "BRA"), "Relatorio_" + dataAtualForName + ".csv", "text/csv;charset=utf-8");
    }

}
