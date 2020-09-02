# Grav
 
Demo: https://www.iamalexfish.com/games/grav/ 

## Setup:
1. Import the assets into your Assets folder.
1. Drag and drop a force field prefab in the scene.
2. Control the size of the force field by adjusting the scale of the Transform.
3. Control the strength of the force field by adjusting the "Strength" property.

## Creating a Custom Force Field:
1. Create a new 3D game object with a collider set to "Is Trigger".
2. Attach the "GravitationalField.cs" script.
3. Use the "Clear" material for mild transparency. 
4. Include a child game object at the center of the field and name it "planet".

## Notes:
- Force fields can only influence game objects containing a Rigidbody.
- The mesh renderer can be deactivated to make force fields invisible.
- Use the "Clear" material on the mesh renderer to give some transparency to your force fields.

## Contact
Support email: iamalexfish@gmail.com

## How You Can Support:
- Give this repo a star!
- Sponsor!
- Patreon: https://www.patreon.com/Hykudoru
