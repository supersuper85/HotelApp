Feature: Customer

A short summary of the feature

@POST
Scenario: Add Customer
	Given The customer 
		| Age  | Name    | CNP            |
		| 22   | Georgeo | 1234234098125  |
	When I press create customer button
	Then Customer is created successfully

@EDIT
Scenario: Edit Customer
	Given The customer 
		| Name    | Age   | CNP            |
		| Georgeo | 22    | 9999999999989  |
	When I press edit customer button with the following details
		| Name    | Age   | CNP            |
		| Georgeo | 22    | 9999999999988  |
	Then Customer change it's fields with data from edit action

@DELETE
Scenario: Delete Customer
	Given The customer 
		| Name    | Age   | CNP            |
		| Georgeo | 22    | 1234234098112  |
	When I press delete customer button
	Then Customer is deleted successfully