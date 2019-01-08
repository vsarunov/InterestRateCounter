# InterestRateCounter

First noticed from reading the requirements and questions:
1. 	The asked response, should not be put into a single api response. It should be seperated into different controllers. Such as:
	PersonController, InterestRateController. Interest rate should not have any knowledge of agreement or the personal details, only the id
	of the person it is calculated for.
	Front-end if such exist should make separate API calls to get all the data.
	For example interest rate endpoint to calculation would return something like this:
	
	{
		"newRate": 1.9,
		"currentRate": 2.4,
		"difference": 0.5
	}
	
	
2.  Do we store customer details and agreements only when they ask for interest rate calculation?
3.  Do we use only the initial data? e.g We calculate interest rate only for pre-existing customer? Can we accept new people?
4.	Do we have to block all invalid "Base rate codes" submitted?
5. 	If Margin is fixed, do we re-use the old one, when calculating, that is stored with predefined data or do we calculate with the margin submitted with the agreement?
6.  Do we add a new agreement each time a request comes in for interest rate calculation?
7.  It is said that the user "submits agreement and new base rate code", thew new base rate could should be a part of the agreement if it is part of the model asked?
8.  Is 0 as amount,margin or agreement duration a valid request?
9.  Return "Information about person and agreement" with the new agreement or the current one?
10. What do we return if we were un-able to calculate new itnerest rate? Current information?

I have defaulted to use the margin that is submitted per request. As this seemed to be the more logical solution. As well as to save new agreement each time a request comes in
as long as there is an existing user with the id in the agreement. I have used the new base rate code from the agreement submitted during request as this seemed more appropriate then
submit it separately from the agreement if it is already part of it. On top of that to the agreement model I have added a timestamp as how would you track the previous agreement.

Nuget packages:

1. StructureMap dependency injection as it is a more elegant and readable way to register.
2. AutoMapper to map the result coming from the data layer, as the data layer should not bleed into the entry point.
3. Serilog to logto file while running application

Database:

I have used in memory database to be able to spin up one on runtime, to make it easier to test.
As well I have used one to many relationship between a customer and and agreement where one customer can have many agreements.

I have used a structure of controller -> service -> repository. We are talking to abstractions all the time. Controller should not speak directly to the data layer
and data layer should not bleed with any information to the entry point, that is why in between there is a service, which facilitates communication between the two
and performs any data gatherins and manipulation needed. 

By using abstractions I am able to hide the implementation of where is the data comming from in the repository, thus service does not care
and we can in the future for example replace the way we get data in the repository and it should not effect any other parts of the application.

The uri where to take the base rate value is store in appsettings.json, however it is just the beginning of the uri, the main uri.
As if we would need to replace it, we would not be replacing the full route of the api call, that is why the remaning part of it, the route
part is stored in the repository to use.

When the request comes in a validation kicks in ofthe model and the base rate code. I would implement a separate validation service whereI would be
able to define rules of validation if I had more time.



You can test the application by making a PUT request via swagger with, for intance, the following json body:

{
  "id": 0,
  "amount": 5500,
  "margin": 2.9,
  "baseRateCode": "VILIBOR1m",
  "duration": 72,
  "customerId": 78706151287,
}

You can replace the base rate code value and the customer id to one of the two existing ones.


What would I do with more time:

Add more security, e.g csp police, antiforgery, add security header, add logging middleware.
Structure Unit test bit more to make them more readable and impliment DRY principal to them as well as write more of them.
Implement more request object validation based on requirements.
