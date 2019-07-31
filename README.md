
# Pactify

Pactify is very simple contract testing tool for .NET Core. It was inspired by [PACT.io](https://docs.pact.io/).

  |   | master  | develop  |
|---|--------|----------|
|AppVeyor|[![Build status](https://ci.appveyor.com/api/projects/status/0i8kk52yy53c5mm9/branch/master?svg=true)](https://ci.appveyor.com/project/GooRiOn/pactify/branch/master)|[![Build status](https://ci.appveyor.com/api/projects/status/0i8kk52yy53c5mm9/branch/develop?svg=true)](https://ci.appveyor.com/project/GooRiOn/pactify/branch/develop)|
|CodeCov|[![codecov](https://codecov.io/gh/GooRiOn/Pactify/branch/master/graph/badge.svg)](https://codecov.io/gh/GooRiOn/Pactify)|[![codecov](https://codecov.io/gh/GooRiOn/Pactify/branch/develop/graph/badge.svg)](https://codecov.io/gh/GooRiOn/Pactify)

# Installation

### Package manager

```bash

Install-Package Pactify -Version 1.0.0

```

  

### .NET CLI

```bash

dotnet add package Pactify --version 1.0.0

```

  

# Getting started

Start with creating a simple unit test on **consumer** side:

```csharp
[Fact]  
public async Task Consumer_Should_Create_A_Pact()  
{  
    var options = new PactDefinitionOptions  
    {  
        IgnoreContractValues = true,  
        IgnoreCasing = true  
    };  
    
    await PactMaker  
        .Create(options)  
        .Between("orders", "parcels")  
        .WithHttpInteraction(cb => cb  
            .Given("There is a parcel with some id")  
            .UponReceiving("A GET Request to retrieve the parcel")  
            .With( request => request  
                .WithMethod(HttpMethod.Get)  
                .WithPath("api/parcels/1"))  
            .WillRespondWith(response => response  
                .WithHeader("Content-Type", "application/json")  
                .WithStatusCode(HttpStatusCode.OK)  
                .WithBody<ParcelReadModel>()))  
        .PublishedAsFile("../../../../../pacts")  
        .MakeAsync();   
}
```
 
 In the above example the result JSON file will be saved in the specified directory. Alternatively you can publish the  JSON via **HTTP** as follows:

```csharp
.PublishViaHttp("http://myapi.com/pacts", HttpMethod.Post, apiKey = "myApiKey");
``` 

The above unit test will create a PACT JSON between **orders** and **parcels** services:


```javascript
{  
  "consumer": {  
    "name": "orders"  
  },  
  "provider": {  
    "name": "parcels"  
  },  
  "interactions": [  
    {  
      "provider_state": "There is a parcel with some id",  
      "description": "A GET Request to retrieve the parcel",  
      "request": {  
        "method": "GET",  
        "path": "api/parcels/1"  
  },  
      "response": {  
        "headers": {  
          "content-Type": "application/json"  
  },  
        "status": 200,  
        "body": {  
          "id": "00000000-0000-0000-0000-000000000000",  
          "name": null,  
          "price": 0.0  
  }  
      }  
    }  
  ],  
  "options": {  
    "ignoreCasing": true,  
    "ignoreContractValues": true  
  }  
}
```
  
  Because the whole idea assumes that consumer is always right this part of the testing **should always pass**! 

Having that, you can move to the verifying the pact on the provider's side:

```csharp
[Fact]  
public async Task Provider_Should_Meet_Consumers_Expectations()  
{  
    await PactVerifier  
        .CreateFor<Startup>()  
        .Between("orders", "parcels")  
        .RetrieveFromFile("../../../../../pacts")  
        .VerifyAsync();  
}
```

Notice that ``PactVerifier`` was created using **``Startup``** class. Thanks to that your provider's API will be ran in memory using ``Microsoft.AspNetCore.TestHost`` package with proxy ``HttpClient`` object. If for some reason this is not a right solution, you can simply pass your custom ``HttpClient`` instance instead:

```csharp
PactVerifier.Create(myHttpClient)
...
```

If the pact's verification passes the above test should pass. Otherwise proper error messages are going to be displayed to you.


# Integration with PACT broker
Pactify integrates easily with PACT Broker which allows you to manage and read (via UI) your pacts on external HTTP server running inside Docker container. First, clone the broker's repository:

```bash
git clone https://github.com/DiUS/pact_broker-docker.git
```

Navigate to the repository and run the **``docker-compose.yml``** file using the following command:

```bash
docker-compose up -d
```
This will run all the broker's necessary infrastructure in the **detached** mode. The UI should be exposed at **``localhost:9292``**:

![UI](https://user-images.githubusercontent.com/7096476/62197643-82040c80-b380-11e9-9438-d3b03d39ce17.png)

Change your code to publish and receive the pact from the broker:

```csharp
[Fact]  
public async Task Consumer_Should_Create_APact()  
{  
    var options = new PactDefinitionOptions  
  {  
        IgnoreContractValues = true,  
        IgnoreCasing = true  
  };  
  
    await PactMaker  
		.Create(options)  
        .Between("orders", "parcels")  
        .WithHttpInteraction(cb => cb  
            .Given("There is a parcel with some id")  
            .UponReceiving("A GET Request to retrieve the parcel")  
            .With( request => request  
                .WithMethod(HttpMethod.Get)  
                .WithPath("api/parcels/1"))  
            .WillRespondWith(response => response  
                .WithHeader("Content-Type", "application/json")  
                .WithStatusCode(HttpStatusCode.OK)  
                .WithBody<ParcelReadModel>()))  
        .PublishedViaHttp("http://localhost:9292/pacts/provider/parcels/consumer/orders/version/1.2.104", HttpMethod.Put) 
        .MakeAsync();  
}  
  
[Fact]  
public async Task Provider_Should_Meet_Consumers_Expectations()  
{  
    await PactVerifier  
        .CreateFor<Startup>()  
        .Between("orders", "parcels")  
        .RetrievedViaHttp("http://localhost:9292/pacts/provider/parcels/consumer/orders/latest")  
        .VerifyAsync();  
}

```

Both tests should be **green**. After refreshing the broker's UI, you should see the pact:

![ui-pact](https://user-images.githubusercontent.com/7096476/62197921-0eaeca80-b381-11e9-80f2-fd8961a8c891.png)

Once you click on it, you should be able to inspect your pact file:

![ui-pact-details](https://user-images.githubusercontent.com/7096476/62198044-37cf5b00-b381-11e9-91b7-2d3e22e88bb4.png)

# Contributing
Want to help developing Pactify? Awesome! Here you can find [contributor guide](https://github.com/GooRiOn/Pactify/blob/develop/CONTRIBUTING.md) ;)

# Icon

Icon made by Freepik from [www.flaticon.com](http://flaticon.com) is licensed by [Creative Commons BY 3.0](http://creativecommons.org/licenses/by/3.0/)
