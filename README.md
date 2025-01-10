# DRY Mailjet Client Library Package

## Table of Contents
1. [Introduction](#introduction)
2. [Usage Guide](#usage-guide)
3. [Links](#links)

### Introduction
***
This package provides utility methods for sending email using the Mailjet Client with C#.

## Usage Guide
***
Download the package ```DRY.MailJetClient.Library```.'
Register the service in the service collection by calling ```ConfigureMailJet()``` on IServiceCollection.
The method accepts four other arguments: ```string: apiKey```, ```string: apiSecret```, ```string: emailAddress```, and ```string: senderName```.
The ```SendAsync()``` method has overloads for sending single and multiple emails and also methods for sending emails with attachements
Check out the [Github Repository](https://github.com/ojotobar/DRYMailjetClientLibrary) to see the available methods implemented in each category

## Links
***
To view the source code or get in touch:
* [Github Repository Link](https://github.com/ojotobar/DRYMailjetClientLibrary)
* [Send Me A Mail](mailto:ojotobar@gmail.com)
