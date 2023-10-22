using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence;

public class DataContext : IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserFollowing> UserFollowings { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new { aa.AppUserId, aa.ActivityId }));

        builder.Entity<ActivityAttendee>()
            .HasOne(u => u.AppUser)
            .WithMany(a => a.Activities)
            .HasForeignKey(aa => aa.AppUserId);

        builder.Entity<ActivityAttendee>()
            .HasOne(u => u.Activity)
            .WithMany(a => a.Attendees)
            .HasForeignKey(aa => aa.ActivityId);

        builder.Entity<Activity>()
            .Property(e => e.Options)
            .HasConversion(GetValueConverter(), new OptionsValueComparer(false));

        builder.Entity<Comment>()
            .HasOne(a => a.Activity)
            .WithMany(c => c.Comments)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserFollowing>(b =>
        {
            b.HasKey(k => new { k.ObserverId, k.TargetId });

            b.HasOne(o => o.Observer)
                .WithMany(f => f.Followings)
                .HasForeignKey(o => o.ObserverId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(t => t.Target)
                .WithMany(f => f.Followers)
                .HasForeignKey(t => t.TargetId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Transaction>()
            .HasOne(t => t.TransactionUser)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.TransactionUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }


    private ValueConverter<string[], string> GetValueConverter()
    {
        // Configure the value converter for ActivityOptions property
        var converter = new ValueConverter<string[], string>(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        return converter;
    }
}

public class OptionsValueComparer : ValueComparer<string[]>
{
    public OptionsValueComparer(bool favorStructuralComparisons) : base(favorStructuralComparisons)
    {
    }

    public override bool Equals(string[] x, string[] y)
    {
        if (x == null && y == null)
        {
            return true;
        }

        if (x == null || y == null)
        {
            return false;
        }

        if (x.Length != y.Length)
        {
            return false;
        }

        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode(string[] x)
    {
        if (x == null)
        {
            return 0;
        }

        int hashCode = 0;
        foreach (string item in x)
        {
            hashCode += item.GetHashCode();
        }

        return hashCode;
    }
}