Feature: AddingATransaction
	In order to keep track of my income and expenses
	I need to be able to add a transaction

Scenario Outline: Adding a Transaction with a date in the past
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 20.5       |
	| Comment | Stuff      |
	When I press add
	Then the transaction should be added to the list

	Examples: 
	| testName   | transactionType |
	| addExpense | expense         |
	| addIncome  | income          |

Scenario Outline: Adding a Transaction with a date in the future
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 3000-06-06 |
	| Amount  | 20.5       |
	| Comment | Stuff      |  
	When I press add
	Then the transaction should be added to the list

	Examples: 
	| testName   | transactionType |
	| addExpense | expense         |
	| addIncome  | income          |

Scenario Outline: Adding a Transaction with no date
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value |
	| Date    |       |
	| Amount  | 20.5  |
	| Comment | Stuff |
	When I press add
	Then the transaction should be added to the list
	And  the date is the current date

	Examples: 
	| testName   | transactionType |
	| addExpense | expense         |
	| addIncome  | income          |

Scenario Outline: Adding a Transaction with 0 as the amount
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 0          |
	| Comment | Stuff      |
	When I press add
	Then the transaction should be added to the list

	Examples: 
	| testName   | transactionType |
	| addExpense | expense         |
	| addIncome  | income          |

Scenario Outline: Adding a Transaction with no comment
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 0          |
	| Comment |            |
	When I press add
	Then the transaction should be added to the list

	Examples: 
	| testName   | transactionType |
	| addExpense | expense         |
	| addIncome  | income          |

Scenario Outline: Adding a Transaction with no Category
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category '' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 12         |
	| Comment | Stuff      |
	When I press add
	Then the handler returns an error indicator - 'There must be a category selected'

	Examples: 
	| testName   | transactionType |
	| addExpense | expense         |
	| addIncome  | income          |

Scenario Outline: Adding a Transaction with no method
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method ''
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 12         |
	| Comment | Stuff      |
	When I press add
	Then the handler returns an error indicator - 'There must be a payment method selected'

	Examples: 
	| testName   | transactionType |
	| addExpense | expense         |
	| addIncome  | income          |