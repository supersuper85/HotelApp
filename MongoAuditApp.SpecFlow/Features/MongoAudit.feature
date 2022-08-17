Feature: MongoAudit

A short summary of the feature

@POST
Scenario: Add Mongo Audit
	Given The mongo audit
		| EntityId  | EntityName    | ActionType  | TimeStamp                 | OldValues     | NewValues              | 
		| 1         | Georgeo       | INSERT      | 2022-08-11T07:47:11.000   | null          | "{"id":1, "value":1}"  |
	When I press create mongo audit button
	Then Mongo audit is created successfully
