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


