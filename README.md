# net-core-web-template
.NET Core 3.1 web application template with working examples of AWS Cognito Identity Provider utilities.
This project supports all the standard, self-service identity components you need to get a web application off the ground:
* Login/Logout
* Account Registration
* Account (E-mail) Confirmation
* Password Recovery
* Password Change
* Roles-based Authorization (Cognito User Groups)


## Initialization Steps
1. open a powershell terminal, navigate to the root of the repository, and run:
```
PS > .\initialize.ps1
```
2. you will be prompted to provide a name for your project

3. once you provide a name the solution will be updated appropriately

4. open your solution by opening the .sln file with Visual Studio and build the solution

**note:** you must install the .NET Core SDK 3.1 before building the solution - you can find that [here](https://dotnet.microsoft.com/download/dotnet-core/3.0)

## Project Setup Steps
1. the application is setup to use AWS Cognito Identity Provider.  To get this working you will need to create a Cognito User Pool in AWS and fill in the appropriate config values in the `appsettings.Development.json` file:
```
"AWS": {
  "Region": "<aws region code>",
  "UserPoolClientId": "<aws cognito pool client id>",
  "UserPoolClientSecret": "<aws cognito pool client secret>",
  "UserPoolId": "<aws cognito pool id>"
}
```
2. the project is also built around connecting to a database, though this is not required to run the application.  The current configuration of the project is built on leveraging a MySQL 8.0+ database.  Setup a MySQL database and update the default connection in the `appsettings.Development.json` file:
```
"ConnectionStrings": {
  "DefaultConnection": "server=;port=;database=;user=;password="
}
```
