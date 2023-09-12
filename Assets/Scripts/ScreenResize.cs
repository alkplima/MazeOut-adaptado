using UnityEngine;
using UnityEngine.Events;

public class ScreenResize : MonoBehaviour{

    public UnityEvent onResize;
    private Vector2 resolution;
 
    private void Awake(){
        resolution = new Vector2(Screen.width, Screen.height);
    }
    
    private void LateUpdate(){
        if( resolution.x != Screen.width || resolution.y != Screen.height ){
            
            onResize.Invoke();
    
            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }
    }
    
}
