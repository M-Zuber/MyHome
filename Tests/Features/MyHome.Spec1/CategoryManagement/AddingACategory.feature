Feature: AddingACategory
	In order to categorize the transactions
	I need to have categories with unique names

Scenario Outline: Adding a category
	Given The category type is '<categoryType>'
	And I have entered '<categoryName>' as the name
	And there is no other category with that name
	When I press add
	Then the category should be added to the list

	Examples:
	| testName           | categoryName | categoryType  |
	| addExpenseCategory | food         | expense       |
	| addIncomeCategory  | salary       | income        |
	| addPaymentMethod   | check        | paymentmethod |

Scenario Outline: Adding a category - with a duplicate name
	Given The category type is '<categoryType>'
	And I have entered '<categoryName>' as the name
	And there is another category with the same name
	When I press add
	Then the handler returns an error indicator

	Examples:
	| testName                     | categoryName | categoryType  |
	| duplicateNameExpenseCategory | food         | expense       |
	| duplicateNameIncomeCategory  | salary       | income        |
	| duplicateNamePaymentMethod   | check        | paymentmethod |

Scenario Outline: Adding a category - with a blank name
	Given The category type is '<categoryType>'
	And I have entered nothing for the name
	Then the handler returns an error indicator

	Examples:
	| testName                 | categoryName | categoryType  |
	| blankNameExpenseCategory |              | expense       |
	| blankNameIncomeCategory  |              | income        |
	| blankNamePaymentMethod   |              | paymentmethod |