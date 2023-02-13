using AutoMapper;
using Castle.Core.Resource;
using Customer_Information.WebAPI;
using Customer_Information.WebAPI.Controllers;
using Customer_Information.WebAPI.Models;
using Customer_Information.WebAPI.Models.Dto;
using Customer_Information.WebAPI.Repository;
using Customer_Information.WebAPI.Repository.IRepository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Information.Tests.Controller
{
    public class CustomerControllerTest
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly IMapper _mapper;
        private readonly CustomerController _controller;
        protected ApiResponse _response;
        public CustomerControllerTest()
        {
            _customerRepo = A.Fake<ICustomerRepository>();
            _countryRepo = A.Fake<ICountryRepository>();
            _mapper = A.Fake<IMapper>();
            //_controller = new CustomerController(_customerRepo, _countryRepo, _mapper);
            this._response = new();
        }

        [Fact]
        public async Task GetCustomerAllInfo_OkReturn()
        {
            //Arrange
            var customer = A.Fake<IEnumerable<Customer>>();
            var customerList = A.Fake<List<CustomerDto>>();
            A.CallTo(() => _mapper.Map<List<CustomerDto>>(customer)).Returns(customerList);

            var controller = new CustomerController(_customerRepo, _countryRepo, _mapper);

            // Act
            var result = await controller.GetCustomerAllInfo();

            // Assert
            var actionResult = result.Result as OkObjectResult;
            Assert.NotNull(actionResult);
            Assert.Equal(200, actionResult.StatusCode);

            var apiResponse = actionResult.Value as ApiResponse;
            Assert.NotNull(apiResponse);
            Assert.Equal(customerList, apiResponse.Result);
            Assert.Equal(HttpStatusCode.OK, apiResponse.StatusCode);
        }

    }
}
