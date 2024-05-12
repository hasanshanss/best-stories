# Best Stories API
RESTful API for retrieving the "best stories" from the Hacker News API

## How to run app?
You can use "dotnet run" command in order to run the app.
There is only one endpoint that you can access: _/api/News/BestStories/{storyAmount}_

## Potential improvements
As an improvement the "Retry Policy" could be added. <br> 
Also, the versioning could be implemented in a better way, rather than taking the version number from the config file.

We could separate controllers by their versions in the appropriate folder structure, like _"Versions/V1", "Versions/V2"_ etc. and every folder will have the same controller with different implementation (based on their version). <br>
Then we could use the version number in the endpoint route in order to access to the required version. (like _"/api/{version}/News/BestStories/{storyAmount}"_).

