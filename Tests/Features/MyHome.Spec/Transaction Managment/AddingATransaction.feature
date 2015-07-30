Feature: AddingATransaction
	In order to keep track of my income and expenses
	I need to be able to add a transaction

Scenario Outline: Adding a Transaction
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name     | Value      |
	| Date     | 2015-06-06 |
	| Amount   | 20.5       |
	| Comments |            |
	When I press add
	Then the transaction should be added to the list

	Examples: 
	| testName   | transactionType |
	| addExpense | expense         |
	| addIncome  | income          |

Scenario Outline: Adding a Transaction with 0 as the amount
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name     | Value      |
	| Date     | 2015-06-06 |
	| Amount   | 0          |
	| Comments |            |
	When I press add
	Then the transaction should be added to the list

	Examples: 
	| testName   | transactionType |
	| addExpense | expense         |
	| addIncome  | income          |