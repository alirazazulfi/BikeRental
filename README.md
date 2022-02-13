# BikeRental

## Instructions 
1. Open the Project in Visual Studio.
2. Run the Database Migration, placed in **BikeRental.Repository** project.
3. After database migrations you can set **BikeRental** as **StartUp Project** and run the project  
4. This will execute the Web view of the **Swagger OpenAPI** 
   * https://localhost:7261/swagger/index.html
5. First Login using Manager or User by the given credentials below.
6. Use the **Bearer Token** generated after the login to get the access of the other api enpoints. 
7. Click on the **Authorize Button** on the top of the page and enter the **Bearer Token**.
8. API's are restricted according to their access level. eg. Manager cannot access the API's which are only for the Users. and vice versa.  


## Task Requirements 
The requirements for the test project are: 
Write an application to manage bike rentals:

* The application must be React-based. **-- only backend**
* Include at least 2 user roles: Manager and User
* Users must be able to create an account and log in.
* Each bike will have the following information in the profile: Model, Color, Location, Rating, and a checkbox indicating if the bike is available for rental or not.
 
**Managers can:**
* Create, Read, Edit, and Delete Bikes.
* Create, Read, Edit, and Delete Users and Managers.
* See all the users who reserved a bike, and the period of time they did it.
* See all the bikes reserved by a user and the period of time they did it.

**Users can:**
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
  * Username: user1 | user2 | user3
  * Password:  user123

## API Endpoints
  * **Login: /api/Login**
  * **SignUp: /api/SignUp**

**Manager**
* **Bikes Management:**
   * Get List of all Bikes to show in grid. - GET - **/api/Bike**
   * Get Bike by ID to show in edit page. - GET - **/api/Bike/GetById**
   * Add Bike. - POST - **/api/Bike**
   * Update Bike. - PUT - **/api/Bike**
   * Delete Bike (soft delete). - DELETE - **/api/Bike**
   
* **Users Management  (Users and Managers):**
   * Get List of all user to show in grid. - GET - **/api/User**
   * Get user by ID to show in edit page. - GET - **/api/User/GetById**
   * Add user. - POST - **/api/User**
   * Update user. - PUT - **/api/User**
   * Delete user (soft delete). - DELETE - **/api/User**

* **Reservation Report**
  * By Users:
     * Get all the users who reserved a bike, and the period of time they did it. - GET - **/api/Reservation/UserReport** 
  * By Bikes:
     * Get all the bikes reserved by a user and the period of time they did it. - GET - **/api/Reservation/BikeReport** 

**User** 
* **Reservation:**
   * Reserve a bike for a specific period of time. - POST - **/api/Reservation/ReserveBike**
   * Cancel a reservation. - POST - **/api/Reservation/Cancel**
   * Get List of Bike Reservation for logged in user. - GET - **/api/Reservation/GetForUser** 
* **Bike:**
   * See a list of all available bikes for some specific dates.. - POST - **/api/Bikes/AvailableBikes**
   * Filter by model, color, location, or rate averages. - POST - **/api/Bike/Search**
   * Rate the bikes with a score of 1 to 5. - POST - **/api/Bike/Rating**
