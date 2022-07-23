# AuditionClean
An app to clean up unreferenced audio and peak files from the *_Recorded directory.


## Usage Requirements
* Built for MacOS
	* The included binary runs on MacOS.  I'm assuming it will not run out-of-the box on Windows, so you may have to download the source code and re-publish it for Windows use.
* Adobe Audition CS6
	* This app was created to clean up files from Audition CS6 sessions, but may work for other versions of Audition.

## How to Use (Mac)
1. Move the app to the folder containing your session (.sesx) file.
2. Navigate to session folder in the Terminal app.  The app *__needs__* to be executed from the folder with the session file for it to work.
	* If you don't know how to do this: https://techwiser.com/how-to-navigate-to-a-folder-in-terminal-mac/
3. Run the app using the command: `./AuditionClean`
4. The app will delete any unreferenced audio (.wav, .mp3, etc.) and peak (.pkf) files and output the undeleted files as well as any errors.
	* Curiouse about peak (.pkf)? Here ya go: https://fileinfo.com/extension/pkf
#### Note:
Don't want to move the app to each session folder?  Me neither!  It's possible to register an alias so the app can be run from any folder on your Mac.  The problem is, I don't know which shell you're running, so you'll have to figure this out on your own.  Just know this is possible to set up and makes things a lot easier!