# Article Generation

## Description

The Airplane Transaction Article Generator is a .NET Worker Service that periodically checks for new or updated transactions related to airplane companies in a database. It utilizes the OpenAI API to generate articles based on the retrieved transaction data and stores these articles back in the database.

## Technologies Used

- ASP.NET Core (latest version)
- OpenAI API
- MS SQL Server Database

## Setup Instructions

1. Clone the repository
2. Add your OpenAI API Key and URL (create the appsettings.json file)
3. Configure your database connection string also in the appsettings.json file

## Usage

To run the application, simply start the app. The background service will check for new or changed transactions every 10 seconds, generate articles about them using the OpenAI API, and store the results in the database while logging activity using ILogger<>.

## Additional Information

For more details on the OpenAI API, visit OpenAI Documentation.
