﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        AudioManager.Instance.Init();
        UIManager.Instance.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
