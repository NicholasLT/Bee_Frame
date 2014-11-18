Bee_Frame (C#)
=========

Bee Killing Application

Simply run the source code to run the application:

[Generate] will populate the grid with 'Bees'

[Attack] will randomly choose a target and a random value between 0-80 to inflict on the Bee - Each Bee has a different 'Death Percentage' based on its type.

This application was made in accordance with the provided specification;


Object Orientated Programming 
The purpose of the test is to assess basic knowledge and understanding of object-oriented 
programming and design. 
The Test
Write classes to represent 3 different types of Bee - Worker, Queen and Drone. 
Each Bee has a floating-point health property, which is not writable externally and upon creation is 
set to a value of 100 (percent). 
Each Bee has a Damage() method that takes a single integer parameter that should be a value 
between 0 and 100. When this method is called, the health of the bee is to be reduced by that 
percentage of their current health. 
When a Worker has a health below 70% it cannot fly and therefore is pronounced Dead. When a 
Queen has a health below 20%, or a Drone below 50%, it is pronounced dead. This 'Dead' property 
should be readable from each Bee. When a Bee is dead, no further health deductions should be 
recorded by the bee, although the Damage() method should still be invoke-able without error.
Your application should create a single list containing 10 instances of each type of Bee. It must 
support methods to allow Damage() to be called for each Bee, and to return the health status of 
each bee, including whether it is alive or not.
The user interface should show the current status of each bee and contain a button. When clicked, a 
different random value between 0 and 80 should be selected for each bee and applied with a call to 
Damage(). After each click the page should refresh to show the health status of the bees.
