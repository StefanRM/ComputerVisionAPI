  ______   ______   .___  ___. .______                   
 /      | /  __  \  |   \/   | |   _  \                  
|  ,----'|  |  |  | |  \  /  | |  |_)  |                 
|  |     |  |  |  | |  |\/|  | |   ___/                  
|  `----.|  `--'  | |  |  |  | |  |                      
 \______| \______/  |__|  |__| | _|                      
                                                         
____    ____  __       _______. __    ______   .__   __. 
\   \  /   / |  |     /       ||  |  /  __  \  |  \ |  | 
 \   \/   /  |  |    |   (----`|  | |  |  |  | |   \|  | 
  \      /   |  |     \   \    |  | |  |  |  | |  . `  | 
   \    /    |  | .----)   |   |  | |  `--'  | |  |\   | 
    \__/     |__| |_______/    |__|  \______/  |__| \__| 
                                                         
     ___      .______    __                              
    /   \     |   _  \  |  |                             
   /  ^  \    |  |_)  | |  |                             
  /  /_\  \   |   ___/  |  |                             
 /  _____  \  |  |      |  |                             
/__/     \__\ | _|      |__|                             


	This project uses Microsoft.ProjectOxford.Vision. It's documentation:
https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/tutorials/csharptutorial

	The program is written in C# and it sorts a bunch of images (.jpg, .jpeg, .png)
according to its confidence factor. After sorting the name tag that has the
highest confidence factor is choosen as folder name and the image is moved
there.
	When the program finishes, all images are sorted according to their tags,
and those that do not have a tag can be found in the "Tagless" folder.

	Images Location: Computer Vision\bin\Debug
					 -> There are 2 folders with images;
					 -> copy the content of the images to the Debug directory;
					 -> run the App;
					 -> see its result.