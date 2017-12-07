Feature: getBooks


Scenario: get book
	Given I get the request to server:/api/Books
	Then I get the returned message with code:
	| StatusCode | ReasonPhrase |
	| 200        | OK           |
	Then I get the returned message with code:
	| StatusCode | ReasonPhrase |
	| 200        | OK           |