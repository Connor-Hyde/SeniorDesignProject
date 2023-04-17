# Terra Table
# Table of Contents

1. [Project Description](#project-description)
2. [User Interface Specification](#user-interface-specification)
3. [Test Plan and Results](#test-plan-and-results)
4. [User Manual](#user-manual)
5. [Spring Final PPT Presentation](#spring-final-ppt-presentation)
6. [Final Expo Poster](#final-expo-poster)
7. [Assessments](#assessments)
    1. [Initial Self-Assessments](#initial-self-assessments)
    2. [Final Self-Assessments](#final-self-assessments)
8. [Summary of Hours and Justification](#summary-of-hours-and-justification)
9. [Summary of Expenses](#summary-of-expenses)
10. [Appendix](#appendix)

## Project Description
<a name="project-description"></a>

### Background
The purpose of this project is to elevate the gameplay experience of table-top role-playing games such as “Dungeons and Dragons”. These games are often played in the ‘theater of the mind’ using minimal props for visualizing the world in which the players are role-playing. However, many players do like to use a ‘battle mat’ to help visualize the specific locations and layouts of a certain area in the game.

![photoHere](Images/2DPlaymat.jpg)

These mats consist of a large grid of squares. Each square represents a scaled down distance in game; players can use miniature figures of their characters to represent where they are in the world. The downside to these mats is that they are low tech and only two dimensional. Some players turn to using small blocks or walls to create a more 3D feeling to their environments, while others invest time and money into creating elaborate dioramas for their games.

![photoHere](Images/3DModel.jpg)

This project aims to bridge the gap between low tech battle mats and high detail dioramas with programable, 3D topography, built into the game table.

### Objectives
This project aims to create a device that can be inserted into any table and convert it into a 3D topographical map that is fully programable to create scenes and landscapes for their gameplay. The final product will consist of a grid of cubes, each capable of being raised and lowered to create a 3D landscape. The shape of the scene will be determined by a graphical user interface where the players can create their map in 2D in the program and then watch as their creation comes to life in the table.

## User Interface Specification
<a name="user-interface-specification"></a>

The user interface was designed using Windows Presentation Foundation (WPF), and consists of 3 key features:

1. A slider for specifying height
2. 3 buttons, for executing actions
3. A 10x10 grid of squares, visually representing what the physical table will look like


![photoHere](Images/UIDesign.png)

### Slider
The slider uses a gradient of colors from green to yellow to red, with a text box displaying the percentage of max height, and the corresponding height in inches. Whatever value is chosen on the slider will be applied to a grid square on click.

### Buttons
There are presently 3 buttons that allow the user to execute their respective actions:

1. Import: A text file can be imported, which will instantly apply a pre-recorded configuration to the grid.
2. Export: This exports the current configuration, so that at a later time it can be re-applied if desired.
3. Run: This executes the main functions of the table, sending the current configuration to the Arduino sketch. This is done by serialized communication with the board, and upon selecting this command the user will begin to see movements in the table until all cubes have been raised to desired height.

### Grid
The grid uses a gradient color scale to help the user visualize what they are creating on the table in 3 dimensions. From the image above, this would create something similar to a staircase, with a platform at the top. The idea was derived from topographical terrain maps that use gradients to visualize elevation on a 2 dimensional plane.

## Test Plan and Results
<a name="test-plan-and-results"></a>

> Describe execution and results of all tests

## User Manual
<a name="user-manual"></a>

> Includes links and screenshots of online user manual

## Spring Final PPT Presentation
<a name="spring-final-ppt-presentation"></a>

> Add link to presentation

## Final Expo Poster
<a name="final-expo-poster"></a>

> Add link to poster

## Assessments
<a name="assessments"></a>

### Initial Self-Assessments
<a name="initial-self-assessments"></a>

> Fall semester self-assessments

### Final Self-Assessments
<a name="final-self-assessments"></a>

> Spring semester self-assessments (do not include confidential Team-Assessments)

## Summary of Hours and Justification
<a name="summary-of-hours-and-justification"></a>

> One per individual team member

## Summary of Expenses
<a name="summary-of-expenses"></a>

> Provide an overview of project expenses

## Appendix
<a name="appendix"></a>

> Include any additional information, resources, or references