Feature: Products

@mytag
Scenario: Adds Product with correct data
    Given Correct Product
    When Product is added
    Then Product should match correct product
    
Scenario: Adding Product with 0 quantity should create unavailable product
    Given Product with 0 quantity
    When Product is added
    Then Product should not be available
    
Scenario: Adding and Updating Product should keep unchanged Product in History
    Given Correct Product
    When Product is added
    And Product is updated
    Then Product should contain changes in history
    