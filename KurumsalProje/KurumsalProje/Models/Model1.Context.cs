﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KurumsalProje.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KKPBCEEntities : DbContext
    {
        public KKPBCEEntities()
            : base("name=KKPBCEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BSMGRBCECCM001> BSMGRBCECCM001 { get; set; }
        public virtual DbSet<BSMGRBCEGEN001> BSMGRBCEGEN001 { get; set; }
        public virtual DbSet<BSMGRBCEGEN002> BSMGRBCEGEN002 { get; set; }
        public virtual DbSet<BSMGRBCEGEN003> BSMGRBCEGEN003 { get; set; }
        public virtual DbSet<BSMGRBCEGEN004> BSMGRBCEGEN004 { get; set; }
        public virtual DbSet<BSMGRBCEGEN005> BSMGRBCEGEN005 { get; set; }
        public virtual DbSet<BSMGRBCEMAT001> BSMGRBCEMAT001 { get; set; }
        public virtual DbSet<BSMGRBCEOPR001> BSMGRBCEOPR001 { get; set; }
        public virtual DbSet<BSMGRBCEROT001> BSMGRBCEROT001 { get; set; }
        public virtual DbSet<BSMGRBCEWCM001> BSMGRBCEWCM001 { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<BSMGRBCEBOM001> BSMGRBCEBOM001 { get; set; }
        public virtual DbSet<BSMGRBCEWCMHEAD> BSMGRBCEWCMHEAD { get; set; }
        public virtual DbSet<BSMGRBCEWCMOPR> BSMGRBCEWCMOPR { get; set; }
        public virtual DbSet<BSMGRBCEWCMTEXT> BSMGRBCEWCMTEXT { get; set; }
        public virtual DbSet<BSMGRBCECCMHEAD> BSMGRBCECCMHEAD { get; set; }
        public virtual DbSet<BSMGRBCECCMTEXT> BSMGRBCECCMTEXT { get; set; }
        public virtual DbSet<BSMGRBCEBOMCONTENT> BSMGRBCEBOMCONTENT { get; set; }
        public virtual DbSet<BSMGRBCEBOMHEAD> BSMGRBCEBOMHEAD { get; set; }
        public virtual DbSet<BSMGRBCEROTBOMCONTENT> BSMGRBCEROTBOMCONTENT { get; set; }
        public virtual DbSet<BSMGRBCEROTHEAD> BSMGRBCEROTHEAD { get; set; }
        public virtual DbSet<BSMGRBCEROTOPRCONTENT> BSMGRBCEROTOPRCONTENT { get; set; }
        public virtual DbSet<BSMGRBCEMATHEAD> BSMGRBCEMATHEAD { get; set; }
        public virtual DbSet<BSMGRBCEMATTEXT> BSMGRBCEMATTEXT { get; set; }
    }
}
