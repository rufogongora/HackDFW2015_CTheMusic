using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameOfSong : MonoBehaviour {

	Text theText;
	// Use this for initialization
	void Start () {
		theText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		theText.text = "Name of the song: " + Test.NameOfSong;
	}
}
