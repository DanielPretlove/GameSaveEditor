# XCDESave
A library to handle parsing and editing of Xenoblade Chronicles: Definitive Edition save files.

## WARNING: THIS IS A WORK-IN-PROGRESS!
This library is still in development, and so far only a few parts of the files have been figured out. Much more reverse engineering of the file format is still required.

NOTE: Name resolution of various IDs to names (Example: displaying Item IDs: 2-0004 -> "Monado", 4-0642 -> "Colony Cap") is not yet implemented.

## List of supported files/sections:
bfsgame0xx.sav: (Single Save File)

* Money
* Noponstones
* Inventory structure
* Items (Almost 100%, a few sections of item data are still unidentified)
* Party: List of characters in Party + total count
* Party Member: Party Member/Player Character data (Almost 100%, a few sections still unidentified)
* Arts Levels + Arts Max Unlock Levels

bfsgame0xx.tmb: (Save File Thumbnail Image)

* Thumbnail image

bfssystem.sav: (Game Settings/Event Theater Save File)

* Not yet supported