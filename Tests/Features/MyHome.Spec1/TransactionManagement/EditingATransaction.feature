Feature: EditingATransaction
	In order to keep track of my income and expenses
	Given that I am human
	I need to be able to edit transactions already in the program

Scenario Outline: Editing the date to a date in the past
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 20.5       |
	| Comment | Stuff      |
	When I change the 'Date' to '2000-06-06'
	Then the new 'Date' equals '2000-06-06'

	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |

Scenario Outline: Editing the date to a date in the future
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 3000-06-06 |
	| Amount  | 20.5       |
	| Comment | Stuff      |
	When I change the 'Date' to '4000-06-06'
	Then the new 'Date' equals '4000-06-06'

	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |

Scenario Outline: Editing the date to none
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value |
	| Date    |       |
	| Amount  | 20.5  |
	| Comment | Stuff |
	When I change the 'Date' to ''
	Then the new 'Date' equals ''

	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |

Scenario Outline: Editing the amount
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 0          |
	| Comment | Stuff      |
	When I change the 'Amount' to '20'
	Then the new 'Amount' equals '20'

	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |

Scenario Outline: Editing the comment to nothing
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 0          |
	| Comment | Stuff      |
	When I change the 'Comment' to ''
	Then the new 'Comment' equals ''

	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |

Scenario Outline: Editing the comment
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 0          |
	| Comment | Stuff      |
	When I change the 'Comment' to 'something else'
	Then the new 'Comment' equals 'something else'

	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |

Scenario Outline: Editing the category
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 12         |
	| Comment | Stuff      |
	When I change the 'Category' to 'clothes'
	Then the new 'Category' equals 'clothes'


	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |

Scenario Outline: Editing the category to nothing
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 12         |
	| Comment | Stuff      |
	When I change the 'Category' to ''
	Then the handler returns an error indicator - 'There must be a category selected'

	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |

Scenario Outline: Editing the payment method
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 12         |
	| Comment | Stuff      |
	When I change the 'Method' to 'check'
	Then the new 'Method' equals 'check'

	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |

Scenario Outline: Editing the payment method to nothing
	Given The transaction type is '<transactionType>'
	And the following transaction data with a category 'food' and payment method 'cash'
	| Name    | Value      |
	| Date    | 2015-06-06 |
	| Amount  | 12         |
	| Comment | Stuff      |
	When I change the 'Method' to ''
	Then the handler returns an error indicator - 'There must be a payment method selected'

	Examples: 
	| testName    | transactionType |
	| editExpense | expense         |
	| editIncome  | income          |