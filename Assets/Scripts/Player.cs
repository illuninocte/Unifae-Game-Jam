﻿/*Created by CaelumLaron ~ 04/09/2019*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public class Player : MonoBehaviour{
	
	[Header("Movement Settings")]
	[SerializeField] float walkSpeed = .3f;
	[SerializeField] float groundRunSpeed = 8f;
	[SerializeField] float airRunSpeed = 3f;
	
	[Header("Jump Settings")]
	[SerializeField] float jumpForce = 10f;
	[SerializeField] float jumpTime = 1f;

	//Cached Objects
	Rigidbody2D rg;
	BoxCollider2D bx2d;
	CircleCollider2D c2d;

	//Cached Variable
	float inputX, jumpCounter;
	bool stopPlayer = false;
	bool standing = false, jumpInput, runInput, jumping = false;

	void Start(){
		bx2d = GetComponent<BoxCollider2D>();
		c2d = GetComponent<CircleCollider2D>();
		rg = GetComponent<Rigidbody2D>();
		Hazard.onDeath += EndGame;
	}

	void FixedUpdate(){
		if(stopPlayer)
    		return;
		runInput = Input.GetKey(KeyCode.LeftShift);
    	inputX = Input.GetAxisRaw("Horizontal");
    	if(standing){
    		rg.velocity = new Vector2(inputX * (runInput? groundRunSpeed : walkSpeed), rg.velocity.y);
    	}
		else{
			if(Mathf.Abs(rg.velocity.x) > airRunSpeed){
				rg.velocity = new Vector2(inputX * groundRunSpeed, rg.velocity.y);
			}
			else{
				rg.velocity = new Vector2(inputX * airRunSpeed, rg.velocity.y);
			}
		}
	}

    void Update(){
    	if(stopPlayer)
    		return;
    	standing = c2d.IsTouchingLayers(LayerMask.GetMask("Ground"));
    	if(standing && Input.GetButtonDown("Jump")){
    		rg.velocity = Vector2.up * jumpForce;
    		jumpCounter = jumpTime;
    		jumping = true;
    	}
    	if(jumping && Input.GetButton("Jump")){
    		if(jumpCounter > 0){
    			rg.velocity = Vector2.up * jumpForce;
    			jumpCounter -= Time.deltaTime;
    		}
    		else{
    			jumping = false;
    		}
    	}
    	if(Input.GetButtonUp("Jump")){
    		jumping = false;
    	}

    }

    void EndGame(){
    	Debug.Log("Player Died!");
    	stopPlayer = true;
    	rg.velocity = Vector2.zero;
    	Hazard.onDeath -= EndGame;
    }

}
