using UnityEngine;
using System.Collections;

public class ArrayMaster : MonoBehaviour {

	public int maxIntSize = 1000000000; //Max size for random numbers
	public int[] ourArray; // the array we're using
	public GUIConsole guiConsole; // the gui console to log strings to 
	public ArrayGUIDisplay arrayGUIDisplay;  
	public ButtonMaster buttonMaster;

	/// <summary>
	/// Logs to the GUI console. GUI Console configured inside guiConsole gameObject in the main scene.
	/// </summary>
	/// <param name="toLog">String to display to console.</param>
	void logToConsole(string toLog){
		if(guiConsole){
			guiConsole.addLine(toLog);
		}
	}


	/// <summary>
	/// Creates an array based on parameters assigned (arraySize)
	/// Proceeds to randomly create a random array of numbers
	/// In the future, this class will be threaded so we can create a bunch of arrays containing the same numbers with different sorts
	/// </summary>
	/// <param name="arraySize">Array size.</param>
	public void createArray(int arraySize){
		ourArray = new int[arraySize];
		assignRandomArrayNumbers();
		arrayGUIDisplay.listArray();
		buttonMaster.showAndEnableButtons();
	}

	/// <summary>
	/// Assigns the array random numbers.
	/// </summary>
	void assignRandomArrayNumbers(){
		for(int i = 0; i < ourArray.Length; i++){
			ourArray[i] = Random.Range(0,maxIntSize);
		}
	}










}
