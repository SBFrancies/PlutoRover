# PlutoRover Architecture Discussion

## Components

### Backing DB

This could be used to store basic details about the rover(s) that would likely remain consistant such as their names, serial numbers, etc. A read only replica store could be created to avoid bottlenecking at the database layer.

### API

This is the component that has been started for this project. It could be hosted in a number of ways depending on the nature of the rovers. For example if the rovers have peaks and troughs of actvitiy - maybe they need to shut down to charge - they could be hosted on a serverless architecture like Azure Functions or AWS Lambdas.

### Front End UI

There could be multiple front end UI. Maybe one for simply viewing movements and one that allowed commands to be inputted. If web based it could be built in a response manner using a modern framework suhc as React or Angular.

### Service Bus and subscribers

The commands could be sent to a service bus which would allow outside subscribing processes to pick up on them. This might be neccessary for actions further down the line, for example a rover maintenace team.

### Event store

An event store could be used to keep a record of the commands, allowing them to be rolled back or replayed. This might be implemented in some kind of NoSql database such as Azure Table storage, CosmosDb or AWS Dynamo DB.
