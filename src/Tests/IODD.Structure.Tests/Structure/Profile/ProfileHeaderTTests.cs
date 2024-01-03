namespace IODD.Structure.Tests.Profile
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Profile;

    using Xunit;

    public class ProfileHeaderTTests
    {
        private readonly ProfileHeaderT _testClass;
        private readonly string _profileIdentification;
        private readonly string _profileRevision;
        private readonly string _profileName;
        private readonly string _profileSource;
        private readonly string _profileClassID;

        public ProfileHeaderTTests()
        {
            _profileIdentification = "TestValue284053785";
            _profileRevision = "TestValue289365005";
            _profileName = "TestValue712996940";
            _profileSource = "TestValue1116732365";
            _profileClassID = "TestValue2009017925";
            _testClass = new ProfileHeaderT(_profileIdentification, _profileRevision, _profileName, _profileSource, _profileClassID);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ProfileHeaderT(_profileIdentification, _profileRevision, _profileName, _profileSource, _profileClassID);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_ProfileHeaderT()
        {
            // Arrange
            var same = new ProfileHeaderT(_profileIdentification, _profileRevision, _profileName, _profileSource, _profileClassID);
            var different = new ProfileHeaderT("TestValue758769181", "TestValue2126882243", "TestValue42170293", "TestValue1402107999", "TestValue709578160");

            // Assert
            _testClass?.Equals(default(object)).Should().BeFalse();
            _testClass?.Equals(new object()).Should().BeFalse();
            _testClass?.Equals((object)same).Should().BeTrue();
            _testClass?.Equals((object)different).Should().BeFalse();
            _testClass?.Equals(same).Should().BeTrue();
            _testClass?.Equals(different).Should().BeFalse();
            _testClass?.GetHashCode().Should().Be(same.GetHashCode());
            _testClass?.GetHashCode().Should().NotBe(different.GetHashCode());
            (_testClass == same).Should().BeTrue();
            (_testClass == different).Should().BeFalse();
            (_testClass != same).Should().BeFalse();
            (_testClass != different).Should().BeTrue();
        }

        [Fact]
        public void ProfileIdentificationIsInitializedCorrectly()
        {
            _testClass.ProfileIdentification.Should().Be(_profileIdentification);
        }

        [Fact]
        public void ProfileRevisionIsInitializedCorrectly()
        {
            _testClass.ProfileRevision.Should().Be(_profileRevision);
        }

        [Fact]
        public void ProfileNameIsInitializedCorrectly()
        {
            _testClass.ProfileName.Should().Be(_profileName);
        }

        [Fact]
        public void ProfileSourceIsInitializedCorrectly()
        {
            _testClass.ProfileSource.Should().Be(_profileSource);
        }

        [Fact]
        public void ProfileClassIDIsInitializedCorrectly()
        {
            _testClass.ProfileClassID.Should().Be(_profileClassID);
        }
    }
}