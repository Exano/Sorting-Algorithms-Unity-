using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections;

public class ArrayGUIDisplay : MonoBehaviour {

	public Text textToDisplay;
	public Text timeTakenTextToDisplay;
	ArrayMaster arrayMaster;
	public int resultsPerPage;
	public 	int currentPage;
	public int numberOfPages;

	public bool updateTextDuringUpdate;

	public RectTransform warningText;
 

	//Start the GUI, find our array master and hide the warning that we're computing.
	void Start(){
		hideComputingWarning();
		arrayMaster = GameObject.Find("ArrayMaster").GetComponent("ArrayMaster") as ArrayMaster;
	
	}


	/// <summary>
	/// Bubble sort algorithm. It's just a brute force, mean and slow sort for the array.
	/// In the future, this will be threaded. For now, it's a coroutine so we can display the computing warning at the beginning.
	/// </summary>
	IEnumerator bubbleSort(){
		showComputingWarning();
		yield return new WaitForSeconds(.001f);
		Stopwatch st = startStopwatch();


		for(int i = arrayMaster.ourArray.Length-1; i >= 0; i--){

			for(int k = 1; k <= i; k++){

				if(arrayMaster.ourArray[k-1] > arrayMaster.ourArray[k]){
					int tempInt = arrayMaster.ourArray[k-1];
					arrayMaster.ourArray[k-1] = arrayMaster.ourArray[k];
					arrayMaster.ourArray[k] = tempInt;

				}

			}

		}  

		reportTimeToDisplay("Bubble Sort", stopStopwatch(st));
		updateText();
		hideComputingWarning();
	} 

	/// <summary>
	/// Insertion sort algorithm. It's not brute force, but it's not much better.
	/// In the future, this will be threaded. For now, it's a coroutine so we can display the computing warning at the beginning.
	/// </summary>
	IEnumerator insertionSort(){
		showComputingWarning();
		yield return new WaitForSeconds(.001f); 
		Stopwatch st = startStopwatch();
		for(int i = 1; i < arrayMaster.ourArray.Length; i++){

			int tempIndex = arrayMaster.ourArray[i];
			int k = i;

			while(k > 0 && arrayMaster.ourArray[k-1] > tempIndex){

				arrayMaster.ourArray[k] = arrayMaster.ourArray[k-1];
				k--;
			}

			arrayMaster.ourArray[k] = tempIndex; 

		}

		  
		reportTimeToDisplay("Insertion Sort", stopStopwatch(st));
		updateText();
		hideComputingWarning();
	} 


	/// <summary>
	/// Selection sort algorithm. Terrible with large lists, and worse than insertion sort. 
	/// In the future, this will be threaded. For now, it's a coroutine so we can display the computing warning at the beginning.
	/// </summary>
	IEnumerator selectionSort(){
		showComputingWarning();

		yield return new WaitForSeconds(.001f); 

		Stopwatch st = startStopwatch();

		for(int i = 0; i < arrayMaster.ourArray.Length; i++){
			int minValue = i;

			for(int k = i; k < arrayMaster.ourArray.Length; k++){

				if(arrayMaster.ourArray[minValue] > arrayMaster.ourArray[k]){
					minValue = k;
				}


			}

			int tempVar = arrayMaster.ourArray[i];
			arrayMaster.ourArray[i] = arrayMaster.ourArray[minValue];
			arrayMaster.ourArray[minValue] = tempVar; 

		} 
 
		reportTimeToDisplay("Selection Sort", stopStopwatch(st));
		updateText();
		hideComputingWarning();
	}


	/// <summary>
	/// Shell Sort algorithm. It's really a better selection sort, but it's pretty and way faster. 
	/// </summary> 
	IEnumerator shellSort(){

		showComputingWarning();
		yield return new WaitForSeconds(.001f);
		Stopwatch st = startStopwatch();

		int k; 
		for(int theGap = arrayMaster.ourArray.Length / 2; theGap > 0; theGap /= 2){ // in case you forget, divide and assignment operator is /=  --- IE  c /= a ==  c = c / a . The kind of reason why you hate bitwise

			for (int i = theGap; i < arrayMaster.ourArray.Length; i++){
				int tempValue = arrayMaster.ourArray[i];

				for(k = i; k >= theGap && tempValue.CompareTo( arrayMaster.ourArray[k-theGap] ) < 0 ; k -=theGap){

					arrayMaster.ourArray[k] = arrayMaster.ourArray[k - theGap];
				}
				arrayMaster.ourArray[k] = tempValue;



			}
		}

 
		 
		reportTimeToDisplay("Shell Sort", stopStopwatch(st));
		updateText();
		hideComputingWarning();

	}

	/// <summary>
	/// Quick sort algorithm. Using divide and conquer, it's the first viable sort in this list. 
	/// In the future, this will be threaded. For now, it's a coroutine so we can display the computing warning at the beginning.
	/// </summary>
	IEnumerator quickSortSetup(){

		showComputingWarning();
		yield return new WaitForSeconds(.001f);
		Stopwatch st = startStopwatch();
		 

		int lowNum = 0; 
		int highNum = arrayMaster.ourArray.Length-1;
		quickSort(lowNum, highNum, st);
 
		reportTimeToDisplay("Quick Sort", stopStopwatch(st));
		hideComputingWarning();
		updateText();

	}

	public void quickSort(int lowNum, int highNum, Stopwatch timer){

	 
		if(lowNum >= highNum){
			return;
		}
			
		//pivot picking, it's like cherry's but better
		int middle = lowNum + (highNum- lowNum) / 2;
		int pivot = arrayMaster.ourArray[middle];

		int i = lowNum;
		int k = highNum;

		while(i <= k){
			
			while(arrayMaster.ourArray[i] < pivot){
				i++;
			}

			while(arrayMaster.ourArray[k] > pivot){
				k--;
			}

			if(i <= k){
				int temp = arrayMaster.ourArray[i];
				arrayMaster.ourArray[i] = arrayMaster.ourArray[k];
				arrayMaster.ourArray[k] = temp;
				i++;
				k--;
			}

		}

		if(lowNum < k){
			quickSort(lowNum, k, timer);
		} 
		if(highNum > i){
			quickSort(i, highNum, timer);
		}
	}




	/// <summary>
	/// Starts the bubble sort method. In the future, please use threading here
	/// </summary>
	public void startBubbleSort(){
		// we do this until we get threading implemented later this week
		consoleLog("Beginning Bubble Sort");
		StartCoroutine(bubbleSort());
	}

	/// <summary>
	/// Starts the insertion sort algorithm.
	/// </summary>
	public void startInsertionSort(){
		consoleLog("Beginning Insertion Sort");

		StartCoroutine(insertionSort());
	}
	/// <summary>
	/// Starts the selection sort algorithm.
	/// </summary>
	public void startSelectionSort(){

		consoleLog("Beginning Selection Sort");
		StartCoroutine(selectionSort());
	}


	/// <summary>
	/// Starts the shell sort algorithm.
	/// </summary>
	public void startShellSort(){
		consoleLog("Beginning Shell Sort");
		StartCoroutine(shellSort());

	}
	/// <summary>
	/// Starts the quick sort.
	/// </summary>
	public void startQuickSort(){
		consoleLog("Beginning Quick Sort");
		StartCoroutine(quickSortSetup());
	}


	/// <summary>
	/// Starts the stopwatch.
	/// </summary>
	/// <returns>The stopwatch we've started.</returns>
	//to make this thread compatable later on
	Stopwatch startStopwatch(){
		Stopwatch ST = new Stopwatch();
		ST.Start();

		return ST;
	}

	/// <summary>
	/// Stops the stopwatch.
	/// </summary>
	/// <returns>The stopwatch we stopped time elapsed.</returns>
	/// <param name="toStop">The stopwatch to stop.</param>
	long stopStopwatch(Stopwatch toStop){
		toStop.Stop();
		return toStop.ElapsedMilliseconds;
	}
 
	/// <summary>
	/// Lists the array -- gets number of pages, sets current page to 0, and updates the text
	/// </summary>
	public void listArray(){
		numberOfPages = Mathf.CeilToInt(arrayMaster.ourArray.Length/resultsPerPage);
		currentPage = 0;
		updateText();
	}
	 

	/// <summary>
	/// Reports the time/algorithm to the console and the algorithm time text display
	/// </summary>
	/// <param name="algorithmName">Algorithm name.</param>
	/// <param name="timeTakenForAlgorithm">Time taken for algorithm.</param>
	void reportTimeToDisplay(string algorithmName, long timeTakenForAlgorithm){
		string formattedLength =  arrayMaster.ourArray.Length.ToString("N0");
		timeTakenTextToDisplay.text = algorithmName + " took " + timeTakenForAlgorithm + "ms to complete.\n\nArranged " + formattedLength + " numbers";
		consoleLog("Finished " + algorithmName);
	} 

	/// <summary>
	/// Log a string to the GUI console.
	/// </summary>
	/// <param name="toLog">String to log to the console.</param>
	void consoleLog(string toLog){
		arrayMaster.guiConsole.addLine(toLog);
	}


	/// <summary>
	/// Hides the 'computing XX' warning.
	/// </summary>
	void hideComputingWarning(){
		warningText.transform.position = new Vector3(12500,0,0); // get it out of the way, it's going to 0,0,0 and it's only used as a warning. In your projects, you can remove this, it's just to illustrate what's happening since I didn't want to create animations and the like.

	}

	/// <summary>
	/// Shows the computing warning.
	/// </summary>
	void showComputingWarning(){
		warningText.transform.localPosition = new Vector3(0,0,0);  
	}

	/// <summary>
	/// Turns array to next page. Updates the text.
	/// </summary>
	public void nextPage(){
		currentPage++;
		if(currentPage > numberOfPages){
			currentPage = 0;
		}
		updateText();
	}

	/// <summary>
	/// Turns array to previous page. Updates the text.
	/// </summary>
	public void previousPage(){
		currentPage--;
		if(currentPage < 0){
			currentPage = numberOfPages;
		}
		updateText();
	}

	/// <summary>
	/// Moves the array up by one.
	/// </summary>
	public void shiftArrayUp(){

		int originalPosition = arrayMaster.ourArray[0];

		for(int i = 0; i < arrayMaster.ourArray.Length-1; i++){
			arrayMaster.ourArray[i] = arrayMaster.ourArray[i+1];
		}

		arrayMaster.ourArray[arrayMaster.ourArray.Length-1] = originalPosition;
		updateText();
	}

	/// <summary>
	/// Moves the array down by one.
	/// </summary>
	public void shiftArrayDown(){		
		int originalPosition = arrayMaster.ourArray[arrayMaster.ourArray.Length-1]; // last element of array

		for(int i = arrayMaster.ourArray.Length-1; i > 0; i--){
			arrayMaster.ourArray[i] = arrayMaster.ourArray[i-1];
		}

		arrayMaster.ourArray[0] = originalPosition;
		updateText();
	} 


	/// <summary>
	/// Updates the results text using the array from arrayMaster.
	/// </summary>
	void updateText(){
		textToDisplay.text = "";
		 
		for(int i = currentPage*resultsPerPage; i < (currentPage+1)*resultsPerPage; i++){
			if(i < arrayMaster.ourArray.Length){
				textToDisplay.text += (i+1) + " - " + arrayMaster.ourArray[i] + "\n";
			}
		}
	}

}
