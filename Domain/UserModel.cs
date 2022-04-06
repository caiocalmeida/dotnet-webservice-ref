using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetWsRef.Domain;

[Table("users")]
public class UserModel
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    public UserModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}