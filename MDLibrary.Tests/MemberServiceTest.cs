using MDLibrary.Domain;
using MDLibrary.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MDLibrary.Tests
{
    public class MemberServiceTest
    {
        [Fact]
        public void AddNewMember_AddsAMemberToList_ReturnsCountOne()
        {
            //Arrange
            var testMemberService = new MemberService();
            var testMember = new Member()
            {
                Name = "Janne"
            };

            var expectedResult = 1;

            //Act
            testMemberService.AddNewMember(testMember);
            var actualResult = testMemberService.Members.Count;

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
