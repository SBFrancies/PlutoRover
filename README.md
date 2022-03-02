# PlutoRover

## Description

A small API project for moving a rover around the surface of Pluto. Built using C# and ASP.NEt Core 3.1

## Requirements to run

1) .Net Core 3.1

## Projects

### PlutoRover.Api

This project contains the code for moving the rover.

### PlutoRover.UnitTests

This project contains the unit tests.

## Instructions

1) Start the project and navigate to the /swagger endpoint.

2) To "launch" a rover use the POST endpoint which does not take a GUI route parameter, the body of the method should be in the format below:

```
{
  "maxX": 100, // Defines the maximum extent of the map in the X-axis
  "maxY": 100, // Defines the maximum extent of the map in the Y-axis
  "startX": 0, // Defines the starting X coordinate of the rover
  "startY": 0, // Defines the starting Y coordinate of the rover
  "startDirection": "North", // Defines the starting direction of the rover, valid values are North, South, East or West
  "roverName": "Rover", // Defines the name of the rover (optional)
  "mapName": "Pluto" // Defines the name of the map (optional)
}
```

If succesful the response should have a 200 OK status code and a body like this:

```
{
  "id": "9887cc5e-ce03-42ad-b2c6-5a7f04630ef2",
  "name": "Rover",
  "x": 0,
  "y": 0,
  "direction": "North",
  "map": {
    "maxX": 100,
    "maxY": 100
  }
}
```

The id can then be used to issue commands to the rover using the other POST endpoint with the route parameter. The body of the endpoint should look like this:

```
{
  "commands": "FFFRFFFFLBBBB" // Commands for moving the rover, must be made up of the letters F, L, R, and B exclusively
}
```

The response will show the updated position of the rover:

```
{
  "id": "9887cc5e-ce03-42ad-b2c6-5a7f04630ef2",
  "name": "Rover",
  "x": 4,
  "y": 0,
  "direction": "North",
  "map": {
    "maxX": 100,
    "maxY": 100
  }
}
```

The GET endpoints can be used to find the details of one or all of the rovers that have been launched during a session.

## Notes

1) A rover cannot move outside the bounds of the grid it is assigned, if instructed to it will simply not move

## If I had more time / Future changes

1) Add StyleCop or some other static code analysis tool

2) Create a UI for inputing commands and monitoring rover movements

3) Persist the command list to allow commands to be replayed or rewound

4) Persist the rovers and maps so they are not just stored in memory and wiped when the application stops

5) Upgrade the projects to .Net 6
