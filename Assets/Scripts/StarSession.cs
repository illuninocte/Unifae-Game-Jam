﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarSession : MonoBehaviour{

	public void LoadLevel(){
		SceneManager.LoadScene(1);		
	}

	public void QuitGame(){
		Application.Quit();
	}
}