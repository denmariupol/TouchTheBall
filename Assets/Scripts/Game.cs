using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    private OneTouch oneTouch;
    private Touch touch;

    public GameObject touchPointPrefab;
	void Start ()
    { 
        Instantiate(touchPointPrefab,new Vector2(100,100),Quaternion.identity);
        oneTouch = new OneTouch();
        oneTouch.Init();
	}
	
	// Update is called once per frame
	void Update ()
    {
         oneTouch.MouseTouch();

	}
}
