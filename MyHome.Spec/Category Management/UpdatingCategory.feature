Feature: UpdatingCategory
	In order to ensure user comfort
	I need to allow for updating categories

Scenario: Chaning the name of a category to a new unique name
	Given A category with the name "medical"
	When I press change name
	And provide "medicine" as the new name
	Then the name is changed

Scenario: Chaning the name of a category to a new non-unique name
	Given A category with the name "books"
	And a category with the same name
	When I press change name
	And provide "school" as the new name
	Then the name stays the same
