﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Linq;

namespace SquareApplication.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SquaresEntities : DbContext
    {
        public SquaresEntities()
            : base("name=SquaresEntities")
        {
        }

        public User GetUser(string name)
        {
            var user = Users.SingleOrDefault(u => u.name == name);
            return user;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<Set_Tag> Set_Tag { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Tile> Tiles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<vSetWithTilesAndUser> vSetWithTilesAndUsers { get; set; }
    }
}
