# Rubik

## Introduction
Rubik is an interactive project that simulates a Rubik's Cube, featuring textured smaller cubes created with Blender. Users can manipulate the cube using basic inputs to perform various movements. Movements are selected by pressing the corresponding key, based on the [move notation](https://en.wikipedia.org/wiki/Optimal_solutions_for_Rubik%27s_Cube#:~:text=The%20letters%20L%2C%20R%2C%20F,a%20prime%20symbol%20(%20%E2%80%B2%20)) (M, S, and E not implemented).

![alt text](https://github.com/CVanderbilt/Rubik/blob/master/other_resources/rubikVid.gif "SampleAnimation")

### Implemented Movements:
* **U**: Rotate the upper face 90 degrees clockwise.
* **D**: Rotate the bottom face 90 degrees clockwise.
* **L**: Rotate the left face 90 degrees clockwise.
* **R**: Rotate the right face 90 degrees clockwise.
* **F**: Rotate the front face 90 degrees clockwise.
* **B**: Rotate the back face 90 degrees clockwise.
* **X**: Rotate the entire cube along the X-axis.
* **Y**: Rotate the entire cube along the Y-axis.
* **Z**: Rotate the entire cube along the Z-axis.
* **Shift + Key**: Perform the specified movement in the opposite direction.

### Implementation Details
Each input triggers the rotation of a face. The rotation is simulated by rotating the center piece and setting it as the parent of the other squares involved in the movement. To avoid conflicts between movements, squares currently being moved cannot be affected by additional rotations until the current movement is complete.

For tracking squares in specific faces, trigger zones are set up around each center face. When a square exits a trigger, it is removed from the list of squares associated with that face. Conversely, when a square enters a trigger, it is added to the corresponding face's list.

#### Fixing Rotations
Each rotation is a 90-degree movement. The implementation involves a script called "Rotations," which maintains a variable set to 90 degrees when a face rotation is triggered. On each update, the face rotates slightly at a given speed, subtracting the rotated degrees from the initial 90 degrees. A similar logic applies to rotating the entire cube, with the distinction that face rotations are independent of cube rotations, allowing simultaneous movements.
