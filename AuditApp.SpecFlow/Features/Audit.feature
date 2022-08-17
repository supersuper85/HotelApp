Feature: Audit

A short summary of the feature

@POST
Scenario: Add Audit
	Given The audit
		| EntityId  | EntityName    | ActionType  | TimeStamp                 | OldValues     | NewValues              | 
		| 1         | Georgeo       | INSERT      | 2022-08-11T07:47:11.000   |               | "{"id":1, "value":1}"  |
	When I press create audit button
	Then Audit is created successfully