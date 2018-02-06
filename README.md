# BGL Life Remote Technical Test

## The Weather App
### Introduction
Your task is to build a web application which will allow users to enter a location and view details about the weather in that area. The design of the application is up to you, as is the choice of weather API - however, we have provided an API key for OpenWeatherMap which you are free to use if you wish. This key will have been emailed to you by our Tech Recruitment Team.

There isn’t a strict time limit, but we would appreciate you letting us know roughly how long the solution took you to code. Once completed, we will review and then arrange a video conference where you can meet the rest of the team and we’ll review your solution informally as a group.

Any problems, email our Tech Recruitment Team on techjobs@bglgroup.co.uk

### Acceptance Criteria

| Given                              	| When                      	| Then                                               	|
|------------------------------------	|---------------------------	|----------------------------------------------------	|
| A working application              	| The home page loads       	| I can enter a location into a text field           	|
| A location field                   	| I enter a location        	| I can click a search button                        	|
| I have entered a valid location    	| I click the search button 	| I am shown details of the weather in that location 	|
| I have entered an invalid location 	| I click the search button 	| I am shown a suitable error message               	|
 
### Assumptions
* The provided solution will be
	* ASP.Net MVC, .Net Framework or Core
	* Fully tested
  * Loosely-coupled
  * Able to show SOLID principles at work, where relevant
* The primary aim of the test is to indicate capability in C# coding, software design and development, however feel free to use CSS and JavaScript if you wish to add flair to the presentation layer
* All references should be added as Nuget Packages and should be publicly accessible on nuget.org
* The weather information displayed should include the following elements, if they are available from the chosen API
  * Location name
  * Temperature
    * Current
    * Maximum
    * Minimum
  * Pressure
  * Humidity
  * Sunrise
  * Sunset

### Notes
Example weather API:
* https://openweathermap.org/api
