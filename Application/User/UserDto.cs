using DotnetWsRef.Domain;
using System.Text.Json.Serialization;

namespace DotnetWsRef.Application.User;

public class UserDto
{
    [JsonPropertyName("ID")]
    public Guid? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    public UserDto()
    { }

    public UserDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public UserModel NewUser()
    {
        if (Name == null)
        {
            throw new ArgumentNullException(nameof(Name));
        }

        return new UserModel(Guid.NewGuid(), Name);
    }

    public UserModel ToUserModel()
    {
        var isIdNull = Id == null;
        var isNameNull = Name == null;
        if (isIdNull || isNameNull)
        {
            throw new ArgumentNullException($"{(isIdNull ? nameof(Id) : string.Empty)} {(isNameNull ? nameof(Name) : string.Empty)}".Trim());
        }

        return new UserModel(Id.Value, Name);
    }

    public static UserDto From(UserModel user)
    {
        return new UserDto(user.Id, user.Name);
    }
}