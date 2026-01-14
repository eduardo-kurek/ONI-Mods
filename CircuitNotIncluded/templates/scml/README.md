To create a new circuit, follow these steps:

- Open the psd template (../CircuitTemplate.psd);
- Create the circuit with whatever size you want;
- Save this two images here: `panel_0.png` and `place_0.png`;
- Then you need to open the `logic_circuit_x.scml` file, change the title of the project to `logic_circuit_{width}x{height}`;
- Adjust the pivot for both created images to be in the center;
- Move all frames of the `off` and `place` animations to the correct position (aligned with x and in the middle of y);
- Save the scml;
- Execute `generate_kanim.bat` (must install `kanimal cli`);
- The files will be generated at `kanim` folder;
- Add a postfix of `_0` to the generated png file;
- Move the generated files to the appropriate folders in the mod structure;
- Register the circuit in code, following the existing patterns (`RegisterBuildings.cs`).