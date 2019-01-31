using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SecondTask.Models
{
    public class Person
    {
        public int PersonId {get; set;}
        public string AccountId { get; set;}
        public string AccountName {get;set;}
        public bool IsBlocked { get; set;}
        public DateTime LastIn { get;set;}
    }
}