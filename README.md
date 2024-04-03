# SeoTracker
This repository contains the source code for the SeoTracker, including both the Angular front-end application (frontend) and the ASP.NET Web API back-end (backend).

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

## Prerequisites
Before you begin, ensure you have the following installed:

Node.js and npm (for the Angular front-end)
.NET SDK (for the ASP.NET back-end)
Installing
A step-by-step series of examples that tell you how to get a development environment running.

## Frontend (Angular)
Navigate to the frontend directory:
`cd frontend`

Install the required npm packages:
`npm install`

Serve the application on a development server:
`ng serve`

The Angular application will be available at http://localhost:4200/.

## Backend (ASP.NET Web API)
Navigate to the backend directory:
`cd backend\src\SeoTracker.Api.Host`

Build and run the ASP.NET Web API project:

`dotnet build`
`start-process src\SeoTracker.Api.Host\bin\Debug\net8.0\SeoTracker.Api.Host.exe`

The ASP.NET Web API will be available at http://localhost:5000.
