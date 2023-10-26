/*
	Detecção de movimento com Webcam
	Desenvolvido por Murillo Brandão
	Sob orientação do Prof. Dr. Luciano Vieira de Araújo
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Webcam : MonoBehaviour {

	/*
		PARAMETROS
		Display - RawImage para exibir o blend
		threshold - sensibilidade da detecção
		blendColor - cor de exibição da detecção
	 */


	public RawImage Display, Display2;

	[Range(0, 255)]
	public int threshold = 30;
	[Range(0f, 1f)]
	public float density = .02f;

	public Color32 blendColor = new Color32(255,255,255,255);


	bool initialized = false;
	public WebCamTexture webcamTexture;
	Texture2D blendTexture, copyTexture;
	int diffsum = 0;
	int nullCount = 0;
	float lastAspectRatio;

	Color32[] lastData = null;
	Color32[] bufferData_H = null;
	Color32[] bufferData_V = null;
	Color32[] blendData = null;
	Color32[] checkData = null;

	[HideInInspector]
	public float scaleH = 1f, scaleV = 1f;
	// Escala da webcam com relação ao display

	Coroutine cr;
	bool crIsRunning = true;

	IEnumerator Start() {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);
        if( Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone) ){
			cr = StartCoroutine(StartWebcam());
        }
    }

	/*
		StartWebcam
		Inicializa a webcam e texturas
	 */
	IEnumerator StartWebcam(){
		crIsRunning = true;

        WebCamDevice[] devices = WebCamTexture.devices;

		if (VariaveisGlobais.idWebcam >= WebCamTexture.devices.Length)
			VariaveisGlobais.idWebcam = 0;

        webcamTexture = new WebCamTexture(devices[VariaveisGlobais.idWebcam].name, 320, 240, 15);
        webcamTexture.Play();

        //Debug.Log(webcamTexture.width+"x"+webcamTexture.height);

        int thresholdTries = 0;

        while (webcamTexture.width <= 16 && thresholdTries < 1000)
        {
            while (!webcamTexture.didUpdateThisFrame)
            {
                yield return new WaitForEndOfFrame();
            }
            webcamTexture.Pause();
            Color32[] colors = webcamTexture.GetPixels32();
            webcamTexture.Stop();

            yield return new WaitForEndOfFrame();

            webcamTexture.Play();
            thresholdTries++;
            //Debug.Log(webcamTexture.width + "x" + webcamTexture.height);
        }


        blendTexture = new Texture2D( webcamTexture.width, webcamTexture.height );
        copyTexture = new Texture2D(webcamTexture.width, webcamTexture.height);

        if ( Display ){
			Display.texture = blendTexture;
			scaleH = webcamTexture.width / Display.rectTransform.rect.width;
			scaleV = webcamTexture.height / Display.rectTransform.rect.height;
		}

        if (Display2)
        {
            Display2.texture = copyTexture;
        }

			lastAspectRatio = Camera.main.aspect;

		initialized = true;
		crIsRunning = false;
	}


	/*
		UPDATE
		Todo frame calcula a diferença de frames e atualiza a textura
	 */
	void Update()
	{
		// Aguarda inicialização e inicializa variaveis
		if( !initialized || webcamTexture.width == 0 ) return;
		if( lastData == null ){
			lastData = webcamTexture.GetPixels32();

			if (VariaveisGlobais.Webcam_espelhar_H)
            {
				bufferData_H = new Color32[lastData.Length];
				lastData.CopyTo(bufferData_H, 0);
				for (int i = 0; i < webcamTexture.width; i++)
					for (int j = 0; j < webcamTexture.height; j++)
						lastData[i + (webcamTexture.width * j)] = bufferData_H[((i - (webcamTexture.width - 1)) *(-1)) + (webcamTexture.width * j)];
			}
			
			if (VariaveisGlobais.Webcam_espelhar_V)
			{
				bufferData_V = new Color32[lastData.Length];
				lastData.CopyTo(bufferData_V, 0);
				for (int i = 0; i < webcamTexture.width; i++)
					for (int j = 0; j < webcamTexture.height; j++)
						lastData[i + (webcamTexture.width * j)] = bufferData_V[i + (webcamTexture.width * ((j - (webcamTexture.height - 1)) *(-1)))];
			}


			blendData = new Color32[ lastData.Length ];
			checkData = new Color32[ lastData.Length ];
			for(int i=0; i<lastData.Length; i++){
				blendData[i] = new Color32(0,0,0,0);
				checkData[i] = new Color32(0,0,0,0);
			}
		}

		if ((Camera.main.aspect != lastAspectRatio) && (Display  != null))
		{
			scaleH = webcamTexture.width / Display.rectTransform.rect.width;
			scaleV = webcamTexture.height / Display.rectTransform.rect.height;
		}

		Difference();

		// Pula frames identicos (fps)
		if( diffsum == 0 ){
			if( nullCount++ < 5 ) return;
		}
		else nullCount = 0;


		// Atualiza textura e vetor de checagem
		for(int i=0; i<blendData.Length; i++){
			checkData[i] = blendData[i];
		}

		blendTexture.SetPixels32( blendData );
		blendTexture.Apply();
        copyTexture.SetPixels32(lastData);
        copyTexture.Apply();
    }

	/*
		DIFFERENCE
		Calcula a diferença entre o frame atual e o frame anterior
		Coloca a diferença em blendData
	*/
	void Difference(){
		Color32[] actualData = webcamTexture.GetPixels32();


		if (VariaveisGlobais.Webcam_espelhar_H)
		{
			bufferData_H = new Color32[actualData.Length];
			actualData.CopyTo(bufferData_H, 0);
			for (int i = 0; i < webcamTexture.width; i++)
				for (int j = 0; j < webcamTexture.height; j++)
					actualData[i + (webcamTexture.width * j)] = bufferData_H[((i - (webcamTexture.width - 1)) * (-1)) + (webcamTexture.width * j)];
		}

		if (VariaveisGlobais.Webcam_espelhar_V)
		{
			bufferData_V = new Color32[actualData.Length];
			actualData.CopyTo(bufferData_V, 0);
			for (int i = 0; i < webcamTexture.width; i++)
				for (int j = 0; j < webcamTexture.height; j++)
					actualData[i + (webcamTexture.width * j)] = bufferData_V[i + (webcamTexture.width * ((j - (webcamTexture.height - 1)) * (-1)))];
		}

		diffsum = 0;

		for(int i=0, len=actualData.Length; i<len; i++){
			int a = ( (actualData[i].r + actualData[i].g + actualData[i].b) / 3) - 
				( (lastData[i].r + lastData[i].g + lastData[i].b) / 3);

			if( (a^a>>31)-(a>>31) > threshold ){
				blendData[i] = blendColor;
				diffsum += 1;
			}
			else{
				blendData[i] = new Color(0, 0, 0, 0);
			}
		}

		lastData = actualData;
	}

	/*
		CHECKAREA
		Checa se há interação em uma região da webcam
		retorna true ou false
	 */
	public bool checkArea( int x, int y, int width, int height ){
		if( !initialized || checkData == null ) return false;
		
		int sum = 0;
		for(int i=0; i<width*height; i++){
			int tx = x + i%width;
				tx = webcamTexture.width - tx;
            int ty = y + (int)Mathf.Floor(i/width);
				ty = webcamTexture.height - ty;
            int p = Mathf.Max((webcamTexture.width * ty) + tx - webcamTexture.width-1,0);
                sum += (checkData[p].a > 0) ? 1 : 0;
        }

		float d = ((float) sum)/((float) (width*height));
		
		return ( d > density );
	}

    private void OnDestroy()
    {
        //Debug.Log("Destruindo");
		if (webcamTexture)
            webcamTexture.Stop();
		initialized = false;
	}
	public void RenewAccess()
    {
		initialized = false;
		if (webcamTexture.isPlaying)
			webcamTexture.Stop();
		StartCoroutine(Start());
    }

	public void ChangeIdWebcam()
    {
		if (VariaveisGlobais.idWebcam + 1 >= WebCamTexture.devices.Length)
			VariaveisGlobais.idWebcam = 0;
		else 
			VariaveisGlobais.idWebcam++;
		RenewAccess();	
	}

	public void StopWebcam()
	{
		initialized = false;
		if (webcamTexture)
            webcamTexture.Stop();
	}

	private void OnDisable()
    {
		StopWebcam();
    }

	private void OnEnable()
   {
		if (!crIsRunning)
			StartCoroutine(Start());

        PaterlandGlobal.currentWebcam = this;
    }


}