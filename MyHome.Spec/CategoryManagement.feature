Feature: CategoryManagement
	As a user I want to be able to manage the 
	categories for the flow items

Scenario: Adding a category with an unique name
	Given I have entered "food" as the name of the category
	And there is no other category with that name
	When I press add
	Then the category should be added to the list

Scenario: Adding a category with a duplicate name
	Given I have entered "food" as the name of the category
	And there is another category with the same name
	When I press add
	Then the category is not added to the list