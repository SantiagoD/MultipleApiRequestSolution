# Multiple Quote API Request 

An example on C# and .NET 5.0 that calls multiple APIs to quote a shipping service and choose the best deal. 


## Description

We consider APIs that respond with the same data in different formats and the system tries to identify the lowest offer and return it in the least amount of time.
The solution contains Sample APIs, a main Console library that process the requests and a Test project.

We have 3 Sample APIs, including one that consumes and returns XML. The calls are all POST Verb requests and an authorization header can be configured and sent, so we consider individual credentials for the API´s.


## Objective

The main idea is to include asynchronous calls for each API so we don´t have to wait for the response of each request.


![Sync and Async flows, one repeats n times, the other calls 3 requests](https://github.com/SantiagoD/MultipleApiRequestSolution/blob/master/async_sync.JPG?raw=true "Sync/Async differences")

Sending multiple requests at the same time we reduce the response time, depending on the longest duration of the set as our critical path.

## MultipleApiRequest Structure
To organize the artifacts in the project we have a hierarchy of a creator/factory abstract class called *CompanyApiCreator* that allows to create an instance of one of the used APIs utility classes.


![CompanyApiCreator class diagram](https://github.com/SantiagoD/MultipleApiRequestSolution/blob/master/creator_class.JPG?raw=true "CompanyApiCreator class diagram")

We have a factory method *CreateCompanyApi* for instantiating each type.
That abstract class also encapsulates the method to run the whole process of creating the requests and processing the results.

For Company API utility classes we have an abstract class *ICompanyApi* that implements the generic call on the method *SearchPrice* so we can easily maintain that code and add new APIs.


![ICompanyApi class diagram](https://github.com/SantiagoD/MultipleApiRequestSolution/blob/master/api_class.JPG?raw=true "ICompanyApi class diagram")

That class also defines common properties that can be setted for each API. 
- *ApiType*: JSON or XML
- *Name*: Name that identifies the company or API
- Credentials: Settings to build an authorization header.
- URL: Base URL for the API
- RequestUri: URI that completements the base URL to describe the action used for the call.
---
> Those informations can be stored in settings files, cloud secrets storage or even databases.
>
To deal with the differences in those APIs we have two methods that must be implemented in the children classes:

- *BuildInput*: Takes an standard input for the request and converts it on an specific input object for the current API.
- *ReadResult*: Takes the output as an JSON or XML string and parses to recover an standard output.

---

Completing the solution we have two services used to actually send the calls:
- JsonRequestService: Calls with JSON format.
- XmlRequestService: Calls with XML format.

## Sample APIs
We have three .NET 5.0 simple APIs made for this project, their code is included and they are published on Azure to easen the validation and testing part.

**FirstCompanyApi**
- Input Format:
```json
{
  "contactAddress": {
    "streetName": "Montreal Road",
    "streetNumber": 1343,
    "zipCode": "K1L 6C7"
  },
  "warehouseAddress": {
    "streetName": "Brew Creek Rd",
    "streetNumber": 1500,
    "zipCode": "V0N 3G0"
  },
  "packageDimensions": [
    {
      "length": 152,
      "width": 102,
      "height": 102
    },
    {
      "length": 432,
      "width": 318,
      "height": 267
    }
  ]
}
```
- Output Format:
```json
{
    "total": 15.6
}
```
- URL: https://firstcompanyapisdb.azurewebsites.net/api/Pricing

**SecondCompanyJsonApi**
- Input Format: (uses cm for dimensions)
```json
{
  "consignee": "1343, Montreal Road, K1L 6C7",
  "consignor": "1500, Brew Creek Rd, V0N 3G0",
  "cartons": [
    {
      "length": 16,
      "width": 11,
      "height": 11
    },
    {
      "length": 44,
      "width": 32,
      "height": 27
    }
  ]
}
```
- Output Format:
```json
{
    "amount": 17.8
}
```
- URL: https://secondcompanyjsonapisdb.azurewebsites.net/api/Consignment

**ThirdCompanyXmlApi**
- Input Format: 
```xml
<?xml version="1.0" encoding="UTF-8"?>
<QuoteParam>
	<source>
		<fullAddress>string</fullAddress>
		<zipCode>string</zipCode>
	</source>
	<destination>
		<fullAddress>string</fullAddress>
		<zipCode>string</zipCode>
	</destination>
	<packages>
		<package>string</package>
	</packages>
</QuoteParam>
```
- Output Format:
```xml
<QuoteResult xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Quote>8.54</Quote>
</QuoteResult>
```
- URL: https://thirdcompanyxmlapisdb.azurewebsites.net/api/Quote

