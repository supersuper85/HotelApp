using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Extensions.Audit.AuditHelpers;
using HotelApp.BLL.Models.AuditModels;
using HotelApp.UnitTests.AuditSenderTests.AuditValidatorTests.TestModels;
using Xunit;

namespace HotelApp.UnitTests.AuditSenderTests.AuditPropertiesSetterTests
{
    public class SetOldValuesTests
    {
        [Fact]
        public void TestSetOldValues_WithInsertActionType()
        {
            var audit = new AuditCreateModel();
            audit.ActionType = "INSERT";

            var oldValuesObject = new ModelWithId();
            oldValuesObject.Id = 1;

            var auditValidator = new AuditSenderPropertiesSetter();

            Action act = () => auditValidator.SetOldValues(ref audit, oldValuesObject);
            Assert.Equal(null, audit.OldValues);
        }

        [Fact]
        public void TestSetOldValues_WithNonInsertActionType()
        {
            var audit = new AuditCreateModel();
            audit.ActionType = "UPDATE";

            var oldValuesObject = new ModelWithId();
            oldValuesObject.Id = 1;

            var expectedResult = Newtonsoft.Json.JsonConvert.SerializeObject(oldValuesObject);

            var auditValidator = new AuditSenderPropertiesSetter();
            auditValidator.SetOldValues(ref audit, oldValuesObject);

            Assert.Equal(expectedResult, audit.OldValues);
        }
    }
}
