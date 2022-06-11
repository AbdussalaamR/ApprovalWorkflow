﻿using System.Collections.Generic;
using MEMOJET.Entities;
using MEMOJET.Identity;

namespace MEMOJET.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public  string UserName{ get; set; }
        //public  string Password{ get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Email { get; set; }
        public ICollection<UserForm> UserForms { get; set; } = new List<UserForm>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    public class CreateUserRequestModel
    {
        public  string UserName{ get; set; }
        public  string Password{ get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; } 
        public  string Email { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
    public class UpdateUserRequestModel
    {
        public  string UserName{ get; set; }
        public  string Password{ get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); 
    }

    public class UserResponseModel:BaseResponse
    {
        public UserDto Data { get; set; }
    }
        
    public class LoggedInUserResponseModel:BaseResponse
    {
        public UserDto Data { get; set; }
        public IList<int> RoleIds { get; set; } = new List<int>();
    }
    public class UsersResponseModel:BaseResponse
    {
        public IList<UserDto> Data { get; set; }
    }
}