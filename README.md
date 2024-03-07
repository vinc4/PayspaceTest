## Tax Calculator App

## Overview
This is a web application that calculates taxes based on different criteria such as income and postal code. It provides various tax calculation methods including progressive, flat value, and flat rate.

## Features
Calculate Tax: Users can input their annual income and postal code to calculate their taxes.
Support for Different Tax Calculation Methods: The app supports progressive, flat value, and flat rate tax calculation methods.
History Tracking: Users can view their tax calculation history.
Exception Handling: Proper exception handling is implemented to provide user-friendly error messages.
### Technologies Used

## Backend:
ASP.NET Core for building the RESTful API
Entity Framework Core for database access
SQLite for database storage

## Frontend:
Blazor for building the user interface
HttpClient for making HTTP requests to the backend API

## Database:
SQLite database for storing postal codes and tax calculation settings
Dependency Injection: Used to manage the application's services and components
Exception Handling: Custom exceptions and error handling mechanisms are implemented to provide meaningful error messages to users.
Swagger: API documentation is generated using Swagger for easy reference.

## API Endpoints
Calculate Tax:
URL: /api/calculator/calculate-tax
Method: POST
### Request Body:
json
Copy code
{
  "income": 50000,
  "postalCode": "1000"
}

### Response:
json
Copy code
{
  "tax": 7500,
  "calculator": "Progressive"
}