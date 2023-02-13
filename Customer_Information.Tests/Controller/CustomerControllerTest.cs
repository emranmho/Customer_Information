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

        [Fact]
        public async Task CreateCustomer_OkReturn()
        {
            //Arrange
            var customerDto = new CustomerDto
            {
                CustomerName = "Jon",
                CountryId = 1,
                FatherName = "Dove",
                MotherName = "Lin",
                MeritialStatus = 1,
            };
            var customer = new Customer
            {
                CustomerName = customerDto.CustomerName,
                CountryId = customerDto.CountryId,
                FatherName = customerDto.FatherName,
                MotherName = customerDto.MotherName,
                MeritialStatus = customerDto.MeritialStatus,
            };

            A.CallTo(() => _mapper.Map<Customer>(customerDto)).Returns(customer);
            A.CallTo(() => _customerRepo.Create(customer)).Returns(Task.FromResult(0));
            

            var controller = new CustomerController(_customerRepo, _countryRepo, _mapper);

            // Act
            var result = await controller.CreateCustomerInfo(customerDto);

            // Assert
            var createdResult = result.Result as CreatedAtRouteResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);

            var apiResponse = createdResult.Value as ApiResponse;
            Assert.NotNull(apiResponse);
            Assert.Equal(customerDto, apiResponse.Result);
            Assert.Equal(HttpStatusCode.Created, apiResponse.StatusCode);
        }

    }
}
