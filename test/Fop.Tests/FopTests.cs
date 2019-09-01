using System.Linq;
using Fop.Exceptions;
using Fop.Order;
using Fop.Filter;
using Sample.Entity;
using Xunit;

namespace Fop.Tests
{
    public class FopTests
    {
        private readonly IQueryable<Student> _students;

        public FopTests()
        {
            _students = DataInitializer.GenerateStudentList();
        }

        [Fact]
        public void ApplyFop_Should_Success_Returns_Filtered_When_Passed_Multiple_Filter()
        {
            // Arrange
            var request = new FopRequest
            {
                // Filter
                FilterList = new IFilterList[]
                {
                    new FilterList
                    {
                        Logic = FilterLogic.And,
                        Filters = new []
                        {
                            new Filter.Filter
                            {
                                Operator = FilterOperators.GreaterThan,
                                DataType = FilterDataTypes.Int,
                                Key = nameof(Student) + "." + nameof(Student.Midterm),
                                Value = "98"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.StartsWith,
                                DataType = FilterDataTypes.String,
                                Key = nameof(Student) + "." + nameof(Student.Name),
                                Value = "A"
                            }
                        }
                    }
                },

                // Order
                Direction = OrderDirection.Desc,
                OrderBy = nameof(Student.IdentityNumber),

                // Page
                PageSize = 100,
                PageNumber = 1
            };

            // Act
            var (result, totalRecords) = _students.ApplyFop(request);

            // Assert
            Assert.True(result.Any(x => x.Name == "Aybars"));
        }

        [Fact]
        public void ApplyFop_Should_Success_Returns_Filtered_When_Passed_Multiple_FilterList_With_DateTime_Filter()
        {
            // Arrange
            var request = new FopRequest
            {
                // Filter
                FilterList = new IFilterList[]
                {
                    new FilterList
                    {
                        Logic = FilterLogic.And,
                        Filters = new []
                        {
                            new Filter.Filter
                            {
                                Operator = FilterOperators.GreaterThan,
                                DataType = FilterDataTypes.DateTime,
                                Key = nameof(Student) + "." + nameof(Student.Birthday),
                                Value = "1993-06-07 00:00:00"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.LessThan,
                                DataType = FilterDataTypes.DateTime,
                                Key = nameof(Student) + "." + nameof(Student.Birthday),
                                Value = "1997-06-07 00:00:00"
                            }
                        }
                    }
                },


                // Order
                Direction = OrderDirection.Desc,
                OrderBy = nameof(Student.IdentityNumber)
            };

            // Act
            var (result, totalRecords) = _students.ApplyFop(request);


            // Assert
            Assert.True(result.Any(x => x.Name == "Aybars"));
            Assert.True(totalRecords == 1);
        }

        [Fact]
        public void ApplyFop_Should_Success_Returns_Filtered_When_Passed_Multiple_FilterList_With_Char_Filter()
        {
            // Arrange
            var request = new FopRequest
            {
                // Filter
                FilterList = new IFilterList[]
                {
                    new FilterList
                    {
                        Logic = FilterLogic.And,
                        Filters = new []
                        {
                            new Filter.Filter
                            {
                                Operator = FilterOperators.Equal,
                                DataType = FilterDataTypes.Char,
                                Key = nameof(Student) + "." + nameof(Student.Level),
                                Value = "a"
                            }
                        }
                    }
                },


                // Order
                Direction = OrderDirection.Desc,
                OrderBy = nameof(Student.IdentityNumber)
            };

            // Act
            var (result, totalRecords) = _students.ApplyFop(request);


            // Assert
            Assert.True(result.Any(x => x.Name == "Aybars"));
        }

        [Fact]
        public void ApplyFop_Should_Success_Returns_Filtered_When_Passed_Multiple_FilterList_With_Multiple_Filter()
        {
            // Arrange
            var request = new FopRequest
            {
                // Filter
                FilterList = new IFilterList[]
                {
                    new FilterList
                    {
                        Logic = FilterLogic.And,
                        Filters = new []
                        {
                            new Filter.Filter
                            {
                                Operator = FilterOperators.GreaterThan,
                                DataType = FilterDataTypes.Int,
                                Key = nameof(Student) + "." + nameof(Student.Final),
                                Value = "9"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.LessOrEqualThan,
                                DataType = FilterDataTypes.Int,
                                Key = nameof(Student) + "." + nameof(Student.Midterm),
                                Value = "100"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.StartsWith,
                                DataType = FilterDataTypes.String,
                                Key = nameof(Student) + "." + nameof(Student.Surname),
                                Value = "A"
                            }
                        }
                    },
                    new FilterList
                    {
                        Logic = FilterLogic.Or,
                        Filters =  new []
                        {
                            new Filter.Filter
                            {
                                Operator = FilterOperators.Equal,
                                DataType = FilterDataTypes.String,
                                Key = nameof(Student) + "." + nameof(Student.IdentityNumber),
                                Value = "100010"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.Equal,
                                DataType = FilterDataTypes.String,
                                Key = nameof(Student) + "." + nameof(Student.IdentityNumber),
                                Value = "100101"
                            },

                        }
                    }
                },


                // Order
                Direction = OrderDirection.Desc,
                OrderBy = nameof(Student.IdentityNumber)
            };

            // Act
            var (result, totalRecords) = _students.ApplyFop(request);


            // Assert
            Assert.True(result.Any(x => x.Name == "Aybars"));
        }

        [Fact]
        public void ApplyFop_Should_Fail_Returns_IntDataTypeNotSupportedException()
        {
            // Arrange
            var request = new FopRequest
            {
                // Filter
                FilterList = new IFilterList[]
                {
                    new FilterList
                    {
                        Logic = FilterLogic.And,
                        Filters = new []
                        {
                            new Filter.Filter
                            {
                                Operator = FilterOperators.Contains,
                                DataType = FilterDataTypes.Int,
                                Key = nameof(Student) + "." + nameof(Student.Midterm),
                                Value = "9"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.LessThan,
                                DataType = FilterDataTypes.Int,
                                Key = nameof(Student) + "." + nameof(Student.Final),
                                Value = "91"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.StartsWith,
                                DataType = FilterDataTypes.String,
                                Key = nameof(Student) + "." + nameof(Student.Name),
                                Value = "Author"
                            }
                        }
                    }
                },


                // Order
                Direction = OrderDirection.Desc,
                OrderBy = nameof(Student.IdentityNumber)
            };

            // Act
            var ex = Record.Exception(() => _students.ApplyFop(request));

            // Assert
            Assert.True(ex is IntDataTypeNotSupportedException);
        }

        [Fact]
        public void ApplyFop_Should_Fail_Returns_StringDataTypeNotSupportedException()
        {
            // Arrange
            var request = new FopRequest
            {
                // Filter
                FilterList = new IFilterList[]
                {
                    new FilterList
                    {
                        Logic = FilterLogic.And,
                        Filters = new []
                        {
                            new Filter.Filter
                            {
                                Operator = FilterOperators.GreaterThan,
                                DataType = FilterDataTypes.Int,
                                Key = nameof(Student) + "." + nameof(Student.Midterm),
                                Value = "9"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.LessThan,
                                DataType = FilterDataTypes.Int,
                                Key = nameof(Student) + "." + nameof(Student.Final),
                                Value = "91"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.GreaterThan,
                                DataType = FilterDataTypes.String,
                                Key = nameof(Student) + "." + nameof(Student.Name),
                                Value = "A"
                            }
                        }
                    }
                },


                // Order
                Direction = OrderDirection.Desc,
                OrderBy = nameof(Student.IdentityNumber)
            };

            // Act
            var ex = Record.Exception(() => _students.ApplyFop(request));

            // Assert
            Assert.True(ex is StringDataTypeNotSupportedException);
        }


        [Fact]
        public void ApplyFop_Should_Fail_Returns_LogicOperatorNotFoundException()
        {
            // Arrange
            var request = new FopRequest
            {
                // Filter
                FilterList = new IFilterList[]
                {
                    new FilterList
                    {
                        Filters = new []
                        {
                            new Filter.Filter
                            {
                                Operator = FilterOperators.GreaterThan,
                                DataType = FilterDataTypes.Int,
                                Key = nameof(Student) + "." + nameof(Student.Midterm),
                                Value = "9"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.LessThan,
                                DataType = FilterDataTypes.Int,
                                Key = nameof(Student) + "." + nameof(Student.Final),
                                Value = "91"
                            },
                            new Filter.Filter
                            {
                                Operator = FilterOperators.GreaterThan,
                                DataType = FilterDataTypes.String,
                                Key = nameof(Student) + "." + nameof(Student.Name),
                                Value = "A"
                            }
                        }
                    }
                },


                // Order
                Direction = OrderDirection.Desc,
                OrderBy = nameof(Student.IdentityNumber)
            };

            // Act
            var ex = Record.Exception(() => _students.ApplyFop(request));

            // Assert
            Assert.True(ex is StringDataTypeNotSupportedException);
        }
    }
}

