using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessment.Api.UnitTests
{
    public static class FakerExtensions
    {
        public static string GetString(this Faker faker, int size = 5000)
        {
            return faker.Random.AlphaNumeric(size);
        }

        public static string? GetStringN(this Faker faker, int size = 5000)
        {
            return faker.Random.Bool() ? faker.Random.AlphaNumeric(size) : null;
        }

        public static decimal GetDecimal(this Faker faker)
        {
            return faker.Random.Decimal();
        }
        public static double GetDouble(this Faker faker)
        {
            return faker.Random.Double();
        }

        public static int GetInt(this Faker faker)
        {
            return faker.Random.Int();
        }

        public static int GetInt(this Faker faker, int min, int max)
        {
            return faker.Random.Int(min, max);
        }

        public static bool GetBool(this Faker faker)
        {
            return faker.Random.Bool();
        }

        public static long GetLong(this Faker faker)
        {
            return faker.Random.Long();
        }


        public static decimal? GetDecimalN(this Faker faker)
        {
            var nullable = faker.Random.Bool();
            return nullable ? (decimal?)null : faker.Random.Decimal();
        }

        public static int? GetIntN(this Faker faker)
        {
            var nullable = faker.Random.Bool();
            return nullable ? (int?)null : faker.Random.Int();
        }

        public static long? GetLongN(this Faker faker)
        {
            var nullable = faker.Random.Bool();
            return nullable ? (long?)null : faker.Random.Long();
        }

        public static bool? GetBoolN(this Faker faker)
        {
            var nullable = faker.Random.Bool();
            return nullable ? (bool?)null : faker.Random.Bool();
        }

        public static DateTime GetDateTime(this Faker faker)
        {
            return faker.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now);
        }

        public static DateTime? GetDateTimeN(this Faker faker)
        {
            var nullable = faker.Random.Bool();
            return nullable ? (DateTime?)null : faker.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now);
        }

        public static DateTime GetDateTime(this Faker faker, DateTime from, DateTime to)
        {
            return faker.Date.Between(from, to);
        }

        public static string GetPhoneNumber(this Faker faker, int maxLength = 15)
        {
            return faker.Phone.PhoneNumber("+1##########");
        }
        public static string? GetPhoneNumberN(this Faker faker, int maxLength = 15)
        {
            return faker.Random.Bool() ? null : faker.GetPhoneNumber(maxLength);
        }
        public static string GetCompanyName(this Faker faker, int maxLength = 15)
        {
            return faker.Company.CompanyName();
        }
        public static string? GetCompanyNameN(this Faker faker, int maxLength = 15)
        {
            return faker.Random.Bool() ? null : faker.GetCompanyName(maxLength);
        }

        public static T Get<T>(this Faker faker) where T : struct, Enum
        {
            return faker.PickRandom<T>();
        }

        public static T? GetN<T>(this Faker faker) where T : struct, Enum?
        {
            return faker.Random.Bool() ? faker.PickRandom<T>() : (T?)null;
        }

        public static string GetEmailAddress(this Faker faker, int maxLength = 254)
        {
            return faker.Person.Email;
        }
        public static string? GetEmailAddressN(this Faker faker, int maxLength = 254)
        {
            return faker.Random.Bool() ? null : faker.GetEmailAddress(maxLength);
        }

        public static string GetFirstName(this Faker faker, int maxLength = 60)
        {
            return faker.Person.FirstName;
        }
        public static string? GetFirstNameN(this Faker faker, int maxLength = 60)
        {
            return faker.Random.Bool() ? null : faker.GetFirstName(maxLength);
        }

        public static string GetLastName(this Faker faker, int maxLength = 60)
        {
            return faker.Person.LastName;
        }
        public static string? GetLastNameN(this Faker faker, int maxLength = 60)
        {
            return faker.Random.Bool() ? null : faker.GetLastName(maxLength);
        }
    }
}
