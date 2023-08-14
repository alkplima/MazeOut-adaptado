using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class LoadVideoStreamingAssets : MonoBehaviour {
	public string VideoName;
	public RenderTexture targetRenderTexture;
	public GameObject imgWaiting;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnEnable()
    {
		if (imgWaiting) 
			imgWaiting.SetActive(true);

		gameObject.AddComponent<VideoPlayer>();

		gameObject.GetComponent<VideoPlayer>().source = VideoSource.Url;
		gameObject.GetComponent<VideoPlayer>().playOnAwake = true;
		gameObject.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.RenderTexture;
		gameObject.GetComponent<VideoPlayer>().aspectRatio = VideoAspectRatio.Stretch;
		gameObject.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.None;
		gameObject.GetComponent<VideoPlayer>().isLooping = true;

		gameObject.GetComponent<VideoPlayer>().targetTexture = targetRenderTexture;

		gameObject.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath,VideoName);
		gameObject.GetComponent<VideoPlayer>().Stop();
		gameObject.GetComponent<VideoPlayer>().Prepare();
		gameObject.GetComponent<VideoPlayer>().Play();

		if (imgWaiting)
			StartCoroutine(loadingIcon());
	}

	private void OnDisable()
	{
		Destroy(gameObject.GetComponent<VideoPlayer>());
	}

	IEnumerator loadingIcon()
	{
		while (!gameObject.GetComponent<VideoPlayer>().isPlaying)
			yield return new WaitForEndOfFrame();

		imgWaiting.SetActive(false);
	}
}
