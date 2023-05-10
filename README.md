# PoseidonApi

## Installation

- Clone the repo : git clone https://github.com/Yann-dv/PoseidonApi.git
- Build and run the app
- Open your browser and go to https://localhost:5001/swagger/

## TestCases

- We use NUnit for the testing
- All CRUD operations for all entities and controllers are tested. We use the InMemoryDatabase for the testing and a Setup.cs to
  initialize a FakeDBContext with fake data for all entities.
- Use 'dotnet test' command to run all the tests

<img src="https://github.com/Yann-dv/PoseidonApi/blob/main/img/test_results.png" width="70%" alt="UnitTests results">