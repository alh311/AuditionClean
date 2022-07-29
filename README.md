# AuditionClean
An app to clean up unreferenced audio and peak files from the *_Recorded directory used by Adobe Audition.

## Usage Requirements
* Built for MacOS
	* The included binaries run on MacOS.  I'm assuming it will not run out-of-the box on Windows, so you may have to download the source code and re-publish it for use on Windows.
* Adobe Audition CS6
	* This app was created to clean up files from Audition CS6 sessions, but may work for other versions of Audition.

## Installation (Mac)
1. You have three options for installation:
	* Download the zip from the app folder.  This is the easiest.
	* Download the unzipped binary from the app folder.
	* Clone the repo and build the binary from source.
2. Ensure the binary has execution permissions.  I've found installing from zip retains the permissions while downloading the unzipped binary does not.  YMMV.  To grant execution permissions see the *Execution Permissions* section below.

## How to Use (Mac)
1. Move the app to the folder containing your session (.sesx) file.
2. Navigate to session folder in the Terminal app.  AuditionClean *__needs__* to be executed from the folder with the session file for it to work.
	* If you don't know how to do this: https://techwiser.com/how-to-navigate-to-a-folder-in-terminal-mac/
3. Run AuditionClean using the command: `./AuditionClean`
4. AuditionClean will delete any unreferenced audio (.wav, .mp3, etc.) and peak (.pkf) files and output the undeleted files as well as any errors.
	* Curious about peak (.pkf) files? Here ya go: https://fileinfo.com/extension/pkf

#### Note:
Don't want to move the AuditionClean to each session folder?  Me neither!  It's possible to register an alias so AuditionClean can be run from any folder on your Mac.  The problem is, I don't know which shell you're running, so you'll have to figure this out on your own.  Just know this is possible to set up and makes things a lot easier!

## Execution Permissions
In Terminal you can use the following command to grant execution privileges to each binary inside the app folder:
```
chmod +x <appName>
```
For example:
```
chmod +x CRSetup
```