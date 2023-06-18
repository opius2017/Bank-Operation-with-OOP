# SQ016 Weekly Tasks
### Week 1 - Task

## Introduction :
This task is aimed at evaluating your understanding and implementation of OOP in your project.

## Challenge
Your task is to create a console application that models a Bank and its operations using OOP concepts.

The result should be displayed in a tabular form as shown below: 

|-------------------|-------------------------------|--------------------------|---------------------|----------|
| FULL NAME  | ACCOUNT NUMBER  | ACCOUNT TYPE | AMOUNT BAL | NOTE |
|-------------------|-------------------------------|--------------------------|---------------------|----------|
| John Doe       |        0987654321         |          Savings        |      10,000        |   Gift   | |--------------------------------------------------------------------------------------------------------------|
| John Doe       |        0987654311         |          Current         |      100,000      |  Food  | |--------------------------------------------------------------------------------------------------------------|

## Functional requirements
-	A single user can have multiple accounts but they must not be of same account type
-	Users should be able to deposit funds into an account. 
-	Users should be able to withdraw funds from an account.
-	Users should be able to Transfer funds between accounts.

## Acceptance requirements
-   Test for valid results.
-   Test for invalid results

## Required Validations
-  Account owners can not withdraw past the minimum balance for a savings account (1000 Naira). 
-  Account owners can empty their account for a current account. 
-  Customer names (first and last) should be sanitized so that it does not start with a digit or a small letter 

## Task requirements
-  Use InMemory to store data.
-  All functional requirements should be completed.
