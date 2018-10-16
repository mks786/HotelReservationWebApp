﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System;
using Testing_IdentityDatabase_Inital_30_03.Models;

/// <summary>
/// Identity Models Generated by Identitys that have been expanded
/// </summary>
namespace Testing_IdentityDatabase_Inital_30_03.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <summary>
    /// ApplicationUser class that deals with users that has been expanded
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //Extended
        public string Title { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Mobile Number")]
        public string MobNo { get; set; }

        [Display(Name = "Address")]
        public string AddressFirstLine { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }



        //Nav
        public virtual ICollection Reservations { get; set; }
        public virtual ICollection Payments { get; set; }
        public virtual ICollection User_Roles { get; set; }



    }

    /// <summary>
    /// ApplicationDbContext that creates a context and the tables of it.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<BoardType> BoardTypes { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer()); 
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }

    /// <summary>
    /// DropCreateAlways used for testing that always drops the database
    /// </summary>
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        /// <summary>
        /// Seeding the database with test users and reservation materials
        /// </summary>
        /// <param name="context">A Database Context</param>
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userStore = new UserStore<ApplicationUser>(context);

                //_____________________________________________________________________
                //populating the Role table
                if (!roleManager.RoleExists(RoleNames.ROLE_ADMINISTRATOR))
                {
                    var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_ADMINISTRATOR));
                }

                if (!roleManager.RoleExists(RoleNames.ROLE_STAFF))
                {
                    var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_STAFF));
                }

                if (!roleManager.RoleExists(RoleNames.ROLE_CUSTOMER))
                {

                    var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_CUSTOMER));
                }

                //_________________________________________________________________

                string userName = "cara_105@hotmail.co.uk";
                string password = "123456";

                //create Admin user and role

                var user = userManager.FindByName(userName);
                if (user == null)
                {

                    var newUser = new ApplicationUser()
                    {
                        Title = "Mr",
                        FirstName = "Administrator",
                        LastName = "Admin",
                        PhoneNumber = "0141454637",
                        UserName = userName,
                        Email = userName,
                        MobNo = "073849573920",
                        AddressFirstLine = "23",
                        Street = "Ledrish Avenue",
                        City = "Balloch",
                        PostCode = "G838LJ",
                        EmailConfirmed = true


                    };

                    userManager.Create(newUser, password);
                    userManager.AddToRole(newUser.Id, RoleNames.ROLE_ADMINISTRATOR);
                }
                //___________________________________________________________________________________
                var user2 = userManager.FindByName("staff@test.com");
                if (user2 == null)
                {

                    var newUser2 = new ApplicationUser()
                    {
                        Title = "Mr",
                        FirstName = "Joe",
                        LastName = "Black",
                        PhoneNumber = "0141454637",
                        UserName = "staff@test.com",
                        Email = "staff@test.com",
                        MobNo = "073849573123",
                        AddressFirstLine = "33",
                        Street = "Ledrish Avenue",
                        City = "Balloch",
                        PostCode = "G838LJ",
                        EmailConfirmed = true

                    };



                    userManager.Create(newUser2, "staff123");
                    userManager.AddToRole(newUser2.Id, RoleNames.ROLE_STAFF);
                }

                //__________________________________________________________________
                var user3 = userManager.FindByName("customer@test2.com");
                if (user3 == null)
                {

                    var newUser3 = new ApplicationUser()
                    {
                        Title = "Mrs",
                        FirstName = "Suzzie",
                        LastName = "White",
                        PhoneNumber = "0141454637",
                        UserName = "customer@test2.com",
                        Email = "customer@test2.com",
                        MobNo = "073849573321",
                        AddressFirstLine = "13",
                        Street = "Ledrish Avenue",
                        City = "Balloch",
                        PostCode = "G838LJ",
                        EmailConfirmed = true

                    };


                    userManager.Create(newUser3, "customer123");
                    userManager.AddToRole(newUser3.Id, RoleNames.ROLE_CUSTOMER);
                }




            }

            //------------------------------

            //RoomTypes
            context.RoomTypes.Add(new RoomType()
            {
                Room_Type = "Single",
                Capacity = 1,
                Description = "A humble but spacious room that Sleeps one and is well suited to fit the lone warnderer or even those who are out on Buisness Trips. "
            });

            context.RoomTypes.Add(new RoomType()
            {
                Room_Type = "Twin",
                Capacity = 2,
                Description = "A room that sleeps two that is ideal for friends of family due to the serperate beds.  "

            });

            context.RoomTypes.Add(new RoomType()
            {
                Room_Type = "Double",
                Capacity = 2,
                Description = "A fantastic room that sleeps two and is perfect for couples looking to have a more than comfertable stay on the island. "
            });

            context.RoomTypes.Add(new RoomType()
            {
                Room_Type = "Family",
                Capacity = 4,
                Description = "Our largest room that is suited for sleeping families upto 4 in total. "
            });
            context.SaveChanges();
            //______________________________________________________

            //Rooms
            context.Rooms.Add(new Room()
            {
                RoomTypeId = 1,
                RoomType = context.RoomTypes.Find(1),
                RoomCosts = 100,
                RoomNumber = "101",
                
            });

            context.Rooms.Add(new Room()
            {
                RoomTypeId = 1,
                RoomType = context.RoomTypes.Find(1),
                RoomCosts = 100,
                RoomNumber = "102",

            });

            context.Rooms.Add(new Room()
            {
                RoomTypeId = 2,
                RoomType = context.RoomTypes.Find(2),
                RoomCosts = 200,
                RoomNumber = "103",

            });

            context.Rooms.Add(new Room()
            {
                RoomTypeId = 3,
                RoomType = context.RoomTypes.Find(3),
                RoomCosts = 200,
                RoomNumber = "201",
            });

            context.Rooms.Add(new Room()
            {
                RoomTypeId = 4,
                RoomType = context.RoomTypes.Find(4),
                RoomCosts = 200,
                RoomNumber = "202",
            });

            context.Rooms.Add(new Room()
            {
                RoomTypeId = 4,
                RoomType = context.RoomTypes.Find(4),
                RoomCosts = 300,
                RoomNumber = "203",
            });



            //______________________________________________________
            //Board Types
            context.BoardTypes.Add(new BoardType()
            {
                Board_Type = "Breakfast",
                BoardCosts = 100
            });

            context.BoardTypes.Add(new BoardType()
            {
                Board_Type = "Half",
                BoardCosts = 200
            });

            context.BoardTypes.Add(new BoardType()
            {
                Board_Type = "Full",
                BoardCosts = 300
            });

            base.Seed(context);
            context.SaveChanges();
            //______________________________________________________

            //Reservations
            Reservation newReservation = new Reservation()
            {

                Arrival = new DateTime(2017, 5, 18),
                Depature = new DateTime(2017, 6, 20),
                RoomId = context.Rooms.First().Id,
                Room = context.Rooms.First(),
                BoardTypeId = context.BoardTypes.First().Id,
                BoardType = context.BoardTypes.First(),
                UserId = userManager.Users.First().Id,
                ApplicationUsers = userManager.Users.First()


            };
            context.Reservations.Add(newReservation);

            newReservation.CalculateBookingBill((newReservation.Depature - newReservation.Arrival).Days);
            newReservation.CalculateDepositAmount((newReservation.Depature - newReservation.Arrival).Days);


            context.Reservations.Add(newReservation);

            //______________________________________________________
            //Extras
            context.Extras.Add(new Extra()
            {
                Type = "Gym and Sauna Use",
                Cost = 7,
                Bookable = true

            });

            context.Extras.Add(new Extra()
            {
                Type = "Daily Newspaper",
                Cost = 2,
                Bookable = true

            });

            context.Extras.Add(new Extra()
            {
                Type = "Safe Use",
                Cost = 4,
                Bookable = true

            });

            context.Extras.Add(new Extra()
            {
                Type = "Room Service Lunch",
                Cost = 6,
                Bookable = false

            });

            context.Extras.Add(new Extra()
            {
                Type = "Room Service Dinner",
                Cost = 10,
                Bookable = false

            });

            //______________________________________________________

            base.Seed(context);
            context.SaveChanges();
        }



    }

}