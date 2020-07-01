# GameData-Scraper
Get data from the game and parse it for easier use in the app

This console app is used to read the json files that exist in the Scrap Mechanic game files and then work them into a format that can easily be used in the Assistant for No Man's Sky apps.

The current way I use the console app is:
- Generate 'intermediate files'
  - These files contain both the gameId as well as the appId. These files are used later to translate the game items 
- Generate app files
- Cut images from game files
- Copy the cut out images to the app `img` directory
- Generate app files (again)
  - This is done a second time so that the `icon` property is set correctly for each item
  
I explained this poorly, I hope that someone will ask me how this process works and re-write this 😅
