Feature: Products

@mytag
Scenario: Adds Product with correct data
    Given Correct Product
    When Product is added
    Then Product should match correct product