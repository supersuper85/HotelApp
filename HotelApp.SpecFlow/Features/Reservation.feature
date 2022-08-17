Feature: Reservation

A short summary of the feature

@POST
Scenario: Add Reservation
	Given The reservation 
		| NumberOfDays | ApartmentId | HotelId |
		| 2            | 3           | 1       |
	Given The customer Id of reservation
		| Id |
		| 2  |
	When I press create reservation button
	Then Reservation is created successfully

@EDIT
Scenario: Edit Reservation
	Given The reservation 
		| NumberOfDays | ApartmentId | HotelId |
		| 2            | 8           | 1       |
	Given The customer Id of reservation
		| Id |
		| 2  |
	When I press edit reservation button with the following details
		| ReleaseDate             | ApartmentId |
		| 2022-08-22T12:00:00.000 | 8           |
	Then Reservation change it's fields with data from edit action

@DELETE
Scenario: Delete Reservation
	Given The reservation 
		| NumberOfDays | ApartmentId | HotelId |
		| 2            | 4           | 1       |
	Given The customer Id of reservation
		| Id |
		| 2  |
	When I press delete reservation button
	Then Reservation is deleted successfully
