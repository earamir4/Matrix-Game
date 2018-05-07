# Matrix Transformations
### Development Team
Kyle Chapman, Benjamin Jordan, Sean McGonegle, Erick Ramirez Cordero, Cameron Root
### Google Systems for Unity
Tyler Baron

## Section I - Game Overview
### 1.1 Summary
**Genre**
Educational

**Audience**
College students taking Linear Algebra

**Game Concept**
Matrix Transformations aims to assist college students learning Linear Algebra by giving them an interactable environment for them to better understand how matrices work within a 2D space.

**Feature Set**
12 levels that cover 4 sections of material covered in a Linear Algebra classroom
Google Sheets for Unity support, allowing for teachers to collect data from students using the software
Areas of customization for teachers, including level description and hint system

## Section II - Gameplay
### 2.1 Progression
This game contains 4 sections of material, each with 3 levels of example problems. Players can choose whichever level they wish to complete, and will progress onto the next level after the current problem is solved. 

### 2.2 Objectives
Each level presents the user with a grid containing a series of points. The player must use the templates provided to them to move these sets of points to a specific destination, which is shown in a faded red. 
They have as many tries as needed to succeed on the level, and may restart anytime they wish

## Section III - Mechanics 
### 3.1 Matrix Template System
The primary way we want the player to solve equations is by providing a lot of matrix templates that they can interact with. These 2x2 matrices restrict what values the player can interact with as a way of guiding the player towards learning the purpose of different aspects of a matrix.
For example, this matrix restricts all inputs but the top right area of the matrix, which handles the ability to shear a set of points. By initially restricting what the player can manipulate, they will be able to see what different points of a matrix does on the graph.
We have created some sets templates based off the material given to us, and the system is set up that more can be created easily enough. The professor has someone working for her that is familiar with Unity, so we discussed with her a way for him to create more if desired.

### 3.2 Math View
One thing the professor was worried about was that users would have little to know understanding of how the templates and matrices actually affect the points on the graph. By simplifying things to work within the template system, we had taken a bit of the math out.
To compensate, we designed a menu on the side that pops up after a player submits, that shows what is going on in more detail. The player can type in a point in the “Choose Point” area below, and the above view will show how the matrices in the work area affect it. 
We originally wanted this to have much more functionality, such as the ability to click directly on a point on the graph, and some color coating of the matrices to indicate what set of numbers is what. However, we had to scrap a lot for time sake.
### 3.3 Rendering System
A shader is used to create a graph background at runtime, using a crisscrossing lines texture developed for the game. This shader is applied to a 2D plane which faces a camera separate from the main one. The output of that camera is applied to a render texture, and that render texture is displayed in the user interface as a simple image.
Simple 3D spheres are used to render the level’s points before, during, and after transformation. They are placed in front of the 2D plane to line up with the coordinate system displayed by that plane’s cartesian graph graphic. The spheres are moved around to show the transformation applied by the input matrices to the coordinates that the spheres represent. The camera zooms in and out to show the extents of the rendered points, and the zoom can also be controlled by the player using the scroll wheel. Where these initial points are placed is set for each level to create the different puzzles.
When the player is inputting their own testing point as shown above, a new differently colored point appears in the matrix render area which is also affected by transformations of the input matrices in the same way.

### 3.4 Google Systems for Unity (GSFU) System
The Google Sheets for Unity system is a way to collect data from students who use the software. Students login on the main menu by inputting four different pieces of information. These are their name, the webapp URL, the google sheet ID, and the password. The code used for the webapp is one that was given to us. The way to implement it is documented, but it is rather simple. It boils down to importing it into the google scripts of the google account that contains the websheet, and then publishing it. The data points that the student needs are found and supplied by the teacher.

The game side GSFU is different. This contains three mandatory codes that aren’t changed and many methods to use. These methods have to be implemented with other methods made by the user. The methods range from creating a player to updating a single value in a single row. The data needs to be parsed after being imported from the google sheet when using any method. All of this is in a single code as a lot of the data importing/exporting can be run through a single parsing code to pull out data needed.

For this game, we used it to track player progress. When the student logs in from the main menu, it will either create a row for him, or see that there is already a row for his name and store the name for later use. When the student finishes a question correctly, the GSFU will send the time it took to complete the question to their row.
