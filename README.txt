Ising Model Simulation -- 01.2025


What is in this repository? 

This repository contains two versions of a program that implements Ising's model for ferromagnetism. The first version of the program is purely textual. It is a C# console application that prints out the state of the particles making up a rectangular cuboidal lattice -- see "The Simulation" below. The second version uses the Unity engine to provide a real-time, graphical, interactive simulation. 
 
The overlap in code between the two versions is large, which is intended. The graphical, Unity version includes most of the .cs files of the textual version as scripts. It requires a couple specific additional classes whose code is written in new .cs files. An example is IsingModelGraphicalUnity. This additional class is solely dedicated to the graphical, Unity version. It inherits from its textual counterpart, IsingModel, and only contain as new attributes what is strictly needed to display things on the screen. The Unity project contains a single Scene with a Camera, a GameObject to which the simulation scripts are attached, as well as some additional GameObjects, scripts and other Unity-specific tools to provide a rudimentary UI. 

The strong separation between the code specific to the graphical, Unity version and the code dedicated to the core of the physical simulation should make it straightforward for the user fluent in another game engine or graphical library to turn the textual version into another graphical version. 


How to run the program? 

The textual version is a C# console application that can be built and run using a C# compiler. Pointers are used, which means one is forced to allow "unsafe code" in the compiler options. The class IsingModelSim contains a Main function in which a 100 x 100 x 1 simulation is readily initialized. 

A Linux build for the graphical version is readily available in the folder "Linux build". The Unity project at the source of this build is available in the folder Ising_model_graphical_Unity. At it is in the repository, it is fairly incomplete, for a full working Unity project is generally pretty large (several GB). A somewhat severe but widespread .gitignore is used to discard many files that are automatically created upon the generation of a new 3D project in Unity. If the project in the Ising_model_graphical_Unity folder is opened with Unity Editor 6, the needed redundant files should be created automatically, making it complete. 
The project can then be explored from the Editor, or built and run. It comes with a rudimentary UI, so that it is not necessary to interact with it in the Editor. See more below under "How to interact with the program?".


The simulation.

Ising's model simulates the evolution of a lattice of particles that can be in one of two states and interact locally. The prime application of this model concerns ferromagnetism. At each simulation step, the state of each particle is updated according to rules that involve the states of the neighboring particles. The rules involve the computation of the energy associated to a given particle. The latter energy also depends on the ambient magnetic field, and the overall temperature of the system. The temperature of the system is represented by a parameter called beta. More precisely, beta is proportional to the inverse of the temperature (sometimes called coldness). This is implemented in the IsingModel class. A rectangular cuboidal lattice of particles is encoded as a three-dimensional array of booleans in C#. The program treats the two-dimensional case if it is fed a lattice of size n x m x 1. 


How to interact with the program? 

The textual version has a Main function in the IsingModelSim class. It readily contains an initialized simulation for a 100x100x1 example. 

The graphical version comes with a simple UI that enables one to interact with the simulation in real time. The WASD keys control the position of the camera. Keeping the right mouse button pressed while moving the mouse around rotates the camera. Below the tag "Parameters" in the top right corners of the screen lie two sliders. They control the two parameters of the simulation: the coldness and the ambient magnetic field. 
Below the tag "Ferromagnetic Chunk" are yet another bunch of sliders. The sliders "Dimensions" control the dimensions of a new rectangular cuboidal ferromagnetic chunk to be simulated. Once dimensions are specified, pressing Enter will spawn the new lattice. 


Authorship. 

This program is a personal project, though it is inspired from the many implementations of the Ising model that can be found online. 

