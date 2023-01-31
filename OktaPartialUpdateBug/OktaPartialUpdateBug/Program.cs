#if OKTA_SDK_6
using Okta.Sdk.Api;
using Okta.Sdk.Client;
using Okta.Sdk.Model;
using Xunit;

var userApi = new UserApi(new Configuration
{
    OktaDomain = "<your_okta_domain>",
    Token = "<your_api_token>"
});

var createdUser = await userApi.CreateUserAsync(new CreateUserRequest
{
    Profile = new UserProfile
    {
        FirstName = "FirstName",
        LastName = "LastName",
        Login = $"{Guid.NewGuid()}@login.com",
        Email = $"{Guid.NewGuid()}@email.com",
        PrimaryPhone = "123-321-4444",
        MobilePhone = "321-123-5555",
        Locale = "en_US",
        SecondEmail = $"{Guid.NewGuid()}@second.com",
        Timezone = "Japan",
    },
});

string newPhone = $"321-123-1000";
var updatedUser = await userApi.PartialUpdateUserAsync(createdUser.Id, new UpdateUserRequest
{
    Profile = new UserProfile
    {
        PrimaryPhone = newPhone,
    },
});

Assert.Equal(createdUser.Profile.FirstName, updatedUser.Profile.FirstName);
Assert.Equal(createdUser.Profile.LastName, updatedUser.Profile.LastName);
Assert.Equal(createdUser.Profile.Login, updatedUser.Profile.Login);
Assert.Equal(createdUser.Profile.Email, updatedUser.Profile.Email);
Assert.NotEqual(createdUser.Profile.PrimaryPhone, updatedUser.Profile.PrimaryPhone);
Assert.Equal(newPhone, updatedUser.Profile.PrimaryPhone);
Assert.Equal(createdUser.Profile.MobilePhone, updatedUser.Profile.MobilePhone);
Assert.Equal(createdUser.Profile.Locale, updatedUser.Profile.Locale);
Assert.Equal(createdUser.Profile.SecondEmail, updatedUser.Profile.SecondEmail);
Assert.Equal(createdUser.Profile.Timezone, updatedUser.Profile.Timezone);
#else
using Okta.Sdk;
using Okta.Sdk.Configuration;
using Xunit;

var configuration = new OktaClientConfiguration
{
    OktaDomain = "<your_okta_domain>",
    Token = "<your_api_token>"
};

var oktaClient = new OktaClient(configuration);

var createdUser = await oktaClient.Users.CreateUserAsync(new CreateUserRequest
{
    Profile = new UserProfile
    {
        FirstName = "FirstName",
        LastName = "LastName",
        Login = $"{Guid.NewGuid()}@login.com",
        Email = $"{Guid.NewGuid()}@email.com",
        PrimaryPhone = "123-321-4444",
        MobilePhone = "321-123-5555",
        Locale = "en_US",
        SecondEmail = $"{Guid.NewGuid()}@second.com",
        Timezone = "Japan",
    },
});

string newPhone = $"321-123-1000";
var oktaUser = new User();
oktaUser.SetProperty("id", createdUser.Id);
oktaUser.Profile = new UserProfile
{
    PrimaryPhone = newPhone,
};
var updatedUser = await oktaClient.Users.PartialUpdateUserAsync(oktaUser, oktaUser.Id);

Assert.Equal(createdUser.Profile.FirstName, updatedUser.Profile.FirstName);
Assert.Equal(createdUser.Profile.LastName, updatedUser.Profile.LastName);
Assert.Equal(createdUser.Profile.Login, updatedUser.Profile.Login);
Assert.Equal(createdUser.Profile.Email, updatedUser.Profile.Email);
Assert.NotEqual(createdUser.Profile.PrimaryPhone, updatedUser.Profile.PrimaryPhone);
Assert.Equal(newPhone, updatedUser.Profile.PrimaryPhone);
Assert.Equal(createdUser.Profile.MobilePhone, updatedUser.Profile.MobilePhone);
Assert.Equal(createdUser.Profile.Locale, updatedUser.Profile.Locale);
Assert.Equal(createdUser.Profile.SecondEmail, updatedUser.Profile.SecondEmail);
Assert.Equal(createdUser.Profile.Timezone, updatedUser.Profile.Timezone);
#endif
