# BikeRental

## Instructions 



## Requirements 
The requirements for the test project are: 
Write an application to manage bike rentals:

* The application must be React-based. **-- only backend**
* Include at least 2 user roles: Manager and User
* Users must be able to create an account and log in.
* Each bike will have the following information in the profile: Model, Color, Location, Rating, and a checkbox indicating if the bike is available for rental or not.
 
###Managers can:

* Create, Read, Edit, and Delete Bikes.
* Create, Read, Edit, and Delete Users and Managers.
* See all the users who reserved a bike, and the period of time they did it.
* See all the bikes reserved by a user and the period of time they did it.

###Users can:
* See a list of all available bikes for some specific dates.
* Filter by model, color, location, or rate averages.
* Reserve a bike for a specific period of time.
* Rate the bikes with a score of 1 to 5.
* Cancel a reservation.

## Login Details
* **Manager :**
  * Username: devadmin 
  * Password:  admin123
* **User :**
  * Username: user1
  * Password:  user123
---------------------------  
  * Username: user2
  * Password:  user123
---------------------------  
  * Username: user3
  * Password:  user123

## API Endpoints
  * **Login: /api/Login**
  * **SignUp: /api/SignUp**

### Manager 
* Bikes Management:
   * Get List of all Bikes to show in grid. - GET - **/api/Bike**
   * Get Bike by ID to show in edit page. - GET - **/api/Bike/GetById**
   * Add Bike. - POST - **/api/Bike**
   * Update Bike. - PUT - **/api/Bike**
   * Delete Bike (soft delete). - DELETE - **/api/Bike**
   
* Users Management  (Users and Managers):
   * Get List of all user to show in grid. - GET - **/api/User**
   * Get user by ID to show in edit page. - GET - **/api/User/GetById**
   * Add user. - POST - **/api/User**
   * Update user. - PUT - **/api/User**
   * Delete user (soft delete). - DELETE - **/api/User**

* Reservation Report By Users:
   * Get all the users who reserved a bike, and the period of time they did it. - GET - **/api/User/UserReport** 
   * 
* Reservation Report By Bikes:
   * Get all the bikes reserved by a user and the period of time they did it. - GET - **/api/Bike/BikeReport** 



