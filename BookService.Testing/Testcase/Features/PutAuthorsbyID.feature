Feature: PutAuthorsbyID

Scenario: Put Author by ID
	Given send a request to server: /api/Authors/
	And And I execute request has following info
	| Name |
	| huy  |
	Then I get the returned message with code:
	| StatusCode | ReasonPhrase |
	| 201        | Created      |
	And Respone message create Auther details with the same data:
	| Name |
	| huy  |
