using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIConsole : MonoBehaviour {
	//GUIConsole is a useful script for creating a 'fake' console system. Pass messages, they'll appear on the Text in the GUI like a traditional console.


	//when designing a console using this script, the text font size and position are decided in the editor, not this script.
	public Text guiText; // set the text you want to use for the console
	public int maxLines = 6; // and the max lines


	string[] consoleLines; // where we're storing the console lines for now. There are alternative ways to handle this, but I like the string array as long as the elements are kept small
 


	void Start () {
		consoleLines = new string[maxLines]; // new array based on the users definition for maximum line amount . 
		addLine("Waiting for user to create array.");
	}

	public void addLine(string toAdd){
		
		for(int i = consoleLines.Length-2; i >= 0; i--){ // start at the top, work our way down to shift the array elements up by one
			consoleLines[i+1] = consoleLines[i];
		}
		consoleLines[0] = toAdd; //add the latest element to the top of the list
		updateText(); //and refresh the text 
	}

	public void wipeConsole(){
		consoleLines = new string[maxLines]; // new array based on the users definition for maximum line amount . 
		updateText();
	}

	//update the Text portion of the console.
	void updateText(){
		guiText.text = ""; //set console text to be blank 
		for(int i = 0; i < consoleLines.Length; i++){
			guiText.text+=consoleLines[i]+"\n";//add line break
		}
	}



}
