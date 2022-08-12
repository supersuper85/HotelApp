Feature: Apartment

A short summary of the feature

@POST
Scenario: Add Apartment
	Given The apartment 
		| DailyRentInEuro  | NumberOfRooms    | ApartmentNumber | HotelId |
		| 25               | 2                | 19               | 1       | 
	When I press create apartment button
	Then Apartment is created successfully

@EDIT
Scenario: Edit Apartment
	Given The apartment 
		| DailyRentInEuro  | NumberOfRooms    | ApartmentNumber | HotelId |
		| 25               | 6                | 54              | 1       | 
	When I press edit apartment button with the following details
		| DailyRentInEuro  | NumberOfRooms    | ApartmentNumber |
		| 25               | 6                | 55              |
	Then Apartment change it's fields with data from edit action

@DELETE
Scenario: Delete Apartment
	Given The apartment 
		| DailyRentInEuro  | NumberOfRooms    | ApartmentNumber | HotelId |
		| 25               | 2                | 19               | 1       | 
	When I press delete apartment button
	Then Apartment is deleted successfully

