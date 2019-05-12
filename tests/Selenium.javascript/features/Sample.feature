Feature: Sample

Scenario: Search for accenture
    When I navigate to site 
	And I search for "Accenture"
	Then I should see on title "Accenture"


Scenario: Search for avanade
    When I navigate to site 
	And I search for "Avanade"
	Then I should see on title "Avanade"
