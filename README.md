# ASTEROIDS
# SETUP
Unity version 2022.3.16
- All files are placed in folder Assets/Game
- The main and only scene is Game/Scenes/Main
- No any 3D party assets needed for running the project
# ARCHITECTURE
- The main pattern for achitecture is Zenject-like DI container which based on Service Locator principle (instead of real injecting)
- For controlling game objects there is a System-Entity based approach. There are some systems such as Movement, Colliding, Life Control etc that 
update enteties. All enteties are NOT Mono Behaviors. The only thing that connects entety controllers are Component that apply data from entity controller
to a Game Object. 
- Systems ARE MonoBehaviors but only in purpose of easeing development. In future all Systems can be re-designed as NON-MonoBehaviors.
- For improving memory usage there is Pool of Game Objects. So no game objects are being disposed. All of them are supposed to be re-used.
- UI Screens are implemented according MVC pattern.
- There are also some patterns inprojects such as Factory and others.
