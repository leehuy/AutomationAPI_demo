Feature: getBooks


Scenario: get book
	Given I get the request to server:/api/Books
	Then I get the returned message with code:
	| StatusCode | ReasonPhrase |
	| 200        | OK           |

	#And Respone message Book details with the same data:
	#| Id | Title               | Year | Price | Genre             | AuthorId |
	#| 1  | Pride and Prejudice | 1813 | 9.99  | Comedy of manners | 1        |
		#And Respone message Book details with the same data:
	#| Id | Title               | Year | Price | Genre             | AuthorId |
	#| 1  | Pride and Prejudice | 1813 | 9.99  | Comedy of manners | 1        |
		#And Respone message Book details with the same data:
	#| Id | Title               | Year | Price | Genre             | AuthorId |
	#| 1  | Pride and Prejudice | 1813 | 9.99  | Comedy of manners | 1        |

