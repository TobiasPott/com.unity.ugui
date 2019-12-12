# Multiple Concurrent Unity UI (MCU)
The Unity UI package allows you to create in-game user interfaces fast and intuitively.
This is an modified versions which allows you to setup multiple UIs controlled by separate users/inputs.

## Prerequisites
### Unity 2019.2
This package is in development, and requires Unity 2019.2.

## Getting Started
The Unity UI user manual can be found [here](https://docs.unity3d.com/Manual/UISystem.html).

## Known Issues
Several components found in the TextMeshPro package are using the EventSystem.current static property and would require modifications themselves to work with the MCU.
This is scheduled for a later point and might be provided as per-component replacements or derived types to address the problem.