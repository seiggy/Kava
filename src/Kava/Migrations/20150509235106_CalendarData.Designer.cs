using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Kava.Models;

namespace Kava.Migrations
{
    [ContextType(typeof(ApplicationDbContext))]
    partial class CalendarData
    {
        public override string Id
        {
            get { return "20150509235106_CalendarData"; }
        }
        
        public override string ProductVersion
        {
            get { return "7.0.0-beta4-12943"; }
        }
        
        public override IModel Target
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Identity");
                
                builder.Entity("Kava.Models.ApplicationUser", b =>
                    {
                        b.Property<int>("AccessFailedCount")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("CharacterName")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("ConcurrencyStamp")
                            .ConcurrencyToken()
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("Email")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<bool>("EmailConfirmed")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<Guid?>("EventEventID")
                            .Annotation("OriginalValueIndex", 5)
                            .Annotation("ShadowIndex", 0);
                        b.Property<string>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<bool>("LockoutEnabled")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<DateTimeOffset?>("LockoutEnd")
                            .Annotation("OriginalValueIndex", 8);
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 9);
                        b.Property<string>("NormalizedEmail")
                            .Annotation("OriginalValueIndex", 10);
                        b.Property<string>("NormalizedUserName")
                            .Annotation("OriginalValueIndex", 11);
                        b.Property<string>("PasswordHash")
                            .Annotation("OriginalValueIndex", 12);
                        b.Property<string>("PhoneNumber")
                            .Annotation("OriginalValueIndex", 13);
                        b.Property<bool>("PhoneNumberConfirmed")
                            .Annotation("OriginalValueIndex", 14);
                        b.Property<string>("SecurityStamp")
                            .Annotation("OriginalValueIndex", 15);
                        b.Property<string>("Server")
                            .Annotation("OriginalValueIndex", 16);
                        b.Property<bool>("TwoFactorEnabled")
                            .Annotation("OriginalValueIndex", 17);
                        b.Property<string>("UserName")
                            .Annotation("OriginalValueIndex", 18);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "AspNetUsers");
                    });
                
                builder.Entity("Kava.Models.Blog.BlogCategory", b =>
                    {
                        b.Property<Guid>("BlogCategoryID")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("UrlSlug")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("BlogCategoryID");
                    });
                
                builder.Entity("Kava.Models.Blog.BlogPost", b =>
                    {
                        b.Property<Guid>("BlogPostID")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<Guid?>("CategoryBlogCategoryID")
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("ShadowIndex", 0);
                        b.Property<string>("Content")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("Meta")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<DateTimeOffset>("ModifiedDateTime")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<DateTimeOffset>("PostedDateTime")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<bool>("Published")
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<string>("ShortDescription")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<string>("Title")
                            .Annotation("OriginalValueIndex", 8);
                        b.Property<string>("UrlSlug")
                            .Annotation("OriginalValueIndex", 9);
                        b.Key("BlogPostID");
                    });
                
                builder.Entity("Kava.Models.Blog.BlogTag", b =>
                    {
                        b.Property<Guid>("BlogTagID")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("UrlSlug")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("BlogTagID");
                    });
                
                builder.Entity("Kava.Models.Calendar.Event", b =>
                    {
                        b.Property<string>("CreatedByUserId")
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("ShadowIndex", 0);
                        b.Property<DateTimeOffset>("EndDateTime")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("EventDescription")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<Guid>("EventID")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<string>("EventName")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("MaxSlots")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<int>("RepeatInterval")
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<bool>("Repeats")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<DateTimeOffset>("StartDateTime")
                            .Annotation("OriginalValueIndex", 8);
                        b.Key("EventID");
                    });
                
                builder.Entity("Kava.Models.Forum.ForumCategory", b =>
                    {
                        b.Property<string>("CategoryDescription")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("CategoryName")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<Guid>("ForumCategoryID")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 2);
                        b.Key("ForumCategoryID");
                    });
                
                builder.Entity("Kava.Models.Forum.ForumPost", b =>
                    {
                        b.Property<Guid?>("ForumCategoryForumCategoryID")
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("ShadowIndex", 0);
                        b.Property<Guid>("ForumPostID")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<Guid?>("ForumTopicForumTopicID")
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("ShadowIndex", 1);
                        b.Property<string>("PostContent")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<DateTimeOffset>("PostCreatedDateTime")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<DateTimeOffset>("PostUpdatedDateTime")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<string>("UserId")
                            .Annotation("OriginalValueIndex", 6)
                            .Annotation("ShadowIndex", 2);
                        b.Key("ForumPostID");
                    });
                
                builder.Entity("Kava.Models.Forum.ForumTopic", b =>
                    {
                        b.Property<Guid>("ForumTopicID")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<DateTimeOffset>("LastPostDate")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("OriginatingUserId")
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("ShadowIndex", 0);
                        b.Property<DateTimeOffset>("TopicCreatedDate")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<bool>("TopicLocked")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<string>("TopicTitle")
                            .Annotation("OriginalValueIndex", 5);
                        b.Key("ForumTopicID");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                    {
                        b.Property<string>("ConcurrencyStamp")
                            .ConcurrencyToken()
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("NormalizedName")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "AspNetRoles");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("ClaimType")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("ClaimValue")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("RoleId")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "AspNetRoleClaims");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("ClaimType")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("ClaimValue")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("UserId")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("Id");
                        b.Annotation("Relational:TableName", "AspNetUserClaims");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("LoginProvider")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("ProviderDisplayName")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("ProviderKey")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("UserId")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("LoginProvider", "ProviderKey");
                        b.Annotation("Relational:TableName", "AspNetUserLogins");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("RoleId")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("UserId")
                            .Annotation("OriginalValueIndex", 1);
                        b.Key("UserId", "RoleId");
                        b.Annotation("Relational:TableName", "AspNetUserRoles");
                    });
                
                builder.Entity("Kava.Models.ApplicationUser", b =>
                    {
                        b.ForeignKey("Kava.Models.Calendar.Event", "EventEventID");
                    });
                
                builder.Entity("Kava.Models.Blog.BlogPost", b =>
                    {
                        b.ForeignKey("Kava.Models.Blog.BlogCategory", "CategoryBlogCategoryID");
                    });
                
                builder.Entity("Kava.Models.Calendar.Event", b =>
                    {
                        b.ForeignKey("Kava.Models.ApplicationUser", "CreatedByUserId");
                    });
                
                builder.Entity("Kava.Models.Forum.ForumPost", b =>
                    {
                        b.ForeignKey("Kava.Models.Forum.ForumCategory", "ForumCategoryForumCategoryID");
                        b.ForeignKey("Kava.Models.Forum.ForumTopic", "ForumTopicForumTopicID");
                        b.ForeignKey("Kava.Models.ApplicationUser", "UserId");
                    });
                
                builder.Entity("Kava.Models.Forum.ForumTopic", b =>
                    {
                        b.ForeignKey("Kava.Models.ApplicationUser", "OriginatingUserId");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", "RoleId");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("Kava.Models.ApplicationUser", "UserId");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("Kava.Models.ApplicationUser", "UserId");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", "RoleId");
                        b.ForeignKey("Kava.Models.ApplicationUser", "UserId");
                    });
                
                return builder.Model;
            }
        }
    }
}
