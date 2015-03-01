using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Test : MonoBehaviour {

	private AudioSource aSource;
	public float[] samples = new float[128]; 
	public GameObject cube;  
	private Transform goTransform; 
	private Vector3 cubePos;  
	private Transform[] cubesTransform; 
	private Vector3 gravity = new Vector3(0.0f,0.25f,0.0f); 
	public ParticleSystem particles;
	private int currSong = 0;

	public static string NameOfSong;

	private float radius = 0f;



	private AudioClip[] clips;

	List<string> allFiles;

	private float currR;
	private float currG;
	private float currB;
	float lastPlay;
	void Awake ()  
	{  

		this.aSource = GetComponent<AudioSource>();   
		this.goTransform = GetComponent<Transform>();  
		radius = samples.Length / 3f;


		clips = Resources.LoadAll<AudioClip>("mp3");

		aSource.Stop ();
		aSource.clip = clips [currSong];
		aSource.Play ();
		NameOfSong = clips [currSong].name;
	}  


	void Start()  
	{  
		
		cubesTransform = new Transform[samples.Length];  
		goTransform.position = new Vector3(-samples.Length/2,goTransform.position.y,goTransform.position.z);  
		GameObject tempCube;  
		lastPlay = Time.time;
		particles.Stop ();
		//For each sample  
		for(int i=0; i<samples.Length;i++)  
		{  

			float theta = (2 * Mathf.PI/samples.Length) * i;

			float randomOffset = Random.Range(0f, 2f);
			float randomOffset2 = Random.Range(0f, 2f);



			float x = radius * Mathf.Sin(theta) + randomOffset2;
			float z = radius * Mathf.Cos (theta) + randomOffset;

			tempCube = (GameObject) Instantiate(cube, new Vector3(x, goTransform.position.y, z),Quaternion.identity);  
			cubesTransform[i] = tempCube.GetComponent<Transform>();
			//cubesTransform[i].renderer.material.color = new Color(Random.Range (0,255), Random.Range(0,255), Random.Range (0,255));
			tempCube.renderer.material.color = new Color(Random.value,Random.value,Random.value);



			cubesTransform[i].parent = goTransform;  


		}  

	}  



	public void Next()
	{
		if (currSong < clips.Length - 1) {
						currSong ++;
						aSource.Stop ();
						aSource.clip = clips [currSong];
						aSource.Play ();

				} else
						currSong = 0;
						aSource.Stop ();
						aSource.clip = clips [currSong];
						aSource.Play ();
	}

	public void Previous()
	{
		if (currSong > 1) {
						currSong --;
						aSource.Stop ();
						aSource.clip = clips [currSong];
						aSource.Play ();
			
				} else
						currSong = clips.Length - 1;
						aSource.Stop ();
						aSource.clip = clips [currSong];
						aSource.Play ();
	}
	
	void Update ()  
	{  

		aSource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);  
		
		NameOfSong = clips [currSong].name;

		float sum = 0;
		float mean = 0;


		if (Input.GetAxis ("Fire1") != 0)
						Debug.Log ("hi");

		for(int i=0; i<samples.Length;i++)  
		{  



			if (samples[i]*(20+i*i) > 10)
			if (!particles.isPlaying && lastPlay < Time.time + 5f)
				{
					particles.Play();
					Debug.Log("WOW");
				}
			cubePos.Set(cubesTransform[i].position.x, Mathf.Clamp(samples[i]*(20+i*i),0,20), cubesTransform[i].position.z);  
			Color x = cubesTransform[i].renderer.material.color;

			if(cubePos.y >= cubesTransform[i].position.y)  
			{  

				cubesTransform[i].position = cubePos;
			}  
			else  
			{  
				cubesTransform[i].position -= gravity;
			}  
			

		}  
	}


}
