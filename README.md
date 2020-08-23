# DeskOverlay
Display custom information from predefined text files as an overlay. 

Supports 2 modes of displaying info:
* bginfo style overlay : Display large data like a dyanamic bginfo applicaiton.
* taskbar info mode : Display a smaller data on the taskbar (on the left of systray)

One of the most common useful scenarios is, when connecting to multiple systems using RDP.
You can specify a system ID to be displayed on the taskbar to quickly identify the system while switching RDs windows.

## Settings

All the appication settings are located under %appdata%\SMFrameworks\DeskOverlay folder

* settings.json - contains application settings for DeskOverlay in json format.
* smbginfo.txt - content to be displayed in normal overlay mode (mode 0)
* smtaskbarinfo.txt - content to be displayed in taskbar mode (mode 1)

## How to use this applicaiton

* For simpler usage, you can create one of the text files specified in the above settings section and it will auto select the mode and apply the default settings.
* You can specify the content to be displayed, and it can be a calculated or automated output from one of your scripts or tools with a specific set of data like current system IP information etc.
* Taskbar info mode is designed for quick system identification purpose, so it is recommended to use it with a relevant label that should be displayed on taskbar.
* Advanced settings can be modified using settings.json file. Please refer to the sample settings.json file provided for more details on available settings and their valid values.


## Installing the applciation

* DeskOverlay is a standalone application that can be used completely in portable mode by default. You can install the app to any of the folders you prefer by simple copying of the binaries.
* Latest binaries can be compiled using Visual Studio after cloning this project.
* Alternatively; you may choose to use the pre-compiled binaries available in zip format under the releases folder at the root of the cloned directory.
* A set of sample settings are included under the samples folder at the root of the cloned directory.