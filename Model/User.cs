using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIRTUAL_LAB_API.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string HashPassword { get; set; }

        public DateTime BirthDate { get; set; }

        [ForeignKey(nameof(UserRole))]
        public int UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }

        //public User(string name, string middleName, string surname, string email, string phone, DateTime birthDate, UserRole userRole) { }

        //public void Register(string hashPassword) { }
        //public void Login(string hashPassword) { }

        //public void StayLogged(string token) { }
        //public void Logout(string token) { }

        //public void ChangePasswordRequest(string oldHashPassword, string newHashPassword) { }

        //public void DeleteAccountRequest(string token) { }
    }
}
