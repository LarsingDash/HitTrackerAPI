using HitTrackerAPI.Controllers;
using HitTrackerAPI.Models;
using HitTrackerAPI.Repositories.AccountRepositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HitTrackerTest;

public class AccountControllerTests
{
    private readonly IAccountRepository _repository = new AccountMock();
    private AccountController _controller;

    [OneTimeSetUp]
    public void AccountControllerSetup()
    {
        _controller = new AccountController(_repository);
    }

    //Tries to create an account
    //Since the mock repository has the accounts 0 and 1, this should make an account with ID 2
    [Test]
    public async Task CreateAccount()
    {
        //Create the account
        var result = await _controller.CreateAccount();

        //Assert that the result was Ok
        Assert.That(result, Is.Not.EqualTo(null));
        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        //Assert that the account (with ID 2) was successfully created
        Assert.That((result as OkObjectResult)!.Value, Is.EqualTo("2"));
    }
}