Feature: Validate Services Page Title
As a User I want to validate that the access link Text 
matches the actual Page title

@tag1
Scenario Outline: Click one of the available items on the service page
	Given I navigate to the EPAM website
	When I hover over the the Service tab on the Navigation bar
	And Click one the '<category>' links  
	Then The displayed page title should match the '<actual title>' of the previously clicked link
	And The page should display a section named 'Our Related Expertise'

	Examples: 
	|category	   |actual title   |
	|Generative AI |Generative AI  |
	|Responsible AI|Responsible AI |