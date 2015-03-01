using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeBox : MonoBehaviour {


	Text textBox;

	// Use this for initialization
	void Start () {
		textBox = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		textBox.text = DateTime.Now.ToLongDateString () + Environment.NewLine;
		textBox.text += DateTime.Now.ToShortTimeString();

	}
}
