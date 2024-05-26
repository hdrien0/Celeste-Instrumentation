# Celeste Instrumentation

This project aims to provide an instrumentation interface for the game Celeste.  
It was originally created to train AI agents (see [Celeste-NEAT](https://github.com/hdrien0/Celeste-NEAT)).

## Description

### Project structure

The project is comprised of two main parts :
* C# code consisting of new classes and [Harmony](https://github.com/pardeike/Harmony) patches of some game classes (`CelesteInstrumentation` folder)
* A python module to communicate with the instrumented game (`CelestePythonInterface` folder)

### How it works

The instrumented game will load, and wait for instructions via socket communication. With the Python interface you'll be able to instruct it which level to load, along with other parameters.  
Every frame, the game will send to the Python interface data about Madeline's state. You can then process it (for example with a neural network) and send back to the game the inputs to perform. This cycle will continue until a specified condition is met (death, transition or timeout)  

See :
* The `examples` folder to see how the Python interface can be used
* The Celeste-NEAT repository for a more complex use case
* The files `SessionParameters.py` and `SessionData.py` in the folder `CelestePythonInterface/CelestePythonInterface` for more details about the data exchanged.

## Installation

There is no installer, you'll need to patch the game yourself. Just follow the following instructions on a Windows computer with a XNA version of Celeste.

### Necessary files and dnSpy

First, grab the latest release in the `Release` tab of this repository.  
In addition to the source code, there should be three files :
* `0Harmony.dll`
* `CelesteInstrumentation.dll`
* `InstrumentationParameters.xml`

Download them, and then get the latest version of [dnSpy](https://github.com/dnSpyEx/dnSpy/releases) (you'll need that to patch the game binary).

### Patching the game

* Go to the location of the file `Celeste.exe` (if you have the Steam version it will be something like `C:\Program Files (x86)\Steam\steamapps\common\Celeste\Celeste.exe`).
* Make a backup copy of `Celeste.exe`, for example `Celeste.exe.old`.
* Move the three files you donwloaded earlier here.
* Open `Celeste.exe` in dnSpy.
* Using the Assembly Explorer of dnSpy (left menu), navigate to `Celeste > Celeste.exe > Celeste > Celeste`. You should get something like the following image :

![Assembly Explorer](https://i.imgur.com/MFFFMRp.png)

* Right click on the code and click `Edit Class`. Press the button with the folder icon (see the image below) and add `0Harmony.dll` and `CelesteInstrumentation.dll`. Then add the following line at the same position as the image below :  
`Instrumentation.Entry.Execute();`

![Assembly Explorer](https://i.imgur.com/DF5OFLh.png)

* Press the `Compile` button, and then press `Ctrl + Shift + S` and `OK` to save the file. You can then close dnSpy. 

## Usage

You can then run `Celeste.exe`. The game will load normally and then wait for instructions.

The global configuration of the instrumented game is done with `InstrumentationParameters.xml`. There you can :
* Change the speed at which the game executes
* Disable the game graphics
* Enable the debug rendering mode
* Disable instrumentation altogether

To use the Python interface :

### Windows

```bash
python -m venv venv
venv/Scripts/activate.bat
pip install CelestePythonInterface
```

### Linux

```bash
python -m venv venv
source venv/bin/activate
pip install CelestePythonInterface
```

## Dev environment setup

If you want to edit and build `CelesteInstrumentation.dll` yourself, you'll need to open the `CelesteInstrumentation` solution folder in **Visual Studio 2019**. You'll then need to add the following assembly references to your project :
* `Celeste.exe`
* `Microsoft.Xna.Framework`
* `Microsoft.Xna.Framework.Game`
* `Microsoft.Xna.Framework.Graphics`

## Contributing

There is a lot of room for improvement, don't hesite to contriibute to this project.