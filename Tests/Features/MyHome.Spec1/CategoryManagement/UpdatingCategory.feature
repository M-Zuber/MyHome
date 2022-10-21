Feature: UpdatingCategory 
	In order to ensure user comfort
	I need to allow for the updating of categories

Scenario Outline: Changing the name of a category to a new unique name
	Given The category type is '<categoryType>'
	And a category with the name '<categoryName>'
	When I change the name to '<newName>'
	Then the category is updated

	Examples: 
	| testName              | categoryName | categoryType  | newName     |
	| updateExpenseCategory | food         | expense       | medicine    |
	| updateIncomeCategory  | salary       | income        | gift        |
	| updatePaymentMethod   | check        | paymentmethod | credit card |

Scenario Outline: Changing the name of a category to a non-unique name
	Given The category type is '<categoryType>'
	And a category with the name '<categoryName>'
	And the '<newName>' already exists
	When I change the name to '<newName>'
	Then the handler returns an error indicator
	And the category name remains '<categoryName>'

	Examples: 
	| testName                       | categoryName | categoryType  | newName     |
	| updateNonUniqueExpenseCategory | food         | expense       | medicine    |
	| updateNonUniqueIncomeCategory  | salary       | income        | gift        |
	| updateNonUniquePaymentMethod   | check        | paymentmethod | credit card |

Scenario Outline: Updating a category - with a blank name
	Given The category type is '<categoryType>'
	And a category with the name '<categoryName>'
	When I have entered nothing for the name
	Then the handler returns an error indicator
	And the category name remains '<categoryName>'

	Examples: 
	| testName                 | categoryName | categoryType  |
	| blankNameExpenseCategory | food         | expense       |
	| blankNameIncomeCategory  | salary       | income        |
	| blankNamePaymentMethod   | check        | paymentmethod |
