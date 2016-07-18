A basic application that will eventually be the basis for a tutorial series, I made this in a couple hours over the weekend in attempts to show how the different sorting methods work (or don't work) when numbers start to scale up.
I've put in a few of the ones I want, but theres still a few things left to be done:

Threading -- Right now everything's on the main unity game thread, which is not great.
Comparison -- Instead of one algorithm at a time, I'm going to have a checkbox and let you compare algorithms against one another asyncronously
Cleaner GUI -- Just a general clean up of the GUI and make it look nice.
Different Starting Set -- Comparing just random numbers isn't comparable to most scenarios we'll be facing, so we need to create arrays with the following; 
	-Random (What we have now) -- Totally randomly generated numbers.
	-Nearly Sorted--The elements are close, but not quite there. Maybe it was split into chunks previously.
	-Reverse Sorted--Elements are sorted backwards.
	-Handful Unique--A few elements are out of place, most are OK.
Smart Filtering -- Lock out buttons based on array length -- IE, Don't allow bubble sorting when we are greater than a million. 