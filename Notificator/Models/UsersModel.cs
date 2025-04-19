using System.ComponentModel.DataAnnotations;

namespace Notificator.Models;

public class UsersModel
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? DiscordWebhookUrl { get; set; }

        public User(int id, string userId, string email, string password, string? name, string? discordWebhookUrl)
        {
            Id = id;
            UserId = userId;
            Email = email;
            Password = password;
            Name = name;
            DiscordWebhookUrl = discordWebhookUrl;
        }
    }

    public class UsersRq
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string PasswordCfm { get; set; }
        public string Email { get; set; }
        public string? DiscordWebhookUrl { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }

    public class LoginRq
    {
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "비밀번호를 입력해주세요.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}