using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class WebCamPhotoCamera : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public RawImage rawImage;
    public Button takePhotoButton;
	public Texture2D photo;

	public GameObject resultados;
	public Text countText;
    
	private bool photoTaken;
    private bool ready;
	int photoCount = 3;
	int puntosCount;

    void Start()
    {       

		webCamTexture = new WebCamTexture();

		//webCamTexture.requestedHeight = 1280;
		//webCamTexture.requestedWidth = 720;
		//webCamTexture = new WebCamTexture(WebCamTexture.devices[0].name, (int)1280, (int)720, 30);

		if (webCamTexture.isPlaying)
		{
			webCamTexture.Stop();
		} else
			webCamTexture.Play();

        Vector3 scale = rawImage.transform.localScale;
        
#if UNITY_IOS
        scale.x *= -1;
       rawImage.transform.localEulerAngles = new Vector3(0, 0, 180);
#endif
        rawImage.transform.localScale = scale;

		/*float scaleY = webCamTexture.videoVerticallyMirrored ? -1.0f : 1.0f;
		rawImage.transform.localScale = new Vector3(webCamTexture.width, scaleY * webCamTexture.height, 0.0f);*/

		Events.DeleteFrame += NewPhoto;
		Events.NewPhoto += NewPhoto;

		//Invoke ("TurnOnCamera", 0.1f);
    }

	void TurnOnCamera(){		

		webCamTexture = new WebCamTexture();
		//webCamTexture.requestedHeight = 1280;
		//webCamTexture.requestedWidth = 720;
		//webCamTexture = new WebCamTexture(WebCamTexture.devices[0].name, (int)1280, (int)720, 30);

		if (webCamTexture.isPlaying)
		{
			webCamTexture.Stop();
		} else
			webCamTexture.Play();
	}

	void NewPhoto(){
		photoTaken = false;
		takePhotoButton.gameObject.SetActive(true);
	}

    void Update()
    {
        if (!photoTaken)
        {            
			rawImage.texture = webCamTexture;
        }
    }

    void OnDestroy()
    {
        webCamTexture.Stop();
		Events.DeleteFrame -= NewPhoto;
		Events.NewPhoto -= NewPhoto;
    }

	public void PhotoReady(){
		countText.transform.gameObject.SetActive (true);
		countText.text = photoCount + "";
		Invoke ("PuntosCount", 0.25f);
	}

	void CountDown(){		
		photoCount--;
		if (photoCount > 0) {
			Invoke ("PuntosCount", 0.25f);
		} else {
			photoCount = 3;
			TakePhoto ();
		}
		countText.text = photoCount+"";
	}

	void PuntosCount(){		
		countText.text += ".";
			puntosCount++;
		if (puntosCount > 2 && photoCount > 0) {
			Invoke ("CountDown", 0.25f);
			puntosCount = 0;
		} else if (photoCount > 0) {
			Invoke ("PuntosCount", 0.25f);
		} else {
			photoCount = 3;
			TakePhoto ();
		}	
	}


    public void TakePhoto()
    {
		countText.transform.gameObject.SetActive (false);
        photoTaken = true;
        takePhotoButton.gameObject.SetActive(false);

       	if (Input.deviceOrientation == DeviceOrientation.Portrait){
			Texture2D temp = new Texture2D(webCamTexture.width, webCamTexture.height);
			temp.SetPixels(webCamTexture.GetPixels());
			temp.Apply();				
			Texture2D rotated = TextureUtils.Rotate90CW(temp);
			photo = new Texture2D(rotated.width, rotated.height);
			photo.SetPixels(rotated.GetPixels());
		}else if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown){
			Texture2D temp = new Texture2D(webCamTexture.width, webCamTexture.height);
			temp.SetPixels(webCamTexture.GetPixels());
			temp.Apply();				
			Texture2D rotated = TextureUtils.Rotate90CCW(temp);
			photo = new Texture2D(rotated.width, rotated.height);
			photo.SetPixels(rotated.GetPixels());
		}else{
			photo = new Texture2D(webCamTexture.width, webCamTexture.height);
			photo.SetPixels(webCamTexture.GetPixels());
		}
        
		photo.Apply();
        
		rawImage.texture = photo;

		resultados.SetActive (true);
    }

}