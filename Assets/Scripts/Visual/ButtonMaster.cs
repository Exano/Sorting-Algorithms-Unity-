using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonMaster : MonoBehaviour {

	public Button[] buttonsToDisable;
	RectTransform[] buttonRectTransforms;
	public Text resultsTextToClear;
	Vector3[] originalPositions;
	public Vector3 targetEndPosition;

	public Transform holderTransform;
	public RectTransform holderRectTransform;
	Vector3 holderOriginalPosition;


	public float buttonMovementSpeed;
	public float movementEndYPositionSubtrationAmount = 300;
 
	//Get the end position of the buttons/GUI elements
	//Get their rectTransforms
	//Get their original positions
	//Disable all the algorithm buttons until an array is created
	void Start () {
		
		targetEndPosition = buttonsToDisable[0].transform.localPosition;
		targetEndPosition.y-=movementEndYPositionSubtrationAmount;

		getRectTransforms();

		originalPositions = new Vector3[buttonsToDisable.Length];
		getOriginalPositions();

		InstantlyDisableAll();
	}

	/// <summary>
	/// Gets the rect transforms of the GUI items and buttons
	/// </summary>
	void getRectTransforms(){
		holderRectTransform = holderTransform.GetComponent<RectTransform>();
		buttonRectTransforms = new RectTransform[buttonsToDisable.Length];
		for(int i = 0; i < buttonRectTransforms.Length; i++){
			buttonRectTransforms[i] = buttonsToDisable[i].GetComponent<RectTransform>();
		}
	}

	/// <summary>
	/// Hides and disables the buttons, moves them down.
	/// </summary>
	public void hideAndDisableButtons(){
		StopAllCoroutines();
		disableAllButtons();
		StartCoroutine(moveButtonsDown());
	}

	/// <summary>
	/// Shows and enables the buttons, moves them up.
	/// </summary>
	public void showAndEnableButtons(){
		StopAllCoroutines();
		StartCoroutine(moveButtonsUp());
		enableAllButtons();
	}

	/// <summary>
	/// Instantly disables all the buttons (without animations).
	/// </summary>
	void InstantlyDisableAll(){
		disableAllButtons();
		setAllButtonsDown();
	}

	/// <summary>
	/// Gets the original positions of all the objects.
	/// </summary>
	void getOriginalPositions(){
		for(int i = 0; i < buttonsToDisable.Length; i++){
			originalPositions[i] = buttonRectTransforms[i].localPosition;
		}
		holderOriginalPosition = holderRectTransform.transform.localPosition;

	}

	/// <summary>
	/// Disable all the buttons (make them uninteractable)
	/// </summary>
	void disableAllButtons(){
		for(int i = 0; i < buttonsToDisable.Length; i++){
			buttonsToDisable[i].interactable = false;
		}
	}

	/// <summary>
	/// Enables all buttons. Makes them interactable.
	/// </summary>
	void enableAllButtons(){
		for(int i = 0; i < buttonsToDisable.Length; i++){
			buttonsToDisable[i].interactable = true;
		}
	}

	/// <summary>
	/// Tells if all objects are at the bottom to stop any coroutine
	/// </summary>
	/// <returns><c>true</c>, if at bottom was true, <c>false</c> otherwise.</returns>
	bool allAtBottom(){
		bool allAccounted = true;

		for(int i = 0; i < buttonsToDisable.Length; i++){
			if(buttonRectTransforms[i].position != targetEndPosition){
				allAccounted = false;
			}
			if(holderRectTransform.localPosition != targetEndPosition){
				allAccounted = false;
			}

		}

		return allAccounted;
	}


	/// <summary>
	/// Tells if all objects are at the top to stop any coroutine
	/// </summary>
	/// <returns><c>true</c>, if at bottom was true, <c>false</c> otherwise.</returns>
	bool allAtTop(){
		bool allAccounted = true;

		for(int i = 0; i < buttonsToDisable.Length; i++){
			if(buttonRectTransforms[i].position != originalPositions[i]){
				allAccounted = false;
			}

			if(holderRectTransform.localPosition != holderOriginalPosition){
				allAccounted = false;
			}
		}

		return allAccounted;
	}

	/// <summary>
	/// Moves the buttons up (Animation).
	/// </summary> 
	IEnumerator moveButtonsUp(){
		while(!allAtTop()){

			holderRectTransform.localPosition = Vector3.MoveTowards(holderRectTransform.localPosition, holderOriginalPosition, buttonMovementSpeed*Time.deltaTime);

			for(int i = 0; i < buttonsToDisable.Length; i++){
				buttonRectTransforms[i].localPosition = Vector3.MoveTowards(buttonRectTransforms[i].localPosition, originalPositions[i], buttonMovementSpeed*Time.deltaTime);
			}
				yield return new WaitForSeconds(0.01f);
		}
	}

	/// <summary>
	/// Moves the buttons down (Animation).
	/// </summary> 
	IEnumerator moveButtonsDown(){
		while(true){

			holderRectTransform.localPosition = Vector3.MoveTowards(holderRectTransform.localPosition, targetEndPosition*2, buttonMovementSpeed*Time.deltaTime);

			for(int i = 0; i < buttonsToDisable.Length; i++){
				buttonRectTransforms[i].localPosition = Vector3.MoveTowards(buttonRectTransforms[i].localPosition, targetEndPosition, buttonMovementSpeed*Time.deltaTime);
			}

			yield return new WaitForSeconds(0.01f);
		}
	}

	/// <summary>
	/// Moves the buttons down (non-animation).
	/// </summary> 
	public void setAllButtonsDown(){
		for(int i = 0; i < buttonsToDisable.Length; i++){
			buttonRectTransforms[i].localPosition = targetEndPosition;
		}
		holderRectTransform.localPosition = targetEndPosition;

	}

}


