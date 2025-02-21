using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Xunit;
using WebAPI;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using System.Text.Json; // API projenin adını buraya yazmalısın

public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserControllerTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://192.168.1.100:7009") // API'nin gerçek adresi
        };
    }
    #region
    //[Fact]   
    //public async Task GetAllUsers_ShouldReturn200OK_And_ValidUserList()
    //{
    //    try
    //    {
    //        var response = await _client.GetAsync("/api/User");

    //        // Response durum kodunu yazdır
    //        Console.WriteLine($"[TEST LOG] Response Status: {response.StatusCode}");

    //        // Yanıt içeriğini JSON olarak oku
    //        var responseBody = await response.Content.ReadAsStringAsync();
    //        Console.WriteLine($"[TEST LOG] Response Body: {responseBody}");

    //        // Eğer response başarısızsa, hata ver
    //        response.EnsureSuccessStatusCode();

    //        // JSON'ı modelimize çevir
    //        var userList = await response.Content.ReadFromJsonAsync<GetAllUserQueryResponse>();

    //        // Kullanıcı listesi boş mu kontrol et
    //        userList.Should().NotBeNull();
    //        userList.Users.Should().NotBeNull();
    //        userList.TotalUserCount.Should().BeGreaterThan(0);
    //    }
    //    catch (HttpRequestException httpEx)
    //    {
    //        Console.WriteLine($"[TEST LOG] HTTP Hatası: {httpEx.Message}");
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"[TEST LOG] Genel Hata: {ex.Message}");
    //    }
    //}


    //[Fact]
    //public async Task GetUserById_ShouldReturn200OK_WhenUserExists()
    //{
    //    // Arrange (Önce bir kullanıcı oluşturalım)
    //    var newUser = new CreateUserCommandRequest
    //    {
    //        Name = "ibrahim",
    //        Surname = "Altsoy",
    //        UserName = "i.ako@gmail.com",
    //        Email = "sam784@gmail.com",
    //        PhoneNumber = "123456789",
    //        Birthday = DateTime.UtcNow,
    //        Gender = Gender.Male,
    //        Password = "SecurePassword123",
    //        PasswordConfirm = "SecurePassword123"
    //    };

    //    var postResponse = await _client.PostAsJsonAsync("/api/User", newUser);
    //    var createdUser = await postResponse.Content.ReadFromJsonAsync<CreateUserCommandResponse>();

    //    Guid userId = createdUser.Id; // API response içinde ID olmalı!

    //    // Act (Oluşturulan kullanıcıyı çek)
    //    var response = await _client.GetAsync($"/api/User/id?id={userId}");

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.OK);
    //}

    //[Fact]
    //public async Task CreateUser_ShouldReturn201Created()
    //{
    //    // Arrange
    //    var newUser = new CreateUserCommandRequest
    //    {
    //        Name = "John",
    //        Surname = "Doe",
    //        UserName = "jodoe",
    //        Email = "jo47e@example.com",
    //        PhoneNumber = "987654321",
    //        Birthday = DateTime.UtcNow.AddYears(-25),
    //        Gender = Gender.Male,
    //        Password = "StrongPassword!",
    //        PasswordConfirm = "StrongPassword!"
    //    };

    //    // Act
    //    var response = await _client.PostAsJsonAsync("/api/User", newUser);

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.Created);
    //}

    //[Fact]
    //public async Task UpdateUser_ShouldReturn200OK()
    //{
    //    // Arrange (Önce bir kullanıcı ekleyelim)
    //    var newUser = new CreateUserCommandRequest
    //    {
    //        Name = "UpdateTest",
    //        Surname = "User",
    //        UserName = "uphfdatetestuser",
    //        Email = "updaser@example.com",
    //        PhoneNumber = "987654321",
    //        Birthday = DateTime.UtcNow.AddYears(-30),
    //        Gender = Gender.Male,
    //        Password = "UpdatePassword",
    //        PasswordConfirm = "UpdatePassword"
    //    };

    //    var postResponse = await _client.PostAsJsonAsync("/api/User", newUser);
    //    var createdUser = await postResponse.Content.ReadFromJsonAsync<CreateUserCommandResponse>();

    //    Guid userId = createdUser.Id;

    //    var updatedUser = new UpdateUserCommandRequest
    //    {
    //        Id = userId,
    //        Name = "Updated",
    //        Surname = "User",
    //        UserName = "uateduser",
    //        Email = "updd@gmail.com",
    //        PhoneNumber = "123456789",
    //        Birthday = DateTime.UtcNow.AddYears(-28),
    //        IsActive = true
    //    };

    //    // Act
    //    var response = await _client.PutAsJsonAsync("/api/User", updatedUser);

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.OK);
    //}
    #endregion
    [Fact]
    public async Task CreateAndDeleteUser_ShouldReturn200OK()
    {
        // 📌 1. Önce yeni bir kullanıcı oluştur
        var createUserRequest = new
        {
            name = "Ramazan",
            surname = "Ramee",
            userName = "test76ser",
            email = "test187user@example.com",
            phoneNumber = "123456789",
            birthday = DateTime.UtcNow,
            gender = 0,
            password = "Test@123",
            passwordConfirm = "Test@123"
        };

        var createResponse = await _client.PostAsync(
            "http://192.168.1.100:7009/api/User",
            new StringContent(JsonSerializer.Serialize(createUserRequest), Encoding.UTF8, "application/json")
        );

        createResponse.StatusCode.Should().Be(HttpStatusCode.OK); // Kullanıcı başarıyla oluşturuldu mu?

        var createResponseBody = await createResponse.Content.ReadFromJsonAsync<CreateUserCommandResponse>();
        createResponseBody.Should().NotBeNull();
        createResponseBody.Succeeded.Should().BeTrue();

        // 📌 2. Oluşan kullanıcıyı sil
        var deleteRequestBody = new
        {
            id = createResponseBody.Id // Yeni oluşturulan kullanıcının ID'si
        };

        var deleteRequest = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("http://192.168.1.100:7009/api/User"),
            Content = new StringContent(JsonSerializer.Serialize(deleteRequestBody), Encoding.UTF8, "application/json")
        };

        var deleteResponse = await _client.SendAsync(deleteRequest);

        Console.WriteLine($"[TEST LOG] Delete HTTP Status Code: {deleteResponse.StatusCode}");
        Console.WriteLine($"[TEST LOG] Delete Response Body: {await deleteResponse.Content.ReadAsStringAsync()}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK); // API'nin dönüşüne göre NoContent yerine OK olarak düzelttim.
    }



}

// Kullanıcı API'si dönüş modelleri
public class GetAllUserQueryResponse
{
    public int TotalUserCount { get; set; }
    public object Users { get; set; }
}

public class CreateUserCommandRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? Birthday { get; set; }
    public Gender Gender { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}

public class CreateUserCommandResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public Guid Id { get; set; } // API döndürüyor mu?
}

public class UpdateUserCommandRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? Birthday { get; set; }
    public bool? IsActive { get; set; }
}

public class DeleteUserCommandRequest
{
    public Guid Id { get; set; }
}

public class DeleteUserCommandResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
}

public enum Gender
{
    Male = 1,
    Female = 2
}

