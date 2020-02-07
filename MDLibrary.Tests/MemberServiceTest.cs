﻿using MDLibrary.Domain;
using MDLibrary.Infrastructure.Persistence;
using MDLibrary.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MDLibrary.Tests
{
    public class MemberServiceTest
    {
        [Fact]
        public void AddNewMember_AddsAMemberToList_ReturnsCorrectCount()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Member_AddNewMember").Options;
            var context = new ApplicationDbContext(options);
            SeedMockData(context);
            var memberToAdd = new Member { Name = "Test To Add" };
            var testMemberService = new MemberService(context);
            var expectedCountNr = 5;

            //Act
            testMemberService.AddNewMember(memberToAdd);
            var actualCountNr = context.Member.ToList().Count;

            //Assert
            Assert.Equal(expectedCountNr, actualCountNr);
        }
        [Fact]
        public void GetAllMembers_MockDataUnodered_GetAllMembersInOrder()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Members_GetAllMembers").Options;
            var context = new ApplicationDbContext(options);
            SeedMockData(context);
            var testMemberService = new MemberService(context);
            var expectedList = new List<Member>
            {
                new Member {ID=2, Name = "TestA Mem222", SSN="8002022222"},
                new Member {ID=1, Name = "TestB Mem111", SSN="8001011111"},
                new Member {ID=4, Name = "TestC Mem444", SSN="8004044444"},
                new Member {ID=3, Name = "TestD Mem333", SSN="8003033333"},
            };

            //act
            var actualList = testMemberService.GetAllMembers();

            //assert
            int nameNr = 0;
            foreach (var member in actualList)
            {
                Assert.Equal(expectedList[nameNr].Name, member.Name);
                nameNr++;
            }
        }

        private void SeedMockData(ApplicationDbContext context)
        {
            var members = new[]
            {
                new Member {ID=1, Name = "TestB Mem111", SSN="8001011111"},
                new Member {ID=2, Name = "TestA Mem222", SSN="8002022222"},
                new Member {ID=3, Name = "TestD Mem333", SSN="8003033333"},
                new Member {ID=4, Name = "TestC Mem444", SSN="8004044444"},
            };

            context.AddRange(members);
            context.SaveChanges();
        }
    }
}
