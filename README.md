# PoseidonApi Documentation

## Installation

- Clone the repo : git clone https://github.com/Yann-dv/PoseidonApi.git
- Build and run the app
- Open your browser and go to https://localhost:5001/swagger/

## Usage

- The app is a REST API that allows you to manage a portfolio of assets
- You can create, read, update and delete some entities : Bids, CurvePoints, Ratings, Rules, Trades and Users
- This Api use a Swagger interface to help developers/clients to use the API and live test it
- Steps to use Swagger interface : 

### Authorization first : as the application is user-authorized based, you have to login first to get a token.
If you try to use the API, without be logged in as an authorized user, you will get a 401 error code.
<img src="https://github.com/Yann-dv/PoseidonApi/blob/main/img/auth0.png" width="70%" alt="Auth0">

1. Go to https://localhost:5001/swagger/
2. Open the Auth Controller
3. Click on "Try it out"
   <img src="https://github.com/Yann-dv/PoseidonApi/blob/main/img/auth1.png" width="70%" alt="Auth1">

4. Fill the parameters and click on "Execute" (use the username and password given by default, others won't work)
5. Copy the token given by the application
   <img src="https://github.com/Yann-dv/PoseidonApi/blob/main/img/auth2.png" width="70%" alt="Auth2">

6. Go back to the "Authorize" button on the top right of the page
7. Paste the token in the "Value" field and click on "Authorize"
   <img src="https://github.com/Yann-dv/PoseidonApi/blob/main/img/auth3.png" width="70%" alt="Auth3">

8. You can now use the API

### Controllers
1. Stay on https://localhost:5001/swagger/ endpoint
2. Open the entity controller you want to use
3. Choose the method you want to use
4. Click on the "Try it out" button
5. Fill the parameters and click on the "Execute" button
6. You can see the request URL, curl, response body and response headers in the "Response" section


### Using Postman
If you prefer to test the API using Postman, Import the PoseidonAPI.postman_collection.json file 
(located at root of the project) in Postman as follow :

1. Import Json collection
   <img src="https://github.com/Yann-dv/PoseidonApi/blob/main/img/postman1.png" width="70%" alt="Postman1">

2. First, launch the Aut POST method to get the token, copy and use it to replace the Authorization : Bearer token inside each request
   <img src="https://github.com/Yann-dv/PoseidonApi/blob/main/img/postman2.png" width="70%" alt="Postman2">

3. Use the methods as wanted.
4. Only Bids methodes are created in the PoseidonAPI.postman_collection.json as example, but feel free to can create the others as you want.

## Testing

- We use NUnit for the testing
- All CRUD operations for all entities and controllers are tested. We use the InMemoryDatabase for the testing and a Setup.cs to
  initialize a FakeDBContext with fake data for all entities.
- Use 'dotnet test' command to run all the tests

<img src="https://github.com/Yann-dv/PoseidonApi/blob/main/img/test_results.png" width="70%" alt="UnitTests results">