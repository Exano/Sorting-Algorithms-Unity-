using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateArrayHandler : MonoBehaviour {

	ArrayMaster arrayMaster;
	public InputField inputField;
	public Button inputFieldButton;

	//Find the array master at the beginning
	void Start () {
		arrayMaster = GameObject.Find("ArrayMaster").GetComponent("ArrayMaster") as ArrayMaster;
	} 

	/// <summary>
	/// Tells the array master to create the array based on parameter in the input field, logs to console, clears input field.
	/// </summary>
	public void createArray(){ 
			arrayMaster.createArray(int.Parse(inputField.text));
			inputField.text = " "; 
			arrayMaster.guiConsole.addLine("Created array with " + arrayMaster.ourArray.Length.ToString("N0") + " elements");
	}

	/// <summary>
	/// The update for this instance is just turning the 'create array' button on or off, depending on if there is any text
	/// In the future, this really shouldn't be in the update method. Just check when input field gets interacted with and what's inside it, and call the following
	/// </summary>
	void Update(){
		if(inputField.text == ""){
			inputFieldButton.interactable = false;
		} else{
			inputFieldButton.interactable = true;
		}
	}
}
