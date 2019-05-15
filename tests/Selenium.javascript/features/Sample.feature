Feature: Sample
#
#Scenario: Search for accenture
#    When I navigate to site 
#	And I search for "Accenture"
#	Then I should see on title "Accenture"
#
#
#Scenario: Search for avanade
#    When I navigate to site 
#	And I search for "Avanade"
#	Then I should see on title "Avanade"
#
#
#	

Scenario: Show V1 products for regular user
    When I navigate to site 
	And I navigate to Oil products
	Then I should see "Filter Set V1" product


	
Scenario: Show V2 products for admin user
    When I navigate to site 
	And I login as a user "Administrator@test.com" with password "YouShouldChangeThisPassword1!"
	And I navigate to Oil products
	Then I should see "Filter Set V2" product