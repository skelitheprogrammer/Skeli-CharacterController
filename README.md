# SkeliCharacterController
>Character controllers with multiple ways to control the player.
>
>This project uses [Zenject](https://github.com/modesttree/Zenject) to inject data without binding it via inspector or GetComponent<T>() (Optional, it's up to you how >you bind).
>
You can upload the entire project with all the controllers or choose what you want, among other resources.
  
## About
>This projects contains multiple ways to control the player with one main philosophy (At the moment only StateMachine + CharacterController).
>
I wanted to create a controller that would be modular and easily extensible. Read more in **Philosophy** section.

### Philosophy
  Main rule of this project is to separate data and logic (something like ECS but in OOP style) and use it in Controller.
  Each system class should process only the data that the controller will process using the logic underlying it.
  
  In this project each PlayerController controls how player will move, rotate, jump. You as a programmer should create systems that will be added to the PlayerController (if you want modify it).

## Future and todo's
  - Add Root motion with Character Controller option
  - Add Rigidbody (with or without State Machine) option
  - Add more modules that will add functionality to movement (including animations)
  - Add IK system
# Contacts
  Feel free to send feedback and ask questions by email: dosynkirill@gmail.com
