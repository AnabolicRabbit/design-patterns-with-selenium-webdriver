Feature: SpecFlowYandexMailTests
	In order to find needed emails
	As a SpecFlow user
	I want to be able to navigate to pages through yandex mail controls

Background:
	Given I log in to yandex mail box

@smoke
Scenario: The logged in user name should match the entered login
	When I get the actual user name from User Account option
	Then the actual user name should match the entered login

@smoke
Scenario Outline: The created email should be present in the Draft folder
	When I create a draft email with '<subject>' and '<body>'
	And get the actual subject
	Then the email with '<subject>' should be present in the folder

	Examples:
		| subject  | body          |
		| Greeting | Hi, Selenium! |
		| One      | First email   |

@smoke
Scenario Outline: Addressee, Subject and Body from the created draft email should match the entered ones
	When I create a draft email with '<subject>' and '<body>'
	And get the actual addressee
	And get the actual subject
	And get the actual body
	Then the expected addresse, the '<subject>' and the '<body>' from the draft email should match the entered ones

	Examples:
		| subject | body         |
		| Two     | Second email |

@smoke
Scenario Outline: The sent email should not be present in the Draft folder
	When I create a draft email with '<subject>' and '<body>'
	And get number of emails in the Draft folder before sending
	And send the created draft email
	And navigate to the Draft folder
	And get difference between emails number before and after sending
	Then the sent email should not be present in Draft folder

	Examples:
		| subject | body        |
		| Three   | Third email |

@smoke
Scenario Outline: The sent email should be present in the Sent folder
	When I create a draft email with '<subject>' and '<body>'
	And send the created draft email
	And navigate to the Sent folder
	And get the actual subject
	Then the email with '<subject>' should be present in the folder

	Examples:
		| subject | body         |
		| Four    | Fourth email |

@smoke
Scenario Outline: The login message should be diplayed on the Sign Out form
	When I create a draft email with '<subject>' and '<body>'
	And send the created draft email
	And navigate to the Sent folder
	And navigate to the Sign Out form
	And get the actual login message
	Then the expected login message should be displayed on the form

	Examples:
		| subject | body        |
		| Five    | Fifth email |

@smoke
Scenario Outline: The deleted draft email should be present in the Deleted folder
	When I create a draft email with '<subject>' and '<body>'
	And delete the draft email
	And navigate to the Deleted folder
	And get the actual subject
	Then the email with '<subject>' should be present in the folder

	Examples:
		| subject | body        |
		| Six     | Sixth email |

@smoke
Scenario Outline: Deleted draft emails should not be present in the Draft folder
	When I create a draft email with '<subject>' and '<body>'
	And navigate to the Draft folder
	And delete all draft emails
	And get the actual info message
	Then the expected info message should be displayed in the Draft folder

	Examples:
		| subject | body          |
		| Seven   | Seventh email |

@smoke
Scenario Outline: Deleted draft emails should not be present in the Deleted folder
	When I create a draft email with '<subject>' and '<body>'
	And delete the draft email
	And navigate to the Deleted folder
	And click the clear mark
	And get the actual info message
	Then the expected info message should be displayed in the Deleted folder

	Examples:
		| subject | body         |
		| Eight   | Eighth email |

@smoke
Scenario Outline: The deleted by dragging picture should be present in the Recycle Bin
	When I navigate to the Disk folder
	And get a picture for deletion '<name>'
	And drag the pictire with '<name>' to the RecyclerBin 'Корзина'
	And navigate to the Recycler Bin 'Корзина'
	And get the deleted picture '<name>'
	Then the deleted picture should be present in the Recycle Bin

	Examples:
		| name       |
		| Мишки.jpg  |
		| Москва.jpg |